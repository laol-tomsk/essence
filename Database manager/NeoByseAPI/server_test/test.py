import config
import pyNeoByse
import time
Connection = pyNeoByse.NeoByse(config.URL,config.Account)
labelsEnum = pyNeoByse.LabelsNodesEnum

start_time = time.time()
tx = Connection.__congraph__('prectice')
print("--- %s seconds ---" % (time.time() - start_time))


start_time = time.time()
node = Connection.searth_node_by_name(tx,'S11')
print("--- %s seconds ---" % (time.time() - start_time))


start_time = time.time()
tt = node.has_label(labelsEnum.checkpoint.value)
print("--- %s seconds ---" % (time.time() - start_time))

start_time = time.time()
parent = Connection.return_all_parents_for_the_node(tx,'WW13')
type(parent)
mas = [p for p in parent]
print(mas)
print(len(mas))
print("--- %s seconds ---" % (time.time() - start_time))


#start_time = time.time()
#print(Connection.return_all_nodes_by_labels(tx,labelsEnum.checkpoint))
#print("--- %s seconds ---" % (time.time() - start_time))




