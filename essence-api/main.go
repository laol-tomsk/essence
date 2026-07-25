package main

import (
	"calculator/api_neo4j"
	calculate "calculator/calculate_module"
	"calculator/server"
	"context"
	"encoding/json"
	"fmt"
	"net/http"
	"os"
	"os/signal"
	"sort"
	"strings"
	"sync"
	"syscall"
	"time"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
)

var (
	is_calculating = false

	USE_OBJECTIVE_ONLY = true
)

type Response struct {
	Res string `json:"res"`
}

type data_dict_json struct {
	Data_dict     map[string][]bool
	Data_add_dict map[string][]map[string][]map[string]bool
	Weight        map[string]int64
	Costs         map[string]int
	Method_id     int64
	Iter          int
	Threshold     float64
	IterLength    int
	Algorithm     int
	Iter_count    int
}

func track(msg string) (string, time.Time) {
	return msg, time.Now()
}

func duration(msg string, start time.Time) {
	fmt.Printf("%v: %v\n", msg, time.Since(start))
}

func Calculate(c *gin.Context) {
	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}
	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))
	res, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.JSON(http.StatusOK, Response{
		Res: res,
	})
}

func CalcIncrease(st data_dict_json, iteration_plan []string) {
	node_names := calculate.GetNodeNames(st.Method_id)
	calculate.Default_values = nil
	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))
	defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var defValues map[string]float64
	json.Unmarshal([]byte(defValuesString), &defValues)

	sum := 0.0
	for i, v := range defValues {
		if _, ok := st.Costs[node_names[i]]; ok {
			sum += v * float64(st.Costs[node_names[i]])
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			sum += v * float64(st.Costs[strings.Split(i, "_")[0]])
		}
	}
	fmt.Println(sum)

	data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
	data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
	_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)

	data_dict_copy := make(map[string][]bool)
	data_dict_copy_json, _ := json.Marshal(st.Data_dict)
	_ = json.Unmarshal(data_dict_copy_json, &data_dict_copy)

	for _, i := range iteration_plan {
		for _, v0 := range data_add_dict_copy {
			for i1 := range v0 {
				for i2 := range v0[i1] {
					if i2 == i {
						if st.Iter-st.Iter_count+len(v0[i1][i2]) >= 0 {
							v0[i1][i2][st.Iter-st.Iter_count+len(v0[i1][i2])][fmt.Sprint(st.Iter)] = true
						}
					}
				}
			}
		}
		for ix, vx := range data_dict_copy {
			if ix == i {
				vx[st.Iter] = true
			}
		}
	}

	calculate.Default_values = nil
	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))
	newValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, data_dict_copy, data_add_dict_copy, st.Weight, st.Method_id)
	var newValues map[string]float64
	json.Unmarshal([]byte(newValuesString), &newValues)

	newSum := 0.0
	for i, v := range newValues {
		if _, ok := st.Costs[node_names[i]]; ok {
			newSum += v * float64(st.Costs[node_names[i]])
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			newSum += v * float64(st.Costs[strings.Split(i, "_")[0]])
		}
	}

	fmt.Println(newSum)
	fmt.Println(newSum - sum)
	is_calculating = false
}

func SelectNext(c *gin.Context) {
	fmt.Println("SelectNext got!")
	if is_calculating {
		return
	} else {
		is_calculating = true
	}

	defer duration(track("SelectNext"))

	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}

	fmt.Println("iter: ", st.Iter)
	fmt.Println("iterCount: ", st.Iter_count)

	res2 := SelectNextInternals(st, true)

	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	res2Json, _ := json.MarshalIndent(&res2, "", "   ")

	file, _ := os.Create("C:\\redmine-essence\\redmine\\plugins\\semat_essence\\results.json")
	file.WriteString(string(res2Json))
	file.Close()

	is_calculating = false

	c.JSON(http.StatusOK, Response{
		Res: string(res2Json),
	})
}

func PlanIteration(c *gin.Context) {
	fmt.Println("PlanIteration got!")
	if is_calculating {
		return
	} else {
		is_calculating = true
	}

	defer duration(track("PlanIteration"))

	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}

	fmt.Println(st.Algorithm)
	fmt.Println(st.IterLength)
	fmt.Println(st.Iter)
	fmt.Println(st.Iter_count)

	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))
	var iteration_plan []string
	if st.Algorithm == 0 {
		iteration_plan, _ = PlanIterationInternalsGreedy(st, make([]string, 0))
	}
	if st.Algorithm == 1 {
		iteration_plan, _ = PlanIterationInternalsTree(st, make([]string, 0))
	}
	if st.Algorithm == 2 {
		iteration_plan, _ = PlanIterationInternalsNaive(st, make([]string, 0))
	}

	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	res2Json, _ := json.MarshalIndent(&iteration_plan, "", "   ")

	file, _ := os.Create("C:\\redmine-essence\\redmine\\plugins\\semat_essence\\results.json")
	file.WriteString(string(res2Json))
	file.Close()

	CalcIncrease(st, iteration_plan)

	is_calculating = false

	c.JSON(http.StatusOK, Response{
		Res: string(res2Json),
	})
}

func PlanIterationInternalsGreedy(st data_dict_json, iteration_plan []string) ([]string, float64) {
	// считаем, какая будет сумма, если вообще ничего не добавлять
	calculate.Default_values = nil
	defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var defValues map[string]float64
	json.Unmarshal([]byte(defValuesString), &defValues)
	defSum := 0.0
	node_names := calculate.GetNodeNames(st.Method_id)
	for i, v := range defValues {
		if _, ok := st.Costs[node_names[i]]; ok {
			defSum += v * float64(st.Costs[node_names[i]])
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			defSum += v * float64(st.Costs[strings.Split(i, "_")[0]])
		}
	}
	var res_plan []string
	res_plan = iteration_plan
	res_value := defSum

	//считаем текущую стоимость итерации
	currentCost := 0
	for _, i := range iteration_plan {
		if _, ok := st.Costs[node_names[i]]; ok {
			currentCost += st.Costs[node_names[i]]
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			currentCost += st.Costs[strings.Split(i, "_")[0]]
		}
	}
	fmt.Println("current cost is ", currentCost, ", value is ", defSum, ", num of tasks is ", len(iteration_plan))

	//вычисляем результат применения каждой возможной галочки
	select_next_res := SelectNextInternals(st, false)
	select_next_keys := make([]string, 0, len(select_next_res))
	for k := range select_next_res {
		select_next_keys = append(select_next_keys, k)
	}

	//сортируем по эффективность / часы
	sort.Slice(select_next_keys, func(i int, j int) bool {
		var valI, valJ float64
		valI = select_next_res[select_next_keys[i]]["sum"] - defSum
		valJ = select_next_res[select_next_keys[j]]["sum"] - defSum
		if _, ok := st.Costs[node_names[select_next_keys[i]]]; ok {
			valI /= float64(st.Costs[node_names[select_next_keys[i]]])
		}
		if _, ok := st.Costs[strings.Split(select_next_keys[i], "_")[0]]; ok {
			valI /= float64(st.Costs[strings.Split(select_next_keys[i], "_")[0]])
		}
		if _, ok := st.Costs[node_names[select_next_keys[j]]]; ok {
			valJ /= float64(st.Costs[node_names[select_next_keys[j]]])
		}
		if _, ok := st.Costs[strings.Split(select_next_keys[j], "_")[0]]; ok {
			valJ /= float64(st.Costs[strings.Split(select_next_keys[j], "_")[0]])
		}
		return valI > valJ
	})

	//перебираем все непоставленные галочки от лучшей к худшей
	recursionCalled := false
	for k := range select_next_keys {
		var st_copy data_dict_json
		st_copy.Weight = st.Weight
		st_copy.Costs = st.Costs
		st_copy.Method_id = st.Method_id
		st_copy.Iter = st.Iter
		st_copy.Threshold = st.Threshold
		st_copy.IterLength = st.IterLength
		st_copy.Iter_count = st.Iter_count

		//если очередная галочка не пробивает лимит стоимости, вызываем расчёт с добавлением неё
		costK := 1000
		if _, ok := st.Costs[node_names[select_next_keys[k]]]; ok {
			costK = st.Costs[node_names[select_next_keys[k]]]
		}
		if _, ok := st.Costs[strings.Split(select_next_keys[k], "_")[0]]; ok {
			costK = st.Costs[strings.Split(select_next_keys[k], "_")[0]]
		}

		if currentCost+costK <= st.IterLength {
			data_dict_copy := make(map[string][]bool)
			for i, v := range st.Data_dict {
				for _, v2 := range v {
					data_dict_copy[i] = append(data_dict_copy[i], v2)
				}
			}
			data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
			data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
			_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)
			if _, ok := data_dict_copy[select_next_keys[k]]; ok {
				data_dict_copy[select_next_keys[k]][int(st.Iter)] = true
			} else {
				for _, v0 := range data_add_dict_copy {
					for i1 := range v0 {
						for i2 := range v0[i1] {
							if i2 == select_next_keys[k] {
								if st.Iter-st.Iter_count+len(v0[i1][i2]) >= 0 {
									v0[i1][i2][st.Iter-st.Iter_count+len(v0[i1][i2])][fmt.Sprint(st.Iter)] = true
								}
							}
						}
					}
				}
			}
			st_copy.Data_dict = data_dict_copy
			st_copy.Data_add_dict = data_add_dict_copy
			iteration_plan = append(iteration_plan, select_next_keys[k])

			// делаем расчёт оптимального плана с проставленной галочкой
			new_res_plan, new_res_value := PlanIterationInternalsGreedy(st_copy, iteration_plan)

			// если новый план даёт больший результат, выбираем его
			if new_res_value >= res_value {
				res_plan = new_res_plan
				res_value = new_res_value
			}
			recursionCalled = true
		}

		if recursionCalled {
			break
		}
	}

	// возвращаем результат: либо лучшую из веток рекурсии, либо текущую итерацию, если веток рекурсии не нашлось
	return res_plan, res_value
}

func PlanIterationInternalsTree(st data_dict_json, iteration_plan []string) ([]string, float64) {
	//считаем текущую стоимость итерации
	node_names := calculate.GetNodeNames(st.Method_id)
	currentCost := 0
	for _, i := range iteration_plan {
		if _, ok := st.Costs[node_names[i]]; ok {
			currentCost += st.Costs[node_names[i]]
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			currentCost += st.Costs[strings.Split(i, "_")[0]]
		}
	}

	recursion_called := false
	met_last_one := len(iteration_plan) == 0
	var best_plan []string
	best_value := 0.0
	//перебираем все галочки...

	if !USE_OBJECTIVE_ONLY {
		for i, v := range st.Data_dict {
			//...кроме тех, которые уже перебрали на более высоких уровнях дерева...
			if !met_last_one && i == iteration_plan[len(iteration_plan)-1] {
				met_last_one = true
			}
			if !met_last_one {
				continue
			}
			//...кроме отмеченных...
			if v[int(st.Iter)] == true {
				continue
			}
			//...кроме тех, которые не влазят в объём итерации...
			costK := 1000
			if _, ok := st.Costs[node_names[i]]; ok {
				costK = st.Costs[node_names[i]]
			}
			if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
				costK = st.Costs[strings.Split(i, "_")[0]]
			}
			if currentCost+costK > st.IterLength {
				continue
			}

			//копируем данные
			var st_copy data_dict_json
			st_copy.Weight = st.Weight
			st_copy.Costs = st.Costs
			st_copy.Method_id = st.Method_id
			st_copy.Iter = st.Iter
			st_copy.Threshold = st.Threshold
			st_copy.IterLength = st.IterLength
			st_copy.Iter_count = st.Iter_count
			data_dict_copy := make(map[string][]bool)
			for i2, v2 := range st.Data_dict {
				for _, v3 := range v2 {
					data_dict_copy[i2] = append(data_dict_copy[i2], v3)
				}
			}
			data_dict_copy[i][int(st.Iter)] = true
			st_copy.Data_dict = data_dict_copy
			st_copy.Data_add_dict = st.Data_add_dict
			iteration_plan_copy := append(iteration_plan, i)
			//вызываем расчёт для поддерева
			new_res_plan, new_res_value := PlanIterationInternalsTree(st_copy, iteration_plan_copy)

			//если это поддерево оказалось лучше предыдущих, обновляем оптимумы
			if new_res_value > best_value {
				best_plan = new_res_plan
				best_value = new_res_value
			}
			recursion_called = true
		}
	}

	//дополнительные тоже
	for i0, v0 := range st.Data_add_dict {
		for i1 := range v0 {
			for i2 := range v0[i1] {
				//...кроме тех, которые уже перебрали на более высоких уровнях дерева...
				if !met_last_one && i2 == iteration_plan[len(iteration_plan)-1] {
					met_last_one = true
				}
				if !met_last_one {
					continue
				}
				//...кроме отмеченных...
				if st.Iter-st.Iter_count+len(v0[i1][i2]) < 0 {
					continue
				}
				if v0[i1][i2][st.Iter-st.Iter_count+len(v0[i1][i2])][fmt.Sprint(st.Iter)] {
					continue
				}
				//...кроме тех, которые не влазят в объём итерации...
				costI2 := 1000
				if _, ok := st.Costs[node_names[i2]]; ok {
					costI2 = st.Costs[node_names[i2]]
				}
				if _, ok := st.Costs[strings.Split(i2, "_")[0]]; ok {
					costI2 = st.Costs[strings.Split(i2, "_")[0]]
				}
				if currentCost+costI2 > st.IterLength {
					continue
				}
				//копируем данные
				var st_copy data_dict_json
				st_copy.Weight = st.Weight
				st_copy.Costs = st.Costs
				st_copy.Method_id = st.Method_id
				st_copy.Iter = st.Iter
				st_copy.Threshold = st.Threshold
				st_copy.IterLength = st.IterLength
				st_copy.Data_dict = st.Data_dict
				st_copy.Iter_count = st.Iter_count
				data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
				data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
				_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)
				data_add_dict_copy[i0][i1][i2][st.Iter-st.Iter_count+len(data_add_dict_copy[i0][i1][i2])][fmt.Sprint(st.Iter)] = true
				st_copy.Data_add_dict = data_add_dict_copy
				iteration_plan_copy := append(iteration_plan, i2)
				//вызываем расчёт для поддерева
				new_res_plan, new_res_value := PlanIterationInternalsTree(st_copy, iteration_plan_copy)

				//если это поддерево оказалось лучше предыдущих, обновляем оптимумы
				if new_res_value > best_value {
					best_plan = new_res_plan
					best_value = new_res_value
				}
				recursion_called = true
			}
		}
	}

	//если ни одно поддерево не было использовано
	if !recursion_called {
		calculate.Default_values = nil
		defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
		var defValues map[string]float64
		json.Unmarshal([]byte(defValuesString), &defValues)
		defSum := 0.0
		for i, v := range defValues {
			if _, ok := st.Costs[node_names[i]]; ok {
				defSum += v * float64(st.Costs[node_names[i]])
			}
			if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
				defSum += v * float64(st.Costs[strings.Split(i, "_")[0]])
			}
		}
		best_plan = iteration_plan
		best_value = defSum
	}

	if len(iteration_plan) == 0 {
		fmt.Println("best tree plan: ", best_plan, ", value: ", best_value)
	}

	// возвращаем результат: либо лучшую из веток рекурсии, либо текущую итерацию, если веток рекурсии не нашлось
	return best_plan, best_value
}

func PlanIterationInternalsNaive(st data_dict_json, iteration_plan []string) ([]string, float64) {
	// считаем, какая будет сумма, если вообще ничего не добавлять
	calculate.Default_values = nil
	defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var defValues map[string]float64
	json.Unmarshal([]byte(defValuesString), &defValues)
	defSum := 0.0
	node_names := calculate.GetNodeNames(st.Method_id)

	for i, v := range defValues {
		if _, ok := st.Costs[node_names[i]]; ok {
			defSum += v * float64(st.Costs[node_names[i]])
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			defSum += v * float64(st.Costs[strings.Split(i, "_")[0]])
		}
	}

	//вычисляем результат применения каждой возможной галочки
	select_next_res := SelectNextInternals(st, false)
	select_next_keys := make([]string, 0, len(select_next_res))
	for k := range select_next_res {
		select_next_keys = append(select_next_keys, k)
	}

	//создаём хранилища данных
	data := make([][]float64, len(select_next_keys)+1)
	plans := make([][][]string, len(select_next_keys)+1)
	for i := 0; i <= len(select_next_keys); i++ {
		data[i] = make([]float64, st.IterLength+1)
		plans[i] = make([][]string, st.IterLength+1)
	}

	//динамическое программирование
	for j := 0; j <= st.IterLength; j++ {
		data[0][j] = 0
		plans[0][j] = make([]string, 0)
	}

	for i := 1; i <= len(select_next_keys); i++ {
		costI := 1000
		if _, ok := st.Costs[node_names[select_next_keys[i-1]]]; ok {
			costI = st.Costs[node_names[select_next_keys[i-1]]]
		}
		if _, ok := st.Costs[strings.Split(select_next_keys[i-1], "_")[0]]; ok {
			costI = st.Costs[strings.Split(select_next_keys[i-1], "_")[0]]
		}
		for j := 0; j <= st.IterLength; j++ {
			if costI > j {
				data[i][j] = data[i-1][j]
				plans[i][j] = make([]string, len(plans[i-1][j]))
				copy(plans[i][j], plans[i-1][j])
			} else {
				if data[i-1][j] > data[i-1][j-costI]+select_next_res[select_next_keys[i-1]]["sum"]-defSum {
					data[i][j] = data[i-1][j]
					plans[i][j] = make([]string, len(plans[i-1][j]))
					copy(plans[i][j], plans[i-1][j])
				} else {
					data[i][j] = data[i-1][j-costI] + select_next_res[select_next_keys[i-1]]["sum"] - defSum
					plans[i][j] = make([]string, len(plans[i-1][j-costI])+1)
					plans[i][j] = append(plans[i-1][j-costI], select_next_keys[i-1])
				}
			}
		}
	}

	//считаем результат со всеми предложенными галочками
	data_dict_copy := make(map[string][]bool)
	for i, v := range st.Data_dict {
		for _, v2 := range v {
			data_dict_copy[i] = append(data_dict_copy[i], v2)
		}
	}
	data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
	data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
	_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)
	for _, i := range plans[len(select_next_keys)][st.IterLength] {
		if _, ok := data_dict_copy[i]; ok {
			data_dict_copy[i][int(st.Iter)] = true
		} else {
			for _, v0 := range data_add_dict_copy {
				for i1 := range v0 {
					for i2 := range v0[i1] {
						if i2 == i {
							if st.Iter-st.Iter_count+len(v0[i1][i2]) >= 0 {
								v0[i1][i2][st.Iter-st.Iter_count+len(v0[i1][i2])][fmt.Sprint(st.Iter)] = true
							}
						}
					}
				}
			}
		}
	}
	calculate.Default_values = nil
	final_res_string, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, data_dict_copy, st.Data_add_dict, st.Weight, st.Method_id)
	var final_res map[string]float64
	json.Unmarshal([]byte(final_res_string), &final_res)
	res_value := 0.0
	for i, v := range final_res {
		if _, ok := st.Costs[node_names[i]]; ok {
			res_value += v * float64(st.Costs[node_names[i]])
		}
		if _, ok := st.Costs[strings.Split(i, "_")[0]]; ok {
			res_value += v * float64(st.Costs[strings.Split(i, "_")[0]])
		}
	}

	return plans[len(select_next_keys)][st.IterLength], res_value
}

func SelectNextInternals(st data_dict_json, convert_to_node_names bool) map[string]map[string]float64 {
	calculate.Default_values = nil
	var res map[string]map[string]float64
	res = make(map[string]map[string]float64)
	var wg sync.WaitGroup

	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))

	defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var defValues map[string]float64
	json.Unmarshal([]byte(defValuesString), &defValues)

	for i, v := range st.Data_dict {
		if USE_OBJECTIVE_ONLY {
			continue
		}
		if v[int(st.Iter)] == true {
			continue
		}

		data_dict_copy := make(map[string][]bool)
		for i2, v2 := range st.Data_dict {
			for _, v3 := range v2 {
				data_dict_copy[i2] = append(data_dict_copy[i2], v3)
			}
		}
		data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
		data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
		_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)
		data_dict_copy[i][int(st.Iter)] = true
		wg.Add(1)
		go calculate.StartCalculateWrapper(int(st.Iter), 1, st.Threshold, data_dict_copy, data_add_dict_copy, st.Weight, st.Method_id, &wg, &res, i, defValues, false)
	}

	for i0, v0 := range st.Data_add_dict {
		for i1 := range v0 {
			for i2 := range v0[i1] {
				if st.Iter-st.Iter_count+len(v0[i1][i2]) < 0 {
					continue
				}
				if v0[i1][i2][st.Iter-st.Iter_count+len(v0[i1][i2])][fmt.Sprint(st.Iter)] {
					continue
				}
				data_add_dict_copy := make(map[string][]map[string][]map[string]bool)
				data_add_dict_copy_json, _ := json.Marshal(st.Data_add_dict)
				_ = json.Unmarshal(data_add_dict_copy_json, &data_add_dict_copy)
				data_add_dict_copy[i0][i1][i2][st.Iter-st.Iter_count+len(data_add_dict_copy[i0][i1][i2])][fmt.Sprint(st.Iter)] = true
				wg.Add(1)
				go calculate.StartCalculateWrapper(int(st.Iter), 1, st.Threshold, st.Data_dict, data_add_dict_copy, st.Weight, st.Method_id, &wg, &res, i2, defValues, true)
			}
		}
	}

	wg.Wait()
	var res2 map[string]map[string]float64
	res2 = make(map[string]map[string]float64)
	node_names := calculate.GetNodeNames(st.Method_id)
	best_sum := -1.0
	best_i := ""

	for i, v := range res {
		sum := 0.0
		self := 0.0
		for i2, v2 := range v {
			if _, ok := st.Costs[node_names[i2]]; ok {
				sum += v2 * float64(st.Costs[node_names[i2]])
			}
			if _, ok := st.Costs[strings.Split(i2, "_")[0]]; ok {
				sum += v2 * float64(st.Costs[strings.Split(i2, "_")[0]])
			}
			if node_names[i] == i2 {
				self = v2
			}
		}

		_, ok := node_names[i]
		if convert_to_node_names && ok && node_names[i] != "None" {
			res2[node_names[i]] = make(map[string]float64)
			res2[node_names[i]]["sum"] = sum
			res2[node_names[i]]["self"] = self
			res2[node_names[i]]["is_best"] = 0.0
			if sum > best_sum {
				best_sum = sum
				best_i = node_names[i]
			}
		} else {
			res2[i] = make(map[string]float64)
			res2[i]["sum"] = sum
			res2[i]["self"] = self
			res2[i]["is_best"] = 0.0
			if sum > best_sum {
				best_sum = sum
				best_i = i
			}
		}
	}

	res2[best_i]["is_best"] = 1.0

	return res2
}

func GetAllNode(c *gin.Context) {
	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	type data_dict_json struct {
		Method_id int64
	}

	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}

	session := api_neo4j.GetSession(st.Method_id)
	defer session.Close()

	mas_node_id, err := api_neo4j.Get_guid_all_nodes(session)
	if err != nil {
		c.AbortWithStatusJSON(http.StatusInternalServerError, err)
		return
	}

	dict := map[string][]string{}
	for _, guid := range mas_node_id {
		child, err := api_neo4j.Get_node_parents(session, guid)
		if err != nil {
			c.AbortWithStatusJSON(http.StatusInternalServerError, err)
			return
		}
		node_p_name, err := api_neo4j.Get_node_name(session, guid)
		if err != nil {
			c.AbortWithStatusJSON(http.StatusInternalServerError, err)
			return
		}
		if node_p_name == "None" {
			continue
		}
		dict_child_name := []string{}
		for _, child_guid := range child {
			child_name, err := api_neo4j.Get_node_name(session, child_guid)
			if err != nil {
				c.AbortWithStatusJSON(http.StatusInternalServerError, err)
				return
			}
			dict_child_name = append(dict_child_name, child_name)
		}
		dict[node_p_name] = dict_child_name

	}
	c.JSON(http.StatusOK, dict)
}

func main() {
	router := gin.New()
	router.Use(gin.Recovery())
	router.Use(cors.New(cors.Config{
		AllowOrigins:  []string{"*"},
		AllowMethods:  []string{http.MethodGet, http.MethodPost, http.MethodDelete, http.MethodOptions},
		AllowHeaders:  []string{"Origin"},
		ExposeHeaders: []string{"Content-Length"},
		MaxAge:        12 * time.Hour,
	}))

	router.POST("/calculate", Calculate)
	//router.POST("/calculate", CalcIncrease)
	router.GET("/list_checkpoints", GetAllNode)
	router.GET("/select_next", SelectNext)
	router.GET("/plan_iteration", PlanIteration)

	srv := new(server.Server)

	go func() {
		if err := srv.Run("localhost", "50050", router); err != nil {
			_ = fmt.Errorf(fmt.Sprintf("listen and serve: %s", err.Error()))
		}
	}()

	// handle signals
	quit := make(chan os.Signal, 1)
	signal.Notify(quit, syscall.SIGTERM, syscall.SIGINT)
	<-quit

	defer func() { fmt.Println("shutdown complete") }()

	// perform shutdown
	serverCtx, serverCancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer serverCancel()
	if err := srv.Shutdown(serverCtx); err != nil {
		_ = fmt.Errorf(fmt.Sprintf("shutdown failed: %s", err.Error()))
	}

}
