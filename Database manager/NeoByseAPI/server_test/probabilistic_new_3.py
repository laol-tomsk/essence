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

	def __create_vector__(self,len_parents):
		mas = []
		for a in range(2**len_parents):
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
		for node_guid in mas_all_nodes:
			_dict.update({node_guid:-1})
			_dict_zero.update({node_guid:0})
			dict_node_name.update({node_guid:self.API.get_node_name(node_guid)})

		for _iter in range(self.Iteration):
			if _iter == 0:
				mas_dictionary_nodes.append(_dict_zero)
			else:
				mas_dictionary_nodes.append(_dict)

		return mas_all_nodes,mas_dictionary_nodes,dict_node_name

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
			Lbase = 1
			Rbase = 1
			if self.Data_dict[node_guid][number_iteration]:
				Lbase*=2**self.API.get_degree_influence_node(mas_parents[0],node_guid)
			else:
				Rbase*=2**self.API.get_degree_influence_node(mas_parents[0],node_guid)

			mas_normal_parent = self.API.get_node_parents_labels(node_guid,LabelsNodesEnum.normalVDetail.value)+self.API.get_node_parents_labels(node_guid,LabelsNodesEnum.normalVState.value)
			if len(mas_normal_parent) > 0:
				for parent_guid in mas_normal_parent:
					K = 1
					if self.API.has_label_node(parent_guid,LabelsNodesEnum.normalVState.value):
						N = self.return_N_on_state(parent_guid,number_iteration)
					if self.API.has_label_node(parent_guid,LabelsNodesEnum.normalVDetail.value):
						N = self.return_N_on_details(parent_guid,number_iteration)
					normal = self.API.get_normalValue_node(parent_guid)
					Z = normal*self.ProjectScale
					if self.API.get_type_influence_node(parent_guid,node_guid):
						if N == 0:
							K = 1
							Rbase *= K*(2**self.API.get_degree_influence_node(parent_guid,node_guid))
						elif N >= Z:
							K = math.log(N,Z)
							Lbase*=	K*(2**self.API.get_degree_influence_node(parent_guid,node_guid))
						elif N < Z:
							K = 1 - (1/(1+N))
							Rbase*=(1-K)*(2**self.API.get_degree_influence_node(parent_guid,node_guid))
							Lbase*=	K*(2**self.API.get_degree_influence_node(parent_guid,node_guid))
					else:
						if N == 0:
							pass
						elif N >= Z:
							K = math.log(N,Z)
							Rbase *= K*(2**self.API.get_degree_influence_node(parent_guid,node_guid))
						elif N < Z:
							K = N/Z
							Rbase *= K*(2**self.API.get_degree_influence_node(parent_guid,node_guid))

			mas_stat_parents = self.API.get_node_parents_labels(node_guid,LabelsNodesEnum.checkpoint.value)+self.API.get_node_parents_labels(node_guid,LabelsNodesEnum.state.value)+self.API.get_node_parents_labels(node_guid,LabelsNodesEnum.DynamicCheckpoint.value)
			vector_paretns = self.__create_vector__(len(mas_stat_parents))
			X = 0
			for vector in vector_paretns:
				L = Lbase
				R = Rbase
				Y = 1
				for v in range(len(vector)):
					if bool(int(vector[v])):
						L *= 2**self.API.get_degree_influence_node(mas_stat_parents[v],node_guid)
						if self.API.has_label_node(mas_stat_parents[v],LabelsNodesEnum.DynamicCheckpoint.value):
							Y *= self.Mas_dictionary_nodes[number_iteration - 1][node_guid]
						else:
							Y *= self.Mas_dictionary_nodes[number_iteration][mas_stat_parents[v]]
					else:
						R *= 2**self.API.get_degree_influence_node(mas_stat_parents[v],node_guid)
						if self.API.has_label_node(mas_stat_parents[v],LabelsNodesEnum.DynamicCheckpoint.value):
							Y *= 1 - self.Mas_dictionary_nodes[number_iteration - 1][node_guid]
						else:
							Y *= 1 - self.Mas_dictionary_nodes[number_iteration][mas_stat_parents[v]]
				X +=(L/(L+R))*Y
			self.Mas_dictionary_nodes[number_iteration][node_guid] = X
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
