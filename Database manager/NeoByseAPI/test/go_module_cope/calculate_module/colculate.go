package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"math"
	"strconv"
	"strings"
	"time"

	"api_module/apineo4j"
)

var (
	Labels                                                       = newLabelsRegister()
	Iteration, ProjectScale, Threshold, Data_dict, Data_add_dict = pars_argument()
	Mas_calculate_nodes                                          []string
	Dict_node_name                                               map[string]string
	Mas_dictionary_nodes                                         []map[string]float64
	Turn                                                         []string
)

func newLabelsRegister() *LabelsNodesEnum {
	return &LabelsNodesEnum{
		state:                    "state",
		normalVState:             "normalVState",
		checkpoint:               "checkpoint",
		ManagerOpinionCheckpoint: "ManagerOpinionCheckpoint",
		normalVDetail:            "normalVDetail",
		Addcheckpoint:            "Addcheckpoint",
		DynamicCheckpoint:        "DynamicCheckpoint",
	}
}

type LabelsNodesEnum struct {
	state                    string
	normalVState             string
	checkpoint               string
	ManagerOpinionCheckpoint string
	normalVDetail            string
	Addcheckpoint            string
	DynamicCheckpoint        string
}

func get_mas_dictionary(session neo4j.Session) ([]string, []map[string]float64, map[string]string) {
	mas_checkpoint, err := get_all_nodes_the_label(session, Labels.checkpoint)

	if err != nil {
		panic(err)
	}

	mas_state, err := get_all_nodes_the_label(session, Labels.state)

	if err != nil {
		panic(err)
	}
	var Mas_calculate_nodes []string
	Mas_calculate_nodes = append(Mas_calculate_nodes, mas_checkpoint...)
	Mas_calculate_nodes = append(Mas_calculate_nodes, mas_state...)

	//fmt.Println(Mas_calculate_nodes)
	var Mas_dictionary_nodes []map[string]float64
	_dict := make(map[string]float64)
	_dict_zero := make(map[string]float64)
	Dict_node_name := make(map[string]string)

	for _, node_guid := range Mas_calculate_nodes {
		_dict[node_guid] = -1
		_dict_zero[node_guid] = 0
		name_node, err := get_node_name(session, node_guid)
		if err != nil {
			panic(err)
		}
		Dict_node_name[node_guid] = name_node
	}

	for i := 0; i < Iteration; i++ {
		if i == 0 {
			Mas_dictionary_nodes = append(Mas_dictionary_nodes, _dict_zero)
		}
		newMap := make(map[string]float64)
		for key, value := range _dict {
			newMap[key] = value
		}
		Mas_dictionary_nodes = append(Mas_dictionary_nodes, newMap)
	}

	return Mas_calculate_nodes, Mas_dictionary_nodes, Dict_node_name
}
func main() {
	fmt.Println("START")
	dbUri := "neo4j://localhost:7687"
	Ndriver, err := neo4j.NewDriver(dbUri, neo4j.BasicAuth("Golang", "contrelspawn123", ""))
	if err != nil {
		panic(err)
	}
	defer Ndriver.Close()
	session := Ndriver.NewSession(neo4j.SessionConfig{
		AccessMode:   neo4j.AccessModeRead,
		DatabaseName: "prectice",
	})
	defer session.Close()

	//fmt.Println(Iteration)
	//fmt.Println(ProjectScale)
	//fmt.Println(Data_dict)
	//fmt.Println(Data_add_dict)

	Mas_calculate_nodes, Mas_dictionary_nodes, Dict_node_name = get_mas_dictionary(session)

	calculate_all(session)
}

func track(msg string) (string, time.Time) {
	return msg, time.Now()
}

func duration(msg string, start time.Time) {
	fmt.Printf("%v: %v\n", msg, time.Since(start))
}

func stringInSlice(a string, list []string) bool {
	for _, b := range list {
		if b == a {
			return true
		}
	}
	return false
}

func return_N_on_state(parent_guid string, number_iteretion int) int {
	counter := 0
	for _, copy_state := range Data_add_dict[parent_guid] {
		counter_flag := 0
		for key := range copy_state {
			for _, iter := range copy_state[key] {
				flag, ok := iter[fmt.Sprint(number_iteretion)]
				if ok {
					if flag {
						counter_flag += 1
						break
					}
				}
			}
		}
		if counter_flag == len(copy_state) {
			counter += 1
		}
	}
	return counter
}

func return_N_on_details(parent_guid string, number_iteretion int) int {
	counter := 0
	for _, copy_datail := range Data_add_dict[parent_guid] {
		for key := range copy_datail {
			flag := false
			for _, iter := range copy_datail[key] {
				flag, ok := iter[fmt.Sprint(number_iteretion)]
				if ok {
					if flag {
						counter += 1
						flag = false
						break
					}
				}
			}
			if flag {
				break
			}
		}
	}
	return counter
}

func calculete_node(session neo4j.Session, node_guid string, number_iteration int) {
	if !stringInSlice(node_guid, Turn) {
		Turn = append(Turn, node_guid)
		mas_parents, err := get_node_parents(session, node_guid)
		if err != nil {
			panic(err)
		}
		for _, parent := range mas_parents {
			flag1, err := has_label_node(session, parent, Labels.checkpoint)
			if err != nil {
				panic(err)
			}
			flag2, err := has_label_node(session, parent, Labels.state)
			if err != nil {
				panic(err)
			}
			if flag1 || flag2 {
				if Mas_dictionary_nodes[number_iteration][parent] == -1 {
					calculete_node(session, parent, number_iteration)
				}
			}
		}

		/*for true {
			counter_parent := 0
			for _, parent := range mas_parents {
				if Mas_dictionary_nodes[number_iteration][parent] != -1 {
					counter_parent += 1
				}
			}
			if counter_parent == len(mas_parents) {
				break
			}
		}*/

		flag, err := has_label_node(session, node_guid, Labels.state)
		if err != nil {
			panic(err)
		}
		if flag {
			rezult := 1.0
			for _, parent := range mas_parents {
				rezult *= Mas_dictionary_nodes[number_iteration][parent]
			}
			Mas_dictionary_nodes[number_iteration][node_guid] = rezult
			return
		}

		fmt.Println(Dict_node_name[node_guid])
		Lbase := 1.0
		Rbase := 1.0
		degree_influence, err := get_degree_influence_node(session, mas_parents[0], node_guid)
		if err != nil {
			panic(err)
		}
		//fmt.Println(Data_dict[node_guid][number_iteration])
		if Data_dict[node_guid][number_iteration] {
			Lbase *= math.Pow(2, float64(degree_influence))
		} else {
			Rbase *= math.Pow(2, float64(degree_influence))
		}

		//fmt.Printf("%s -> Lbase:%f, Rbase:%f\n", Dict_node_name[node_guid], Lbase, Rbase)
		mas_normalDetail_parent, err := get_node_parents_labels(session, node_guid, Labels.normalVDetail)
		if err != nil {
			panic(err)
		}
		mas_normalState_parent, err := get_node_parents_labels(session, node_guid, Labels.normalVState)
		if err != nil {
			panic(err)
		}
		var mas_normal_parent []string
		mas_normal_parent = append(mas_normal_parent, mas_normalState_parent...)
		mas_normal_parent = append(mas_normal_parent, mas_normalDetail_parent...)
		//fmt.Println(len(mas_normal_parent))
		if len(mas_normal_parent) > 0 {
			for _, parent_guid := range mas_normal_parent {
				K := 1.0
				flagState, err := has_label_node(session, parent_guid, Labels.normalVState)
				if err != nil {
					panic(err)
				}
				flagDetail, err := has_label_node(session, parent_guid, Labels.normalVDetail)
				if err != nil {
					panic(err)
				}
				var N int
				if flagState {
					N = return_N_on_state(parent_guid, number_iteration)
				} else if flagDetail {
					N = return_N_on_details(parent_guid, number_iteration)
				}
				normal, err := get_normalValue_node(session, parent_guid)
				if err != nil {
					panic(err)
				}
				normal_int, err := strconv.Atoi(normal)
				if err != nil {
					panic(err)
				}
				Z := normal_int * ProjectScale
				_type, err := get_type_influence_node(session, parent_guid, node_guid)
				if err != nil {
					panic(err)
				}
				degree_influence_node, err := get_degree_influence_node(session, parent_guid, node_guid)
				if err != nil {
					panic(err)
				}
				if _type {
					if N == 0 {
						K = 1
						Rbase *= K * math.Pow(2, float64(degree_influence_node))
					} else if N >= Z {
						K = Log(float64(N), float64(Z))
						Lbase *= K * math.Pow(2, float64(degree_influence_node))
					} else if N < Z {
						K = float64(1 - (1 / (1 + N)))
						Rbase *= (1 - K) * math.Pow(2, float64(degree_influence_node))
						Lbase *= K * math.Pow(2, float64(degree_influence_node))
					}
				} else {
					if N == 0 {

					} else if N >= Z {
						K = Log(float64(N), float64(Z))
						Rbase *= K * math.Pow(2, float64(degree_influence_node))
					} else if N < Z {
						K = float64(N / Z)
						Rbase *= K * math.Pow(2, float64(degree_influence_node))
					}
				}
			}
		}
		var mas_stat_parents []string
		mas_parent_checkpoint, err := get_node_parents_labels(session, node_guid, Labels.checkpoint)
		if err != nil {
			panic(err)
		}
		mas_parent_state, err := get_node_parents_labels(session, node_guid, Labels.state)
		if err != nil {
			panic(err)
		}
		mas_parent_dynamic, err := get_node_parents_labels(session, node_guid, Labels.DynamicCheckpoint)
		if err != nil {
			panic(err)
		}
		mas_stat_parents = append(mas_stat_parents, mas_parent_checkpoint...)
		mas_stat_parents = append(mas_stat_parents, mas_parent_dynamic...)
		mas_stat_parents = append(mas_stat_parents, mas_parent_state...)

		vector_parents := create_vector(len(mas_stat_parents))
		//fmt.Println(vector_parents)
		//fmt.Printf("%s -> Lbase:%f, Rbase:%f\n", Dict_node_name[node_guid], Lbase, Rbase)
		//var XChan []float64
		//var tokens = make(chan struct{}, 1)
		var mas_degree []int64
		for _, parent := range mas_stat_parents {
			degree, err := get_degree_influence_node(session, parent, node_guid)
			if err != nil {
				panic(err)
			}
			mas_degree = append(mas_degree, degree)
		}
		var mas_bool_labels []bool
		for _, parent := range mas_stat_parents {
			flag, err := has_label_node(session, parent, Labels.DynamicCheckpoint)
			if err != nil {
				panic(err)
			}
			mas_bool_labels = append(mas_bool_labels, flag)
		}
		X := 0.0
		for _, vector := range vector_parents {
			//go func(vector string) {
			//tokens <- struct{}{}
			L := Lbase
			R := Rbase
			Y := 1.0
			for v := 0; v < len(vector); v++ {
				//fmt.Println("TEST")
				if vector[v] == '1' {
					L *= math.Pow(2, float64(mas_degree[v]))
					if mas_bool_labels[v] {
						Y *= Mas_dictionary_nodes[number_iteration-1][node_guid]
					} else {
						Y *= Mas_dictionary_nodes[number_iteration][mas_stat_parents[v]]
					}
				} else {
					R *= math.Pow(2, float64(mas_degree[v]))
					if mas_bool_labels[v] {
						Y *= 1 - Mas_dictionary_nodes[number_iteration-1][node_guid]
					} else {
						Y *= 1 - Mas_dictionary_nodes[number_iteration][mas_stat_parents[v]]
					}
				}

			}
			//<-tokens
			//XChan = append(XChan, (L/(L+R))*Y)
			X += (L / (L + R)) * Y
			//}(vector)
		}
		//X := 0.0
		//for len(XChan) < len(vector_parents) {
		//
		//}
		//for _, item := range XChan {
		//	X += item
		//}
		Mas_dictionary_nodes[number_iteration][node_guid] = X
		fmt.Printf("%s -> %f\n", Dict_node_name[node_guid], Mas_dictionary_nodes[number_iteration][node_guid])
		return
	}
	return
}

func create_vector(len_vector int) []string {
	var mas []string
	for i := 0; i < int(math.Pow(2, float64(len_vector))); i++ {
		bin_value, _ := ConvertInt(fmt.Sprint(i), 10, 2)
		flag := len(bin_value)
		for flag != len_vector {
			bin_value = "0" + bin_value
			flag = len(bin_value)
		}
		mas = append(mas, bin_value)
	}
	return mas
}

func ConvertInt(val string, base, toBase int) (string, error) {
	i, err := strconv.ParseInt(val, base, 64)
	if err != nil {
		return "", err
	}
	return strconv.FormatInt(i, toBase), nil
}

func Log(base, x float64) float64 {
	return math.Log(x) / math.Log(base)
}
func calculate_all(session neo4j.Session) {
	defer duration(track("calculate"))
	final_dict_end := make(map[string]map[string]float64)
	for iter := 0; iter < Iteration; iter++ {
		Turn = nil
		if iter == 0 {
			continue
		}
		fmt.Printf("ITERATION %d\n", iter)
		final_dict := make(map[string]float64)
		for _, node_guid := range Mas_calculate_nodes {
			if !stringInSlice(node_guid, Turn) {
				calculete_node(session, node_guid, iter)
			}
			final_dict[Dict_node_name[node_guid]] = Mas_dictionary_nodes[iter][node_guid]
		}
		final_dict_end[fmt.Sprint(iter)] = final_dict
	}

	//Запись в файл
	fmt.Println(final_dict_end)
	rawDataOut, err := json.MarshalIndent(&final_dict_end, "", "   ")
	if err != nil {
		panic(err)
	}

	err = ioutil.WriteFile("out.json", rawDataOut, 4)
	if err != nil {
		panic(err)
	}

	outPlh, outPlhp := get_plh_and_plhp(session)
	dictOutPlhAndPlhp := make(map[string]interface{})
	dictOutPlhAndPlhp["plh"] = outPlh
	dictOutPlhAndPlhp["plhp"] = outPlhp

	rawDataOutP, err := json.MarshalIndent(&dictOutPlhAndPlhp, "", "   ")
	if err != nil {
		panic(err)
	}

	err = ioutil.WriteFile("outPlhAndPlhp.json", rawDataOutP, 4)
	if err != nil {
		panic(err)
	}

}

func get_plh_and_plhp(session neo4j.Session) ([][]interface{}, map[string]string) {
	var mas_plh [][]interface{}
	dict_plhp := make(map[string]string)

	for _, node := range Mas_calculate_nodes {
		flag, err := has_label_node(session, node, Labels.checkpoint)
		if err != nil {
			panic(err)
		}
		if (Mas_dictionary_nodes[Iteration-1][node] < Threshold) && (flag) {
			mas := []interface{}{Dict_node_name[node], Mas_dictionary_nodes[Iteration-1][node]}
			mas_plh = append(mas_plh, mas)
		}
		var str_parent []string
		mas_parents, err := get_node_parents(session, node)
		if err != nil {
			panic(err)
		}
		for _, parent := range mas_parents {
			flag, err := has_label_node(session, parent, Labels.checkpoint)
			if err != nil {
				panic(err)
			}
			if flag {
				if !Data_dict[parent][Iteration-1] {
					str_parent = append(str_parent, Dict_node_name[parent])
				}
			}
		}
		dict_plhp[Dict_node_name[node]] = strings.Join(str_parent, ",")
	}
	return mas_plh, dict_plhp
}

func pars_argument() (int, int, float64, map[string][]bool, map[string][]map[string][]map[string]bool) {
	type JsonGO struct {
		Iteration     int
		ProjectScale  int
		Threshold     float64
		Data_dict     map[string][]bool
		Data_add_dict map[string][]map[string][]map[string]bool
	}
	var st JsonGO
	byteValue, err := ioutil.ReadFile("API_ARGUMENT.json")
	if err != nil {
		panic(err)
	}
	//var f interface{}
	err = json.Unmarshal(byteValue, &st)
	if err != nil {
		panic(err)
	}
	/*
		m := f.(map[string]interface{})
		mapInterfase1 := m["Data_dict"].(map[string]interface{})
		mapMasBool := make(map[string][]bool)
		for key, value := range mapInterfase1 {
			strKey := key
			masinter := value.([]interface{})
			var mas []bool
			for _, i := range masinter {
				mas = append(mas, i.(bool))
			}

			mapMasBool[strKey] = mas
		}

		return int(m["Iteration"].(float64)), int(m["ProjectScale"].(float64)), mapMasBool, m["Data_add_dict"].(map[string]interface{})
	*/
	return st.Iteration, st.ProjectScale, st.Threshold, st.Data_dict, st.Data_add_dict
}


