import re
import time
import json
import math
import threading
import HTTP_API_neo4j
import enum
import os

class LabelsNodesEnum(enum.Enum):
	state = "state"
	normalVState = "normalVState"
	checkpoint = "checkpoint"
	ManagerOpinionCheckpoint = "ManagerOpinionCheckpoint"
	normalVDetail = "normalVDetail"
	Addcheckpoint = "Addcheckpoint"
	DynamicCheckpoint = "DynamicCheckpoint"

class COL_THREAD(threading.Thread):
	def __init__(self,api,mas_parents,vector,guid,iteration,p,mas_dictionary_nodes,data_add_dict,projectScale,mas_l,mas_r,mas_y,l,r,y):
		threading.Thread.__init__(self)
		self.L = l
		self.R = r
		self.Y = y
		self.p = p
		self.API = api
		self.mas_parents = mas_parents
		self.vector = vector
		self.node_guid = guid
		self.number_iteration = iteration
		self.Mas_dictionary_nodes = mas_dictionary_nodes
		self.Data_add_dict = data_add_dict
		self.projectScale = projectScale
		self.mas_l = mas_l
		self.mas_r = mas_r
		self.mas_y = mas_y

	def standard_algorithm(self,node_guid,parent_guid,unit_bit,number_iteration,K):
		#print('Standart')
		#print(bool(unit_bit))
		if unit_bit:
			if self.API.get_type_influence_node(parent_guid,node_guid):
				#print("L")
				self.L *= (2**self.API.get_degree_influence_node(parent_guid,node_guid))*K
				if self.API.has_label_node(parent_guid,LabelsNodesEnum.DynamicCheckpoint.value):
					self.Y *= self.Mas_dictionary_nodes[number_iteration-1][node_guid]
				else:
					self.Y *= self.Mas_dictionary_nodes[number_iteration][parent_guid]
			else:
				self.R *= (2**self.API.get_degree_influence_node(parent_guid,node_guid))*K
				if self.API.has_label_node(parent_guid,LabelsNodesEnum.DynamicCheckpoint.value):
					self.Y *= 1 - self.Mas_dictionary_nodes[number_iteration-1][node_guid]
				else:
					self.Y *= 1 - self.Mas_dictionary_nodes[number_iteration][parent_guid]
		else:
			if self.API.get_type_influence_node(parent_guid,node_guid):
				self.R  = (2**self.API.get_degree_influence_node(parent_guid,node_guid))*K
				if self.API.has_label_node(parent_guid,LabelsNodesEnum.DynamicCheckpoint.value):
					self.Y = 1 - self.Mas_dictionary_nodes[number_iteration-1][node_guid]
				else:
					self.Y = 1 -self.Mas_dictionary_nodes[number_iteration][parent_guid]

	def return_N_on_state(self,parent,number_iteration):
		#print('N_S')
		counter = 0
		for alpha_key in self.Data_add_dict.keys():
			counter_flag = 0
			for checkpoint_key in self.Data_add_dict[alpha_key].keys():
				if number_iteration in self.Data_add_dict[alpha_key][checkpoint_key]:
					if self.Data_add_dict[alpha_key][checkpoint_key][number_iteration]:
						counter_flag+=1
			if counter_flag == len(self.Data_add_dict[alpha_key]):
				counter+=1

		return counter

	def return_N_on_details(self,parent,number_iteration):
		#print('N_D')
		counter = 0
		for alpha_key in self.Data_add_dict.keys():
			for checkpoint_key in self.Data_add_dict[alpha_key].keys():
				if number_iteration in self.Data_add_dict[alpha_key][checkpoint_key]:
					if self.Data_add_dict[alpha_key][checkpoint_key][number_iteration]:
						counter+=1
						break

		return counter


	def new_algorithm(self,node_guid,parent_guid,unit_bit,number_iteration,K):
		#print('New')
		normal = self.API.get_normalValue_node(parent_guid)
		Z = normal*self.projectScale
		if Z == 1:
			return self.standard_algorithm(node_guid,parent_guid,unit_bit,number_iteration,K)
		if Z < 1:
			Z = 1
		if Z > 1:
			Z = math.ceil(Z)
		if self.API.has_label_node(parent_guid,LabelsNodesEnum.normalVState.value):
			N = self.return_N_on_state(parent_guid,number_iteration)
		if self.API.has_label_node(parent_guid,LabelsNodesEnum.normalVDetail.value):
			N = self.return_N_on_details(parent_guid,number_iteration)
		if N == normal:
			return self.standard_algorithm(node_guid,parent_guid,unit_bit,number_iteration,K)
		if N < normal:
			K = N/Z
		if N > normal:
			K = math.log(N,Z)

		return self.standard_algorithm(node_guid,parent_guid,unit_bit,number_iteration,K)

	def run(self):
		K = 1
		if self.API.has_label_node(self.mas_parents[self.p],LabelsNodesEnum.normalVState.value) or self.API.has_label_node(self.mas_parents[self.p],LabelsNodesEnum.normalVDetail.value) or self.API.has_label_node(self.mas_parents[self.p],LabelsNodesEnum.Addcheckpoint.value):
			self.new_algorithm(self.node_guid,self.mas_parents[self.p],int(self.vector[self.p]),self.number_iteration,K)
		else:
			self.standard_algorithm(self.node_guid,self.mas_parents[self.p],int(self.vector[self.p]),self.number_iteration,K)

		self.mas_l.append(self.L)
		self.mas_r.append(self.R)
		self.mas_y.append(self.Y)

class COL_THREAD_2(threading.Thread):
	def __init__(self,mas_x,api,mas_parents,vector,guid,iteration,mas_dictionary_nodes,data_add_dict,projectScale,l,r,y):
		threading.Thread.__init__(self)
		self.L = l
		self.R = r
		self.Y = y
		self.API = api
		self.mas_parents = mas_parents
		self.vector = vector
		self.node_guid = guid
		self.number_iteration = iteration
		self.Mas_dictionary_nodes = mas_dictionary_nodes
		self.Data_add_dict = data_add_dict
		self.ProjectScale = projectScale
		self.mas_x = mas_x

	def run(self):
		mas_l = []
		mas_r = []
		mas_y = []
		mas_threads = []
		for p in range(1,len(self.mas_parents)):
			thread = COL_THREAD(self.API,self.mas_parents,self.vector,self.node_guid,self.number_iteration,p,self.Mas_dictionary_nodes,self.Data_add_dict,self.ProjectScale,mas_l,mas_r,mas_y,self.L,self.R,self.Y)
			mas_threads.append(thread)
			thread.start()

		for thread in mas_threads:
			thread.join()

		L = math.prod(mas_l)
		#if L < 0:
		#	L*=-1
		R = math.prod(mas_r)
		#if R < 0:
		#	R*=-1
		Y = math.prod(mas_y)
		#if Y < 0:
		#	Y*=-1
		#print(f"L -> {L}")
		#print(f"R -> {R}")
		#print(f"Y -> {Y}")
		#print(f"X ->{(L/(L+R))*Y}")
		#print(f"vector - > {self.vector}")
		self.mas_x.append((L/(L+R))*Y)

class CalculatingTheProbability(object):
	def __init__(self,name_database,table,Iteration,projectScale = 1):
		self.API = HTTP_API_neo4j.API(name_database)
		self.Iteration = Iteration
		self.ProjectScale = projectScale
		#self.Mas_vector = self.__prepare_the_vector__()
		self.Data_dict, self.Data_add_dict = self.__get_dictionary_data_tables__(table)
		self.Mas_calculate_nodes,self.Mas_dictionary_nodes,self.Dict_name_nodes = self.__get_mas_dictionary__()
		self.Turn = []

	def __prepare_the_vector__(self,manager_decision,len_parents):
		mas = []
		if manager_decision:
			for a in range((2**len_parents)//2,2**len_parents):
				bin_value = bin(a)
				bin_value = bin_value[2:]
				while len(bin_value) != len_parents:
					bin_value='0'+bin_value
				mas.append(bin_value)
		else:
			for a in range((2**len_parents)//2):
				bin_value = bin(a)
				bin_value = bin_value[2:]
				while len(bin_value) != len_parents:
					bin_value='0'+bin_value
				mas.append(bin_value)
		return mas

	def __get_dictionary_data_tables__(self,table):
		alpha_dict = json.loads(table)
		data_dict = {}
		for alpha_key in alpha_dict[0].keys():
			mas = [alpha_dict[0][alpha_key][i][str(i)] for i in range(len(alpha_dict[0][alpha_key]))]
			data_dict.update({alpha_key:mas})
		_dict = {}

		for alpha_key in alpha_dict[1].keys():
			_dict_2 = {}
			for checkpoint_key in alpha_dict[1][alpha_key][0].keys():
				_dict_3 = {}
				for _iter in alpha_dict[1][alpha_key][0][checkpoint_key]:
					for i in _iter.keys():
						_dict_3.update({int(i):_iter[i]})
				_dict_2.update({checkpoint_key:_dict_3})
			_dict.update({alpha_key:_dict_2})


		return data_dict,_dict

	def __get_mas_dictionary__(self):
		mas_all_nodes = self.API.get_all_nodes_the_label(LabelsNodesEnum.checkpoint.value)+self.API.get_all_nodes_the_label(LabelsNodesEnum.state.value)
		mas_dictionary_nodes = []
		_dict = {}
		_dict_zero = {}
		dict_node_name = {}
		for node_guid in mas_all_nodes+self.API.get_all_nodes_the_label(LabelsNodesEnum.normalVDetail.value)+self.API.get_all_nodes_the_label(LabelsNodesEnum.normalVState.value):
			_dict.update({node_guid:-1})
			_dict_zero.update({node_guid:0})
			dict_node_name.update({node_guid:self.API.get_node_name(node_guid)})

		for _iter in range(self.Iteration):
			if _iter == 0:
				mas_dictionary_nodes.append(_dict_zero)
			else:
				mas_dictionary_nodes.append(_dict)

		return mas_all_nodes,mas_dictionary_nodes,dict_node_name

	def calculete_node(self,node_guid,number_iteration):
		#print(node_guid)
		if not(node_guid in self.Turn):
			self.Turn.append(node_guid)
			mas_parents = self.API.get_node_parents(node_guid)
			#print(len(mas_parents))
			for parent in mas_parents:
				if(self.API.has_label_node(parent,LabelsNodesEnum.checkpoint.value) or self.API.has_label_node(parent,LabelsNodesEnum.state.value)):
					if self.Mas_dictionary_nodes[number_iteration][parent] == -1:
						self.calculete_node(parent,number_iteration)

			if(self.API.has_label_node(node_guid,LabelsNodesEnum.state.value)):
				self.Mas_dictionary_nodes[number_iteration][node_guid] = math.prod([self.Mas_dictionary_nodes[number_iteration][parent] for parent in mas_parents])
				#print(f"{self.Dict_name_nodes[node_guid]} -> {self.Mas_dictionary_nodes[number_iteration][node_guid]}")
				return

			print(self.Dict_name_nodes[node_guid])
			X = 0
			L = 1
			R = 1
			Y = 1
			value_log = self.__prepare_the_vector__(self.Data_dict[node_guid][number_iteration], len(mas_parents))
			#print(value_log)
			if self.Data_dict[node_guid][number_iteration]:
				L *= (2**self.API.get_degree_influence_node(mas_parents[0],node_guid))
			else:
				R *= (2**self.API.get_degree_influence_node(mas_parents[0],node_guid))
			#if self.Data_dict[node_guid][number_iteration]:
			#	value_log = self.Mas_vector[len(self.Mas_vector)//2:len(self.Mas_vector)//2+(2**len(mas_parents))]
			#else:
			#	value_log = self.Mas_vector[0:(2**len(mas_parents))]
			#print(f"{self.Dict_name_nodes[node_guid]} -> {value_log}")
			#print(f"iter1 {len(value_log)}")
			#print(f"iter1 {len(value_log[0])-(len(value_log[0])-len(mas_parents)+1)}")
			#print(f"iter2 {(len(value_log[0])-(len(value_log[0])-len(mas_parents)+1))*len(value_log)}")
			mas_x = []
			mas_threads = []
			#print(f"{self.Dict_name_nodes[node_guid]} -> {mas_parents}")
			counter = 0
			for vector in value_log:
				#print(f"{self.Dict_name_nodes[node_guid]} -> {vector}")
				thread = COL_THREAD_2(mas_x,self.API,mas_parents,vector,node_guid,number_iteration,self.Mas_dictionary_nodes,self.Data_add_dict,self.ProjectScale,L,R,Y)
				mas_threads.append(thread)
				print(f"Vector {counter}")
				counter+=1
				os.system("CLS")
				thread.start()

				if len(mas_threads) ==25:
					for thread in mas_threads:
						thread.join()

					mas_threads = []

			for thread in mas_threads:
				thread.join()

			#S = 0.05
			#for p in mas_parents:
			#	if self.API.has_label_node(p,LabelsNodesEnum.checkpoint.value):
			#		if self.Data_dict[p][number_iteration]:
			#			S+= 0.08

			self.Mas_dictionary_nodes[number_iteration][node_guid] = sum(mas_x)
			print(f"{self.Dict_name_nodes[node_guid]} -> {self.Mas_dictionary_nodes[number_iteration][node_guid]}")
			time.sleep(1)
			return
		else:
			return


	def calculete_all(self):
		#print(self.Data_dict)
		start_time = time.time()
		final_dict_end = {}
		for iteration in range(self.Iteration):
			self.Turn = []
			if iteration == 0:
				continue
			print("ITERATION" + str(iteration))
			final_dict = {}
			for node_guid in self.Mas_calculate_nodes:
				if not(node_guid in self.Turn):
					self.calculete_node(node_guid,iteration)

				final_dict.update({self.Dict_name_nodes[node_guid]:self.Mas_dictionary_nodes[iteration][node_guid]})

			final_dict_end.update({str(iteration):final_dict})
		with open('col.json','w') as f:
			f.write(json.dumps(final_dict_end, indent=4))
		#print(self.Mas_dictionary_nodes)
		print("--- %s seconds ---" % (time.time() - start_time))
		return json.dumps(final_dict_end)

	def get_plh_and_plhp(self,thres):
		start_time = time.time()
		mas_plh =[]
		dict_plhp = {}
		for node_guid in self.Mas_calculate_nodes:
			if self.Mas_dictionary_nodes[self.Iteration-1][node_guid] < thres and self.API.has_label_node(node_guid,LabelsNodesEnum.checkpoint.value):
				mas_plh.append([self.Dict_name_nodes[node_guid],self.Mas_dictionary_nodes[self.Iteration-1][node_guid]])
			str_parent = []
			for parent in self.API.get_node_parents(node_guid):
				if self.API.has_label_node(parent,LabelsNodesEnum.checkpoint.value):
					if not(self.Data_dict[node_guid][self.Iteration-1]):
						str_parent.append(self.Dict_name_nodes[parent])

			dict_plhp.update({self.Dict_name_nodes[node_guid]:",".join(str_parent)})

		print("--- %s seconds ---" % (time.time() - start_time))
		return json.dumps(mas_plh),json.dumps(dict_plhp)
