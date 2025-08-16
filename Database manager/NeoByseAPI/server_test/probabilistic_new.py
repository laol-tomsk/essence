import pyNeoByse
import re
import time
import json
import math
import threading
import numba

#def __get_weight_value_log__(self,mas_parents,manager_decision):
#		print('log')
#		mas = []
#		for value_id in range(2**len(mas_parents)):
#			bin_value = bin(value_id)
#			bin_value = bin_value[2:]
#			while len(bin_value) != len(mas_parents):
#				bin_value='0'+bin_value
#			if bool(int(bin_value[1])) == manager_decision:
#				i= 0
#				for parent in mas_parents:
#					mas.append([[parent,bool(int(bin_value[i]))]])
#					i+=1
#		return mas

class cal_thread(threading.Thread):
	def __init__(self,_parent,labels,_number_iteration,l,r,y,k,_mas_dictionary_nodes,_dict_state_and_details):
		threading.Thread.__init__(self)
		self.parent = parent
		self.labelsEnum = labels,
		self.number_iteration = _number_iteration
		self.L = l
		self.R = r
		self.Y = y
		self.mas_dictionary_nodes = _mas_dictionary_nodes
		self.dict_state_and_details = _dict_state_and_details

	def return_R_Y(self,parent,number_iteration,R,Y,K):
		print('R_Y')
		R*=(2**parent[0]['degreeOfEvidenceEnumValue'])*K
		if parent[0].state.has_label(self.labelsEnum.DynamicCheckpoint.value):
			Y*=1-self.mas_dictionary_nodes[number_iteration-1][parent[0].start_node['guid']]
		else:
			Y*=1-self.mas_dictionary_nodes[number_iteration][parent[0].start_node['guid']]

		return R,Y

	def standard_algorithm(self,parent,number_iteration,L,R,Y,K):
		print('start')
		flag = False
		if parent[1]:
			if bool(parent[0]['typeOfEvidence']):
				L*=(2**parent[0]['degreeOfEvidenceEnumValue'])*K
				if parent[0].start_node.has_label(self.labelsEnum.DynamicCheckpoint.value):
					Y*=self.mas_dictionary_nodes[number_iteration-1][parent[0].start_node['guid']]
				else:
					Y*=self.mas_dictionary_nodes[number_iteration][parent[0].start_node['guid']]
			else:
				flag = True
		if not(parent[1]) or flag:
			if flag:
				R,Y = self.return_R_Y(parent,number_iteration,R,Y,K)
			else:
				if bool(parent[0]['typeOfEvidence']):
					return L,R,Y
				else:
					R,Y = self.return_R_Y(parent,number_iteration,R,Y,K)
		return L,R,Y

	def return_N_on_state(self,parent,number_iteration):
		print('NS')
		counter = 0
		for alpha in self.dict_state_and_details[parent[0].start_node['guid']]:
			counter_flag = 0
			for checkpoint in alpha:
				if alpha[checkpoint][0][str(number_iteration)] == True:
					counter_flag+=1
			if counter_flag == len(alpha):
				counter+= 1

		return counter


	def return_N_on_details(self,parent,number_iteration):
		print('ND')
		counter = 0
		for alpha in self.dict_state_and_details[parent[0].start_node['guid']]:
			for checkpoint in alpha:
				if alpha[checkpoint][0][str(number_iteration)] == True:
					counter+=1
					break

		return counter


	def new_algorithm(self,parent,number_iteration,L,R,Y,K):
		print('new')
		Z = parent[0].start_node['normalValue']*self.projectScale
		if Z == 1:
			return self.standard_algorithm(parent,number_iteration,L,R,Y,K)
		if Z < 1:
			Z = 1;
		if Z > 1:
			Z = math.ceil(Z)
		if parent[0].start_node.has_label(self.labelsEnum.normalVState.value):
			N = return_N_on_state(parent,number_iteration)
		else:
			N = return_N_on_details(parent,number_iteration)

		if N == parent[0].start_node['normalValue']:
			return self.standard_algorithm(parent,number_iteration,L,R,Y,K)
		if N < parent[0].start_node['normalValue']:
			K = N/Z
		if N > parent[0].start_node['normalValue']:
			K = math.log(N,Z)

		return self.standard_algorithm(parent,number_iteration,L,R,Y,K)

	def run(self):
		if self.parent[0].start_node.has_label(self.labelsEnum.state.value) or self.parent[0].start_node.has_label(self.labelsEnum.checkpoint.value):
			self.L,self.R,self.Y = self.standard_algorithm(parent,self.number_iteration,self.L,self.R,self.Y,1)
		if self.parent[0].start_node.has_label(self.labelsEnum.normalVDetail.value) or self.parent[0].start_node.has_label(self.labelsEnum.normalVState.value):
			self.L,self.R,self.Y = self.new_algorithm(self.parent,self.number_iteration,self.L,self.R,self.Y,1)

class value_log_Thread(threading.Thread):
	def __init__(self,_value_id, _mas_parents,_manager_decision,_aaa):
		threading.Thread.__init__(self)
		self.value_id = _value_id
		self.mas_parents = _mas_parents
		self.manager_decision = _manager_decision
		self.mas = []
		self.aaa = _aaa

	def run(self):
		if bool(int(self.aaa[self.value_id][1])) == self.manager_decision:
			i = 0
			for parent in self.mas_parents:
				self.mas.append([parent,bool(int(self.aaa[self.value_id][i]))])
				i+=1
		else:
			self.mas = None


class CalculatingTheProbability(object):
	def __init__(self,NeoByseConnect,name_database,table,Iteration,projectScale = 1):
		self.connect = NeoByseConnect
		self.connect_database = self.connect.__congraph__(name_database)
		self.name_database = name_database
		self.iteration = Iteration
		self.labelsEnum = pyNeoByse.LabelsNodesEnum
		self.projectScale = projectScale
		self.turn = []
		self.dict_parent = {}
		self.aaaaaaaa = self._zapolni_aaaaaa_()
		self.data_dict,self.dict_state_and_details,self.mas_dictionary_nodes, self.mas_calculete_nodes, self.dict_guid_name = self.__get_dictionary_data_tables__(table)


	def _zapolni_aaaaaa_(self):
		mas = []
		for a in range(2**20):
			bin_value = bin(a)
			bin_value = bin_value[2:]
			while len(bin_value) != 20:
				bin_value='0'+bin_value
			mas.append(bin_value)
		return mas

	def __get_dictionary_data_tables__(self,table):
		wb = json.loads(table)
		mas = []
		_dict = {}
		_dict_guid_name = {}
		mas_all_nodes = self.connect.return_all_nodes_by_labels(self.connect_database,self.labelsEnum.checkpoint)+self.connect.return_all_nodes_by_labels(self.connect_database,self.labelsEnum.state)
		for node in mas_all_nodes:
			_dict.update({node['guid']:-1})
			_dict_guid_name.update({node['guid']:node['name']})

		for _iter in range(self.iteration):
			mas.append(_dict)

		return wb[0],wb[1],mas, mas_all_nodes,_dict_guid_name

	def __get_weight_value_log__(self,mas_parents,manager_decision):
		print('log')
		print(len(mas_parents))
		mas = []
		mas_threads = []
		for value_id in range(2**len(mas_parents)):
			thread = value_log_Thread(value_id,mas_parents,manager_decision,self.aaaaaaaa)
			mas_threads.append(thread)
			thread.start()

		if thread.mas != None:
			mas.append(thread.mas)
		return mas


	def calculete_node(self,node,number_iteration):
		self.turn.append(node['guid'])
		print(node['name'])
		mas_parents = self.connect.return_all_parents_for_the_node(self.connect_database,node_guid=node['guid'])
		self.dict_parent.update({node['guid']:mas_parents})
		for parent in mas_parents:
			if parent.start_node.has_label(self.labelsEnum.state.value) or parent.start_node.has_label(self.labelsEnum.checkpoint.value):
				if self.mas_dictionary_nodes[number_iteration][parent.start_node['guid']] == -1 and not(parent.start_node['guid'] in self.turn):
					self.calculete_node(parent.start_node,number_iteration)

		if node.has_label(self.labelsEnum.state.value) and number_iteration != 0:
			self.mas_dictionary_nodes[number_iteration][node["guid"]] = math.prod([self.mas_dictionary_nodes[number_iteration][parent.start_node['guid']] for parent in mas_parents])
			return

		if number_iteration == 0:
			self.mas_dictionary_nodes[number_iteration][node["guid"]] = 0
			return
		else:
			X = 0
			for vector in self.__get_weight_value_log__(mas_parents,self.data_dict[node['guid']][number_iteration][str(number_iteration)]):
				L = 1
				R = 1
				Y = 1
				mas_l = []
				mas_r = []
				mas_y = []
				mas_threads = []
				for parent in vector:
					thread = cal_thread(parent,self.labelsEnum,number_iteration,L,R,Y,1,self.mas_dictionary_nodes,self.dict_state_and_details)
					mas_threads.append(thread)
					thread.start()

				for thread in mas_threads:
					thread.join()
					mas_l.append(thread.L)
					mas_r.append(thread.R)
					mas_t.append(thread.Y)

				L = math.prod(mas_l)
				R = math.prod(mas_r)
				Y = math.prod(mas_y)
				X+=(L/(L+R))*Y

			self.mas_dictionary_nodes[number_iteration][node['guid']] = X
			return

	def calculete_all(self):
		start_time = time.time()
		final_dict_end = {}
		for iteration in range(self.iteration):
			if iteration == 0:
				continue
			self.turn = []
			print("ITERATION" + str(iteration))
			for node in self.mas_calculete_nodes:
				if not(node['guid'] in self.turn):
					self.calculete_node(node,iteration)
			final_dict = {self.dict_guid_name[node['guid']]:self.mas_dictionary_nodes[iteration][node['guid']] for node in self.mas_calculete_nodes}
			final_dict_end.update({str(iteration):final_dict})
		print("--- %s seconds ---" % (time.time() - start_time))
		return json.dumps(final_dict_end)

	def get_plh_and_plhp(self,thres):
		start_time = time.time()
		mas_plh = []
		dict_plhp = {}
		for node in self.mas_calculete_nodes:
			if self.mas_dictionary_nodes[self.iteration-1][node['guid']] < thres and node.has_label(self.labelsEnum.checkpoint.value):
				mas_plh.append([self.dict_guid_name[node['guid']],self.mas_dictionary_nodes[self.iteration-1][node['guid']]])
			str_parent = []
			for parent in self.dict_parent[node['guid']]:
				if parent.start_node.has_label(self.labelsEnum.checkpoint.value):
					if self.data_dict[parent.start_node['guid']][number_iteration][str(self.iteration-1)] == False:
						str_parent.append(self.dict_guid_name[parent.start_node['guid']])
			dict_plhp.update({self.dict_guid_name[node['guid']]:",".join(str_parent)})

		print("--- %s seconds ---" % (time.time() - start_time))
		return json.dumps(mas_plh),json.dumps(dict_plhp)
