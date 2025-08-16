package api_neo4j

import (
	"fmt"
	"reflect"
	"slices"
	"time"

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
)

func track(msg string) (string, time.Time) {
	return msg, time.Now()
}

func duration(msg string, start time.Time) {
	fmt.Printf("%v: %v\n", msg, time.Since(start))
}

func Prepare(session neo4j.Session) error {
	defer duration(track("preapre"))

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
			isCheckpoint := result.Record().Values[3].(bool)
			isState := result.Record().Values[4].(bool)
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
	if label == "state" {
		return slices.Contains(stateNodes, guid), nil
	}
	if label == "normalVState" {
		return slices.Contains(normalVStateNodes, guid), nil
	}
	if label == "normalVDetail" {
		return slices.Contains(normalVDetailNodes, guid), nil
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
	//time.Sleep(time.Microsecond)
	return influence.(string), nil
}

func Get_normalValue_node(session neo4j.Session, guid string) (string, error) {
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
