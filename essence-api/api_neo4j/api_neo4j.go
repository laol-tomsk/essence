package api_neo4j

import (
	"fmt"
	"reflect"
	"slices"

	"github.com/neo4j/neo4j-go-driver/v4/neo4j"
)

var (
	Ndriver, _         = neo4j.NewDriver("bolt://localhost:7687", neo4j.BasicAuth("laoltomsk", "Nerybov1999", ""))
	stateNodes         []string
	normalVStateNodes  []string
	normalVDetailNodes []string
	nodeParents        map[string][]string
	nodeMngrOpinions   map[string][]string
	nodeNormalParents  map[string][]string
	nodeNormalValues   map[string]string
	nodeStatParents    map[string][]string
	degreesOfEvidence  map[string]string
	typesOfEvidence    map[string]string
	Prepared           = false

	USE_CACHING    = true
	USE_KEY_ALPHAS = true
)

func getNormalValueWithKeyAlphas(guid string, iter int) int {
	switch guid {
	case "9669ac6f-3798-4684-9ffa-4ab3db53fd02",
		"7421aaff-bacc-47ad-8fc6-c9178b9756e8",
		"956382a7-db5a-4c33-ab5f-1f9e59541916",
		"430e9c6e-fb84-40ba-b5d8-4c1a2fce4fe3",
		"2f3214d2-a9ed-4488-b9c1-c90bb46f6422",
		"b4399eef-5354-4b76-bc1a-58db88c2a849",
		"52968b7c-376a-4e30-880b-ca26cb6c864c",
		"fecc147e-62ac-4fa3-8ab9-217732921ad2",
		"75d28666-3cc5-4604-b9d5-4bfa8e150933",
		"edb5ae76-30e8-46a3-9691-81733a1f56ac",
		"6876abc4-e33e-4a0e-a2ed-b0da657e7161",
		"f792b7cc-d083-4b2a-8736-5df1070f0dfc",
		"b3f0c040-51fb-4701-afac-f076961545dd",
		"b3f53e52-87b9-4dcf-9ea7-4603962cb330",
		"b409fa6d-6503-44ac-9132-481c5b897e07",
		"50c8fd86-3616-4fb6-aa68-ef4b5c2a667d",
		"53e6fa3f-21de-4b3b-9cc7-c491301379cc",
		"5b6ec90a-a89b-4c40-b3d2-bf52c9beb1e8":
		return 1
	case "471a0418-4e18-4ec3-acdf-ebeb2ee04a94",
		"912d1482-7791-4a1b-ae34-813e29ad6ec4",
		"d5e13b48-2151-4b63-ba93-44c52e51d26f",
		"ddb7be24-4999-44cc-b24d-308ea418fe2a",
		"4a1abaaa-3ff5-44c4-a84a-3ba0db1f5d6e",
		"5af6a066-2817-46a1-8584-8fc487d0d170":
		return 3
	case "e468875b-08d3-48eb-b60e-e5f7c78f2c5c",
		"7832df2f-b598-435d-a673-39eb566724a8",
		"9b70bbc7-6bd7-4cc4-a7ba-9982c082b17b",
		"0f2e3911-2052-4dee-a5f3-64b1eee7b143",
		"5be9300a-7dcf-4030-8cdc-06bec9db74fe",
		"1ade43d2-b5f5-4d4c-a393-73a0d878441b",
		"f1c5d74a-dca3-4e6c-9986-c0974503e840",
		"30110a95-a34e-4a0a-8252-586e2f0f2128",
		"b0c68aa1-bb09-407c-8e72-cc45084fb86a",
		"6aa6a68a-23bd-4ef4-894d-86168dc35044",
		"b4437f35-0ada-432c-9b86-3bdabdd61651",
		"06f5f227-e6bf-4261-aadc-474a044ca682",
		"4b3523a6-621e-4555-964c-33af0b53f129",
		"c4a44898-a338-462f-b36a-1a6a26234c8b",
		"9f912430-a1be-4efc-a991-db522fe79a44",
		"0f50f4c4-f686-40c2-8469-afab4f9a6699",
		"da7014ec-f34e-4dfe-a53c-8ce1974666e3",
		"7db4cd75-3e99-4d41-8c7f-23b86d877675",
		"200702ef-625d-4950-957d-1eb98ea9a0bf":
		return 4
	case "01976636-25be-4415-9afe-a861303a3573",
		"db67e149-8281-4bc6-a3df-0a133bca4f72",
		"d153cbe3-0548-41aa-bcfb-7790f6b31d8f",
		"15b9bb75-b53d-4d8e-8a2c-e476687837f7",
		"031bc819-4626-4475-aa28-5bd14a07a3ad",
		"51e3aa27-0e53-4267-a118-b12f7de686b3",
		"ae0bf213-711f-4d3c-b9f5-6141f1d6a533",
		"a6830b9f-b541-4309-bf96-2931326a4b0b",
		"e1d1caff-b28b-4554-bdaa-7477e80b5454",
		"177f5ab8-2321-4b93-b5c4-50b7ee42ac38",
		"b34baab4-9777-4439-bd1d-2c1658ddc4d7",
		"99f68c7d-1107-4b0d-bda5-4d381a46b54d",
		"7bd9a917-d103-43e1-8042-6add24c2e352":
		return 6
	case "4867dfdf-ec76-49f0-a911-3eaea4ce4871",
		"9d1118d6-da9c-4cd2-b30e-c71e9e66fb26",
		"f4ca8e67-c0bf-4828-a771-8d4a85a8e41f":
		return 40
	default:
		return -1
	}
}

func Prepare(session neo4j.Session) error {
	if !USE_CACHING {
		return nil
	}

	if Prepared {
		return nil
	}

	fmt.Println("PREPARE CALLED")

	_, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n:state) RETURN n.guid", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			stateNodes = append(stateNodes, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return nil, nil
	})
	if err != nil {
		return err
	}

	_, err = session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n:normalVState) RETURN n.guid", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			normalVStateNodes = append(normalVStateNodes, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return nil, nil
	})
	if err != nil {
		return err
	}

	_, err = session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n:normalVDetail) RETURN n.guid", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			normalVDetailNodes = append(normalVDetailNodes, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return nil, nil
	})
	if err != nil {
		return err
	}

	nodeParents = make(map[string][]string)
	nodeMngrOpinions = make(map[string][]string)
	nodeNormalParents = make(map[string][]string)
	nodeStatParents = make(map[string][]string)
	_, err = session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (p)-[]->(n) RETURN n.guid, p.guid, p:ManagerOpinionCheckpoint, p:normalVDetail, p:normalVState, p:checkpoint, p:state", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			childGuid := result.Record().Values[0].(string)
			parentGuid := result.Record().Values[1].(string)
			isManagerOpinion := result.Record().Values[2].(bool)
			isNormalVDetail := result.Record().Values[3].(bool)
			isNormalVState := result.Record().Values[4].(bool)
			isCheckpoint := result.Record().Values[5].(bool)
			isState := result.Record().Values[6].(bool)
			if _, ok := nodeParents[childGuid]; ok {
				nodeParents[childGuid] = append(nodeParents[childGuid], parentGuid)
			} else {
				nodeParents[childGuid] = []string{parentGuid}
			}
			if isManagerOpinion {
				if _, ok := nodeMngrOpinions[childGuid]; ok {
					nodeMngrOpinions[childGuid] = append(nodeMngrOpinions[childGuid], parentGuid)
				} else {
					nodeMngrOpinions[childGuid] = []string{parentGuid}
				}
			}
			if isNormalVDetail || isNormalVState {
				if _, ok := nodeNormalParents[childGuid]; ok {
					nodeNormalParents[childGuid] = append(nodeNormalParents[childGuid], parentGuid)
				} else {
					nodeNormalParents[childGuid] = []string{parentGuid}
				}
			}
			if isCheckpoint || isState {
				if _, ok := nodeStatParents[childGuid]; ok {
					nodeStatParents[childGuid] = append(nodeStatParents[childGuid], parentGuid)
				} else {
					nodeStatParents[childGuid] = []string{parentGuid}
				}
			}
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return nil, nil
	})
	if err != nil {
		return err
	}

	if USE_KEY_ALPHAS == false {
		nodeNormalValues = make(map[string]string)
		_, err = session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
			result, err := tx.Run("MATCH (n) WHERE n.normalValue IS NOT NULL RETURN n.guid, n.normalValue", nil)
			if err != nil {
				return nil, err
			}
			for result.Next() {
				guid := result.Record().Values[0].(string)
				value := result.Record().Values[1].(string)
				nodeNormalValues[guid] = value
			}
			if err = result.Err(); err != nil {
				return nil, err
			}
			return nil, nil
		})
		if err != nil {
			return err
		}
	}

	degreesOfEvidence = make(map[string]string)
	typesOfEvidence = make(map[string]string)
	_, err = session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (p)-[s]->(n) RETURN p.guid, n.guid, s.degreeOfEvidenceEnumValue, s.typeOfEvidence", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			p_guid := result.Record().Values[0].(string)
			n_guid := result.Record().Values[1].(string)
			degree := result.Record().Values[2]
			evtype := result.Record().Values[3]
			if degree != nil {
				degreesOfEvidence[p_guid+n_guid] = degree.(string)
			}
			if evtype != nil {
				if reflect.TypeOf(evtype).Kind() == reflect.Bool {
					evtypeBool := evtype.(bool)
					if evtypeBool {
						typesOfEvidence[p_guid+n_guid] = "True"
					} else {
						typesOfEvidence[p_guid+n_guid] = "False"
					}
				} else {
					typesOfEvidence[p_guid+n_guid] = evtype.(string)
				}
			}
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return nil, nil
	})
	if err != nil {
		return err
	}

	Prepared = true
	return nil
}

func Get_guid_all_nodes(session neo4j.Session) ([]string, error) {
	mas_name, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		result, err := tx.Run("MATCH (n) RETURN n.guid", nil)
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_name.([]string), err
}

func Get_node_name(session neo4j.Session, guid string) (string, error) {
	node, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n) WHERE n.guid = $guid RETURN n.name", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return "", err
	}
	//time.Sleep(time.Microsecond)
	if node == nil {
		return guid, nil
	}
	return node.(string), nil
}

func Get_node_manager_opinion(session neo4j.Session, guid string) (string, error) {
	if _, ok := nodeMngrOpinions[guid]; ok {
		return nodeMngrOpinions[guid][0], nil
	}

	node, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (y:ManagerOpinionCheckpoint)-[]->(n) WHERE n.guid = $guid RETURN y.guid", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return "", err
	}
	if node == nil {
		return "", nil
	}
	//time.Sleep(time.Microsecond)
	return node.(string), nil
}

func Get_node_children(session neo4j.Session, guid string) ([]string, error) {
	parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		result, err := tx.Run("MATCH (n)-[]->(p) WHERE n.guid = $guid RETURN p.guid", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		//list[0], list[1] = list[1], list[0]
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return parents.([]string), nil
}

func Get_node_parents(session neo4j.Session, guid string) ([]string, error) {
	if _, ok := nodeParents[guid]; ok {
		return nodeParents[guid], nil
	}

	parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		result, err := tx.Run("MATCH (p)-[]->(n) WHERE n.guid = $guid RETURN p.guid", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		//list[0], list[1] = list[1], list[0]
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return parents.([]string), nil
}

func Get_count_normal(session neo4j.Session) int64 {
	counter := 0
	session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, _ := tx.Run("MATCH (p:normalVState)-[]->(:checkpoint) RETURN DISTINCT n.guid", map[string]interface{}{})
		for result.Next() {
			counter++
		}
		result, _ = tx.Run("MATCH (p:normalVDetail)-[]->(:checkpoint) RETURN DISTINCT n.guid", map[string]interface{}{})
		for result.Next() {
			counter++
		}

		return counter, nil
	})

	return 0
}

func Get_node_parents_labels(session neo4j.Session, guid string, label string) ([]string, error) {
	mas_parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		request := "MATCH (p:" + label + ")-[]->(n) WHERE n.guid = $guid RETURN p.guid"
		result, err := tx.Run(request, map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_parents.([]string), nil
}

func Get_mas_normal_parents(session neo4j.Session, guid string) ([]string, error) {
	if _, ok := nodeNormalParents[guid]; ok {
		return nodeNormalParents[guid], nil
	}

	mas_parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		request := "MATCH (p:normalVDetail)-[]->(n) WHERE n.guid = $guid RETURN p.guid"
		result, err := tx.Run(request, map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		request = "MATCH (p:normalVState)-[]->(n) WHERE n.guid = $guid RETURN p.guid"
		result, err = tx.Run(request, map[string]interface{}{
			"guid": guid,
		})
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_parents.([]string), nil
}

func Get_mas_normal_parents_concrect_projectr(session neo4j.Session) ([]string, error) {
	mas_parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		request := "MATCH (p:normalVDetail)-[]->(:checkpoint) RETURN DISTINCT p.guid"
		result, err := tx.Run(request, map[string]interface{}{})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		request = "MATCH (p:normalVState)-[]->(:checkpoint) RETURN DISTINCT p.guid"
		result, err = tx.Run(request, map[string]interface{}{})
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_parents.([]string), nil
}

func Get_mas_stat_parents(session neo4j.Session, guid string) ([]string, error) {
	if _, ok := nodeStatParents[guid]; ok {
		return nodeStatParents[guid], nil
	}

	mas_parents, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		request := "MATCH (p:checkpoint)-[]->(n) WHERE n.guid = $guid RETURN p.guid"
		result, err := tx.Run(request, map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		request = "MATCH (p:state)-[]->(n) WHERE n.guid = $guid RETURN p.guid"
		result, err = tx.Run(request, map[string]interface{}{
			"guid": guid,
		})
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_parents.([]string), nil
}

func Has_label_node(session neo4j.Session, guid string, label string) (bool, error) {
	if USE_CACHING {
		if label == "state" {
			return slices.Contains(stateNodes, guid), nil
		}
		if label == "normalVState" {
			return slices.Contains(normalVStateNodes, guid), nil
		}
		if label == "normalVDetail" {
			return slices.Contains(normalVDetailNodes, guid), nil
		}
	}
	node, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n) WHERE n.guid = $guid RETURN labels(n)", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return true, err
	}
	//time.Sleep(time.Microsecond)
	if node.([]interface{})[0] == label {
		return true, nil
	} else {
		return false, nil
	}
}

func GetSession(method_id int64) neo4j.Session {
	session := Ndriver.NewSession(neo4j.SessionConfig{
		AccessMode:   neo4j.AccessModeRead,
		DatabaseName: "prectice" + fmt.Sprint(method_id),
		FetchSize:    neo4j.FetchAll,
	})

	return session
}

func Get_type_influence_node(session neo4j.Session, guid_parent string, guid_node string) (bool, error) {
	if _, ok := typesOfEvidence[guid_parent+guid_node]; ok {
		return typesOfEvidence[guid_parent+guid_node] == "True", nil
	}

	node, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (p)-[s]->(n) WHERE p.guid = $guid_p AND n.guid = $guid_n RETURN s.typeOfEvidence", map[string]interface{}{
			"guid_p": guid_parent,
			"guid_n": guid_node,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return true, err
	}
	//time.Sleep(time.Microsecond)
	if node.(string) == "True" {
		return true, nil
	} else {
		return false, nil
	}
}

func Get_degree_influence_node(session neo4j.Session, guid_parent string, guid_node string) (string, error) {
	if _, ok := degreesOfEvidence[guid_parent+guid_node]; ok {
		return degreesOfEvidence[guid_parent+guid_node], nil
	}

	influence, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (p)-[s]->(n) WHERE p.guid = $guid_p AND n.guid = $guid_n RETURN s.degreeOfEvidenceEnumValue", map[string]interface{}{
			"guid_p": guid_parent,
			"guid_n": guid_node,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return "", err
	}
	if influence != nil {
		return influence.(string), nil
	}
	return "", nil
}

func Get_normalValue_node(session neo4j.Session, guid string, iteration int) (string, error) {

	if USE_KEY_ALPHAS {
		return fmt.Sprint(getNormalValueWithKeyAlphas(guid, iteration)), nil
	}

	if _, ok := nodeNormalValues[guid]; ok {
		return nodeNormalValues[guid], nil
	}

	normal, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		result, err := tx.Run("MATCH (n) WHERE n.guid = $guid RETURN n.normalValue", map[string]interface{}{
			"guid": guid,
		})
		if err != nil {
			return nil, err
		}
		if result.Next() {
			return result.Record().Values[0], nil
		}
		return nil, err
	})
	if err != nil {
		return "", err
	}
	//time.Sleep(time.Microsecond)
	return normal.(string), nil
}

func Get_all_nodes_the_label(session neo4j.Session, label string) ([]string, error) {

	mas_name, err := session.ReadTransaction(func(tx neo4j.Transaction) (interface{}, error) {
		var list []string
		request := "MATCH (n:" + label + ") RETURN n.guid"
		result, err := tx.Run(request, map[string]interface{}{})
		if err != nil {
			return nil, err
		}
		for result.Next() {
			list = append(list, result.Record().Values[0].(string))
		}
		if err = result.Err(); err != nil {
			return nil, err
		}
		return list, nil
	})
	if err != nil {
		return nil, err
	}
	//time.Sleep(time.Microsecond)
	return mas_name.([]string), err
}
