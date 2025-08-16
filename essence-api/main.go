package main

import (
	"calculator/api_neo4j"
	calculate "calculator/calculate_module"
	"calculator/server"
	"context"
	"encoding/json"
	"flag"
	"fmt"
	"net/http"
	"os"
	"os/signal"
	"sort"
	"sync"
	"syscall"
	"time"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
)

var (
	port           = flag.Int("port", 50052, "The server port")
	is_calculating = false
)

type Response struct {
	Res  string `json:"res"`
	plh  string
	plhp string
}

type Response2 struct {
	mas_id []string `json:"mas_Id"`
}

type data_dict_json struct {
	Data_dict     map[string][]bool
	Data_add_dict map[string][]map[string][]map[string]bool
	Weight        map[string]int64
	Costs         map[string]int
	Method_id     int64
	Iter          int64
	Threshold     float64
	IterLength    int
	Algorithm     int
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
	fmt.Println(st.Data_dict)
	api_neo4j.Prepare(api_neo4j.GetSession(st.Method_id))
	res, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	c.Writer.Header().Set("Access-Control-Allow-Origin", "*")
	c.JSON(http.StatusOK, Response{
		Res: res,
	})
}

func SelectNext(c *gin.Context) {
	fmt.Println("SelectNext got!")
	if is_calculating {
		return
	} else {
		is_calculating = true
	}

	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}

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
	defer duration(track("PlanIteration"))
	fmt.Println("PlanIteration got!")
	if is_calculating {
		return
	} else {
		is_calculating = true
	}

	var st data_dict_json
	if err := c.ShouldBindJSON(&st); err != nil {
		fmt.Println("error parsing json:" + err.Error())
		c.JSON(http.StatusBadRequest, "error parsing")
		return
	}

	fmt.Println(st.Algorithm)
	fmt.Println(st.IterLength)
	fmt.Println(st.Iter)

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

	calculate.Default_values = nil
	dataForOutput, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var dataForOutputJson map[string]float64
	json.Unmarshal([]byte(dataForOutput), &dataForOutputJson)
	var res2 map[string]map[string]float64
	res2 = make(map[string]map[string]float64)
	node_names := calculate.GetNodeNames(st.Method_id)
	for i, v := range dataForOutputJson {
		item := make(map[string]float64)
		item["sum"] = v
		item["self"] = v
		item["is_best"] = 0.0
		for _, elem := range iteration_plan {
			_, ok := node_names[elem]
			if ok && node_names[elem] == i {
				item["is_best"] = 1.0
			}
			if !ok && elem == i {
				item["is_best"] = 1.0
			}
		}
		res2[i] = item
	}

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

func PlanIterationInternalsGreedy(st data_dict_json, iteration_plan []string) ([]string, float64) {
	// считаем, какая будет сумма, если вообще ничего не добавлять
	calculate.Default_values = nil
	defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
	var defValues map[string]float64
	json.Unmarshal([]byte(defValuesString), &defValues)
	defSum := 0.0
	currentCost := 0
	for _, v := range defValues {
		defSum += v
	}
	var res_plan []string
	res_plan = iteration_plan
	res_value := defSum

	//считаем текущую стоимость итерации
	node_names := calculate.GetNodeNames(st.Method_id)
	for _, i := range iteration_plan {
		costI := 1000
		if v, ok := st.Costs[node_names[i]]; ok {
			costI = v
		}
		if v, ok := st.Costs[i]; ok {
			costI = v
		}
		currentCost += costI
	}
	fmt.Println("current cost is ", currentCost, ", value is ", defSum, ", num of tasks is ", len(iteration_plan))

	//вычисляем результат применения каждой возможной галочки
	select_next_res := SelectNextInternals(st, false)
	select_next_keys := make([]string, 0, len(select_next_res))
	for k := range select_next_res {
		select_next_keys = append(select_next_keys, k)
		fmt.Println(k, " = ", select_next_res[k]["sum"])
	}

	//сортируем по эффективность / часы
	sort.Slice(select_next_keys, func(i int, j int) bool {
		costI := 1000
		costJ := 1000
		if v, ok := st.Costs[select_next_keys[i]]; ok {
			costI = v
		}
		if v, ok := st.Costs[node_names[select_next_keys[i]]]; ok {
			costI = v
		}
		if v, ok := st.Costs[select_next_keys[j]]; ok {
			costJ = v
		}
		if v, ok := st.Costs[node_names[select_next_keys[j]]]; ok {
			costJ = v
		}
		var valI, valJ float64
		valI = (select_next_res[select_next_keys[i]]["sum"] - defSum) / float64(costI)
		valJ = (select_next_res[select_next_keys[j]]["sum"] - defSum) / float64(costJ)
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

		//если очередная галочка не пробивает лимит стоимости, вызываем расчёт с добавлением неё
		costK := 1000
		if v, ok := st.Costs[node_names[select_next_keys[k]]]; ok {
			costK = v
		}
		if v, ok := st.Costs[select_next_keys[k]]; ok {
			costK = v
		}
		fmt.Println("getting ", select_next_keys[k], " costing ", costK, " and giving +", select_next_res[select_next_keys[k]]["sum"]-defSum)
		if currentCost+costK < st.IterLength {
			if _, ok := st.Data_dict[select_next_keys[k]]; ok {
				//для обычных галочек это означает, что мы добавляем галочку в data_dict
				data_dict_copy := make(map[string][]bool)
				for i2, v2 := range st.Data_dict {
					for _, v3 := range v2 {
						data_dict_copy[i2] = append(data_dict_copy[i2], v3)
					}
				}
				data_dict_copy[select_next_keys[k]][int(st.Iter)] = true
				st_copy.Data_dict = data_dict_copy
				st_copy.Data_add_dict = st.Data_add_dict
				iteration_plan = append(iteration_plan, select_next_keys[k])
			} else {
				fmt.Println("k=", k, " ", select_next_keys[k], " is not in Data_dict")
				//для галочек из add я пока не придумал
				continue
			}

			// делаем расчёт оптимального плана с проставленной галочкой
			new_res_plan, new_res_value := PlanIterationInternalsGreedy(st_copy, iteration_plan)

			// если новый план даёт больший результат, выбираем его
			if new_res_value > res_value {
				res_plan = new_res_plan
				res_value = new_res_value
			}
			recursionCalled = true
		} else {
			fmt.Println("skipping, since it's too long ", currentCost+costK, ">", st.IterLength)
		}

		// просматриваем только лучший вариант (жадный алгоритм)
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
		costI := 1000
		if v, ok := st.Costs[node_names[i]]; ok {
			costI = v
		}
		if v, ok := st.Costs[i]; ok {
			costI = v
		}
		currentCost += costI
	}

	recursion_called := false
	met_last_one := len(iteration_plan) == 0
	var best_plan []string
	best_value := 0.0
	//перебираем все галочки...
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
		if v, ok := st.Costs[node_names[i]]; ok {
			costK = v
		}
		if v, ok := st.Costs[i]; ok {
			costK = v
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
		fmt.Println("calling subtree for ", i)
		new_res_plan, new_res_value := PlanIterationInternalsTree(st_copy, iteration_plan_copy)

		//если это поддерево оказалось лучше предыдущих, обновляем оптимумы
		if new_res_value > best_value {
			fmt.Println(i, " becomes the best subtree!")
			best_plan = new_res_plan
			best_value = new_res_value
		}
		recursion_called = true
	}

	for _, v := range st.Data_add_dict {
		for i0 := range v {
			for i1, v1 := range v[i0] {
				//Пока не придумал
				i1 = i1
				v1 = v1
				continue
			}
		}
	}

	//если ни одно поддерево не было использовано
	if !recursion_called {
		fmt.Println("no subtree found, getting result from here")
		calculate.Default_values = nil
		defValuesString, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id)
		var defValues map[string]float64
		json.Unmarshal([]byte(defValuesString), &defValues)
		defSum := 0.0
		for i, v := range defValues {
			costI := 1000
			if v2, ok := st.Costs[node_names[i]]; ok {
				costI = v2
			}
			if v2, ok := st.Costs[i]; ok {
				costI = v2
			}
			defSum += v * float64(costI)
		}
		best_plan = iteration_plan
		best_value = defSum
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
	for _, v := range defValues {
		defSum += v
	}
	node_names := calculate.GetNodeNames(st.Method_id)

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
		if v, ok := st.Costs[node_names[select_next_keys[i-1]]]; ok {
			costI = v
		}
		if v, ok := st.Costs[select_next_keys[i-1]]; ok {
			costI = v
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
	for _, i := range plans[len(select_next_keys)][st.IterLength] {
		data_dict_copy[i][int(st.Iter)] = true
	}
	calculate.Default_values = nil
	final_res_string, _, _ := calculate.StartCalculate(int(st.Iter), 1, st.Threshold, data_dict_copy, st.Data_add_dict, st.Weight, st.Method_id)
	var final_res map[string]float64
	json.Unmarshal([]byte(final_res_string), &final_res)
	res_value := 0.0
	for _, v := range final_res {
		res_value += v
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
		if v[int(st.Iter)] == true {
			continue
		}

		data_dict_copy := make(map[string][]bool)
		for i2, v2 := range st.Data_dict {
			for _, v3 := range v2 {
				data_dict_copy[i2] = append(data_dict_copy[i2], v3)
			}
		}
		data_dict_copy[i][int(st.Iter)] = true
		wg.Add(1)
		go calculate.StartCalculateWrapper(int(st.Iter), 1, st.Threshold, data_dict_copy, st.Data_add_dict, st.Weight, st.Method_id, &wg, &res, i, defValues, false)
	}

	for _, v := range st.Data_add_dict {
		for i0 := range v {
			for i1, v1 := range v[i0] {
				if len(v1) < int(st.Iter) {
					continue
				}
				_, ok := v1[int(st.Iter)][fmt.Sprint(st.Iter)]
				if !ok {
					continue
				}
				if v1[int(st.Iter)][fmt.Sprint(st.Iter)] == true {
					continue
				}
				wg.Add(1)
				go calculate.StartCalculateWrapper(int(st.Iter), 1, st.Threshold, st.Data_dict, st.Data_add_dict, st.Weight, st.Method_id, &wg, &res, i1, defValues, true)
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
		costI := 1000
		if v, ok := st.Costs[node_names[i]]; ok {
			costI = v
		}
		if v, ok := st.Costs[i]; ok {
			costI = v
		}
		for i2, v2 := range v {
			sum += v2 * float64(costI)
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
