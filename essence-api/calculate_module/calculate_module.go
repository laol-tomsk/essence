package calculate_module

import (
	"calculator/api_neo4j"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"math/big"
	"strconv"
	"sync"

	"github.com/neo4j/neo4j-go-driver/v4/neo4j"
)

var (
	Labels               = newLabelsRegister()
	Iteration            int
	ProjectScale         int
	Threshold            float64
	Data_dict            map[string]map[string][]bool
	Data_add_dict        map[string]map[string][]map[string][]map[string]bool
	Weight               map[string]int64
	Dict_node_name       map[string]string
	Mas_dictionary_nodes []map[string]float64
	Turn                 []string
	Default_values       map[string]float64
	Dict_Mutex           = sync.RWMutex{}
)

func newLabelsRegister() *LabelsNodesEnum {
	return &LabelsNodesEnum{
		state:                    "state",
		normalVState:             "normalVState",
		checkpoint:               "checkpoint",
		ManagerOpinionCheckpoint: "ManagerOpinionCheckpoint",
		normalVDetail:            "normalVDetail",
		Addcheckpoint:            "Addcheckpoint",
	}
}

type LabelsNodesEnum struct {
	state                    string
	normalVState             string
	checkpoint               string
	ManagerOpinionCheckpoint string
	normalVDetail            string
	Addcheckpoint            string
}

func get_dict_name_nodes(session neo4j.Session) map[string]string {
	mas_checkpoint, err := api_neo4j.Get_all_nodes_the_label(session, Labels.checkpoint)

	if err != nil {
		log.Fatal(err)
	}

	mas_state, err := api_neo4j.Get_all_nodes_the_label(session, Labels.state)

	if err != nil {
		log.Fatal(err)
	}

	var Mas_calculate_nodes []string
	Mas_calculate_nodes = append(Mas_calculate_nodes, mas_checkpoint...)
	Mas_calculate_nodes = append(Mas_calculate_nodes, mas_state...)

	Dict_node_name := make(map[string]string)

	for _, node_guid := range Mas_calculate_nodes {
		name_node, err := api_neo4j.Get_node_name(session, node_guid)
		if err != nil {
			log.Fatal(err)
		}
		Dict_node_name[node_guid] = name_node
	}

	return Dict_node_name
}

func StartCalculateWrapper(
	iteration int,
	project_scale int,
	threshold float64,
	data_dict map[string][]bool,
	data_add_dict map[string][]map[string][]map[string]bool,
	weight map[string]int64,
	method_id int64,
	wg *sync.WaitGroup,
	res *map[string]map[string]float64,
	i string,
	def_values map[string]float64,
	is_additional bool) {
	//fmt.Println("START")

	if Data_dict == nil {
		Data_dict = make(map[string]map[string][]bool)
	}
	if Data_add_dict == nil {
		Data_add_dict = make(map[string]map[string][]map[string][]map[string]bool)
	}
	if Default_values == nil {
		Default_values = def_values
	}

	Iteration = iteration
	ProjectScale = project_scale
	Threshold = threshold
	Dict_Mutex.Lock()
	Data_dict[i] = data_dict
	Data_add_dict[i] = data_add_dict
	Dict_Mutex.Unlock()
	Weight = weight

	session := api_neo4j.GetSession(method_id)
	defer session.Close()
	defer wg.Done()
	Dict_node_name = get_dict_name_nodes(session)

	res_i, _, _ := calculete_node_no_dynemic(session, Iteration, i, is_additional)
	var res_i_obj map[string]float64
	json.Unmarshal([]byte(res_i), &res_i_obj)
	(*res)[i] = res_i_obj
}

func StartCalculate(
	iteration int,
	project_scale int,
	threshold float64,
	data_dict map[string][]bool,
	data_add_dict map[string][]map[string][]map[string]bool,
	weight map[string]int64,
	method_id int64) (string, string, string) {
	//fmt.Println("START")

	if Data_dict == nil {
		Data_dict = make(map[string]map[string][]bool)
	}
	if Data_add_dict == nil {
		Data_add_dict = make(map[string]map[string][]map[string][]map[string]bool)
	}

	Iteration = iteration
	ProjectScale = project_scale
	Threshold = threshold
	Data_dict["0"] = data_dict
	Data_add_dict["0"] = data_add_dict
	Weight = weight

	session := api_neo4j.GetSession(method_id)
	defer session.Close()
	Dict_node_name = get_dict_name_nodes(session)

	return calculete_node_no_dynemic(session, Iteration, "0", false)
}

func GetNodeNames(method_id int64) map[string]string {
	session := api_neo4j.GetSession(method_id)
	return get_dict_name_nodes(session)
}

func calculete_node_no_dynemic(session neo4j.Session, number_iteration int, data_dict_version string, is_additional bool) (string, string, string) {
	//chanal_lost := logmodule.GetChanal()
	//go logmodule.StartLogs(&chanal_lost, time.Now().Format("2006.01.02 15:04:05"))

	var (
		final_dict      = map[string]float64{}
		final_dictMutex = sync.RWMutex{}
	)
	mas := return_nod_dynemic_data()

	type StructDegree struct {
		Name   string
		Degree float64
	}

	final_dictMutex.Lock()
	var nodes_to_calculate []string
	nodes_to_calculate = make([]string, 0)
	final_dictMutex.Unlock()

	chan_calc := make(chan StructDegree)
	for _, node_guid_mas := range mas {
		for _, node_guid_a := range node_guid_mas {
			final_dictMutex.Lock()
			calculate_really := is_additional || should_be_calculated(session, data_dict_version, node_guid_a, nodes_to_calculate)
			if calculate_really {
				nodes_to_calculate = append(nodes_to_calculate, node_guid_a)
			}
			final_dictMutex.Unlock()
			go func(node_guid string, calculate_really bool) {
				var result_calc StructDegree

				result_calc.Name = Dict_node_name[node_guid]

				if !calculate_really {
					result_calc.Degree = Default_values[Dict_node_name[node_guid]]
					chan_calc <- result_calc
					return
				}

				flag, _ := api_neo4j.Has_label_node(session, node_guid, Labels.state)

				if flag {
					rezult := 0.0
					final_dictMutex.Lock()
					mas_parents, _ := api_neo4j.Get_node_parents(session, node_guid)
					for _, parent := range mas_parents {
						rezult += 1 / final_dict[Dict_node_name[parent]]
					}
					final_dictMutex.Unlock()
					result_calc.Degree = float64(len(mas_parents)) / rezult
					chan_calc <- result_calc
					return
				}

				Lbase := 1.0
				Rbase := 1.0

				guid_manager_opinion, _ := api_neo4j.Get_node_manager_opinion(session, node_guid)
				key_degree_influence, _ := api_neo4j.Get_degree_influence_node(session, guid_manager_opinion, node_guid)

				degree_influence := Weight[key_degree_influence]

				if Data_dict[data_dict_version][node_guid][number_iteration] {
					Lbase *= math.Pow(2, float64(degree_influence))
				} else {
					Rbase *= math.Pow(2, float64(degree_influence)*float64(degree_influence))
				}
				mas_normal_parent, _ := api_neo4j.Get_mas_normal_parents(session, node_guid)

				if len(mas_normal_parent) > 0 {
					for _, parent_guid := range mas_normal_parent {
						var K float64
						flagState, err := api_neo4j.Has_label_node(session, parent_guid, Labels.normalVState)
						if err != nil {
							log.Fatal(err)
						}
						flagDetail, err := api_neo4j.Has_label_node(session, parent_guid, Labels.normalVDetail)
						if err != nil {
							log.Fatal(err)
						}
						var N int
						if flagState {
							N = return_N_on_state(parent_guid, data_dict_version, number_iteration)
						}
						if flagDetail {
							N = return_N_on_details(parent_guid, data_dict_version, number_iteration)
						}
						normal, err := api_neo4j.Get_normalValue_node(session, parent_guid)
						if err != nil {
							log.Fatal(err)
						}
						normal_int, err := strconv.Atoi(normal)
						if err != nil {
							log.Fatal(err)
						}
						Z := normal_int * ProjectScale
						if Z < 2 {
							Z = 2
						}
						_type, err := api_neo4j.Get_type_influence_node(session, parent_guid, node_guid)
						if err != nil {
							log.Fatal(err)
						}
						key_degree_influence_node, err := api_neo4j.Get_degree_influence_node(session, parent_guid, node_guid)
						if err != nil {
							log.Fatal(err)
						}
						degree_influence_node := Weight[key_degree_influence_node]
						if _type {
							if N == 0 {
								Rbase *= math.Pow(2, float64(degree_influence_node))
							} else if N >= Z {
								K = Log(float64(Z), float64(N))
								Lbase *= K * math.Pow(2, float64(degree_influence_node))
							} else if N < Z {
								K = float64(1 - (1 / (1 + N)))
								Rbase *= (1 - K) * math.Pow(2, float64(degree_influence_node))
								Lbase *= K * math.Pow(2, float64(degree_influence_node))
							}
						} else {
							if N == 0 {

							} else if N >= Z {
								K = Log(float64(Z), float64(N))
								Rbase *= K * math.Pow(2, float64(degree_influence_node)*2)
							} else if N < Z {
								K = float64(N / Z)
								Rbase *= K * math.Pow(2, float64(degree_influence_node)*2)
							}
						}
					}
				}
				mas_stat_parents, _ := api_neo4j.Get_mas_stat_parents(session, node_guid)
				if len(mas_stat_parents) == 0 {
					value := Data_dict[data_dict_version][node_guid][number_iteration]
					if value {
						result_calc.Degree = 1
					} else {
						result_calc.Degree = 0
					}
					chan_calc <- result_calc
					return
				}
				vector_parents := create_vector(len(mas_stat_parents))
				var mas_degree []int64
				mas_degree_parent := map[string]float64{}
				for _, parent := range mas_stat_parents {
					key_degree, err := api_neo4j.Get_degree_influence_node(session, parent, node_guid)
					if err != nil {
						log.Fatal(err)
					}
					degree := Weight[key_degree]
					mas_degree = append(mas_degree, degree)
					final_dictMutex.Lock()
					mas_degree_parent[parent] = final_dict[Dict_node_name[parent]]
					final_dictMutex.Unlock()
				}

				X := 0.0
				for _, vector := range vector_parents {
					L := big.NewFloat(Lbase)
					R := big.NewFloat(Rbase)
					Y := big.NewFloat(1)

					for v := 0; v < len(vector); v++ {
						if vector[v] == '1' {
							L.Add(L, big.NewFloat(math.Pow(2, float64(mas_degree[v]))))
							Y.Mul(Y, big.NewFloat(mas_degree_parent[mas_stat_parents[v]]))
						} else {
							R.Add(R, big.NewFloat(math.Pow(2, float64(mas_degree[v]))))
							Y.Mul(Y, big.NewFloat(1-mas_degree_parent[mas_stat_parents[v]]))
						}

					}
					L_end, _ := L.Float64()
					R_end, _ := R.Float64()
					Y_end, _ := Y.Float64()
					X += Y_end * (L_end / (L_end + R_end))
				}

				result_calc.Degree = X
				chan_calc <- result_calc
			}(node_guid_a, calculate_really)
		}
		counter := 0
		for {
			a := <-chan_calc
			final_dictMutex.Lock()
			final_dict[a.Name] = a.Degree
			final_dictMutex.Unlock()
			counter++
			if counter == len(node_guid_mas) {
				break
			}
		}

	}
	f_json, err := json.MarshalIndent(&final_dict, "", "   ")
	if err != nil {
		panic(err)
	}
	//logmodule.StopLogs(&chanal_lost)
	return string(f_json), "", ""
}

func should_be_calculated(session neo4j.Session, goal_node string, node_guid string, nodes_to_calculate []string) bool {
	if goal_node == "0" {
		return true
	}
	if goal_node == node_guid {
		return true
	}

	node_parents, _ := api_neo4j.Get_node_parents(session, node_guid)
	for _, parent := range node_parents {
		for _, node_to_calculate := range nodes_to_calculate {
			if parent == node_to_calculate {
				return true
			}
		}
	}

	return false
}

func return_N_on_state(parent_guid string, data_dict_version string, number_iteretion int) int {
	counter := 0
	is_current_state := false
	for _, copy_state := range Data_add_dict[data_dict_version][parent_guid] {
		counter_flag := 0
		for key := range copy_state {
			if key == data_dict_version {
				is_current_state = true
			}
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
	if is_current_state {
		counter += 1
	}
	return counter
}

func return_N_on_details(parent_guid string, data_dict_version string, number_iteretion int) int {
	counter := 0
	is_current_detail := false
	for _, copy_datail := range Data_add_dict[data_dict_version][parent_guid] {
		for key := range copy_datail {
			if key == data_dict_version {
				is_current_detail = true
			}
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

	if is_current_detail {
		counter += 1
	}
	return counter
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

func return_nod_dynemic_data() [][]string {
	type json_struct struct {
		Mas [][]string
	}

	var st json_struct
	byteValue, err := ioutil.ReadFile("calculate_order.json")
	if err != nil {
		log.Fatal(err)
	}

	err = json.Unmarshal(byteValue, &st)
	if err != nil {
		log.Fatal(err)
	}
	//fmt.Println(st.Mas)
	return st.Mas
}
