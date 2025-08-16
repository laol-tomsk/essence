import HTTP_API_neo4j
import json


API = HTTP_API_neo4j.API("prectice4")
with open("calculate_order.json") as f:
    JSON_MAS = json.load(f)

mas_f = []
for mas in JSON_MAS['mas']:
    mas_t = []
    for node_guid in mas:
        mas_t.append(API.get_node_name(node_guid))

    mas_f.append(mas_t)


with open("calculate_name_order.json",'w') as f:
    f.write(json.dumps({'mas':mas_f}, indent = 4))