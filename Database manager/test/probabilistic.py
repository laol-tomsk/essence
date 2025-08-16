import pyNeoByse
import re
import time
import json

class CalculatingTheProbability(object):
	def __init__(self,NeoByseConnect,name_database,table,Iteration):
		self.connect = NeoByseConnect
		self.connect_database = self.connect.__congraph__(name_database)
		self.database = name_database
		self.iteration = Iteration
		self.data_dict,self.mas_dictionary_nodes = self.__get_dictionary_data_tables__(table)

	def __create_dictionary_nodes__(self,counter):
		mas = []
		dictionary_nodes_all = {name:-1 for name in [node.get("name") for node in self.connect.__congraph__(self.database).nodes.match('cpt').all()]}
		dictionary_nodes_all.update({name:0.5 for name in [node.get("name") for node in self.connect.__congraph__(self.database).nodes.match('cpt_not').all()]})
		#mas_name_nodes_condition = self.connect_database.nodes.match(probabilities="0 1").all()
		#for name_nodes in mas_name_nodes_condition:
			#dictionary_nodes_all[name_nodes.get("name")] = 0.5
		for i in range(counter):
			mas.append(dict(dictionary_nodes_all))
		return mas

	def __chec_calculating_parant__(self,parents):
		counter_patents = None
		for parent in parents:
			if parent in self.dictionary_nodes:
				counter_patents+=1
		chec_counter = None
		for parent in parents:
			if self.dictionary_nodes[parent] != -1:
				chec_counter+=1
		if counter_patents == chec_counter:
			return True
		else:
			return False

	def __get_dictionary_data_tables__(self,table):
		data_dict = {}
		wb = json.loads(table)
		for key in wb.keys():
			name = key
			mas = wb[key]
			data_dict.update({name:mas})
		return data_dict, self.__create_dictionary_nodes__(self.iteration)

		pass

	def __get_all_probabilistic_weights__(self,node_name):
		return list(map(float,self.connect_database.nodes.match(name = node_name).first().get("probabilities").split(" ")))

	def __get_node_parants__(self,node_name):
		return self.connect_database.nodes.match(name=node_name).first().get("parents").split(' ')

	def __get_all_true_probabilistic_weights__(self,node_name):
		probabilistic =  self.__get_all_probabilistic_weights__(node_name)
		i = 1
		mas = []
		for prob in probabilistic:
			if i%2 != 0:
				mas.append(prob)
			i+=1
		return mas

	def __return_probability_using_log__(self,dict_influence,mas_bit,parents_node, len_mas_bit):
		L = 1
		R = 1
		for bit_index in range(0, len_mas_bit):
			if mas_bit[bit_index] == 0:
				R*=2**dict_influence[parents_node[bit_index]]
			if mas_bit[bit_index] == 1:
				L*=2**dict_influence[parents_node[bit_index]]
		#print(L/(L+R))
		return L/(L+R)


	def __get_weight_value__(self,node_name,manager_decision):
		mas = []
		parants = self.__get_node_parants__(node_name)
		probabilistic = self.__get_all_true_probabilistic_weights__(node_name)
		probabilistic.reverse()
		for value_id in range(len(probabilistic)):
			mas_2 = []
			bin_value=bin(value_id)
			bin_value = bin_value[2:]
			while len(bin_value) != len(parants):
				bin_value='0'+bin_value
			dict_values = {parants[i]:bool(int(bin_value[i])) for i in range(len(parants))}
			if dict_values[node_name+'C'] == manager_decision:
				mas_2.append(dict_values)
				mas_2.append(probabilistic[value_id])
				mas.append(mas_2)
		return mas

	def __get_weight_value_log__(self,node_name,manager_decision):
		mas = []
		parants = self.__get_node_parants__(node_name)
		parants = [parants[0]]+[node_name+'S']+parants[1:len(parants)]
		#probabilistic = self.__get_all_true_probabilistic_weights__(node_name)
		#probabilistic.reverse()
		for value_id in range(2**len(parants)):
			mas_2 = []
			bin_value=bin(value_id)
			bin_value = bin_value[2:]
			while len(bin_value) != len(parants):
				bin_value='0'+bin_value
			dict_values = {parants[i]:bool(int(bin_value[i])) for i in range(len(parants))}
			if dict_values[node_name+'C'] == manager_decision:
				mas_2.append(dict_values)
				bin_value_mas = []
				for i in bin_value:
					bin_value_mas.append(int(i))
				#mas_2.append(----,[bin_value_mas[0]] + bin_value_mas[2:len(bin_value_mas)],[parants[0]] +parants[2:len(parants)],len([bin_value_mas[0]] + bin_value_mas[2:len(bin_value_mas)])))
				mas.append(mas_2)
		return mas

	def calculete(self,iteration,node_name):
		#print(node_name)
		if node_name in self.mas_dictionary_nodes[iteration]:
			if self.mas_dictionary_nodes[iteration][node_name] == -1:
				parents = self.__get_node_parants__(node_name)

				for parent1 in parents:
					if parent1 in self.mas_dictionary_nodes[iteration]:
						if self.mas_dictionary_nodes[iteration][parent1] == -1:
							self.calculete(iteration,parent1)

				#Вероятность node-состояние
				if re.fullmatch(r'\D+\d{1}',node_name):
					P= 1.0
					for parent2 in parents:
						if parent2 in self.mas_dictionary_nodes[iteration]:
							P*=self.mas_dictionary_nodes[iteration][parent2]
					self.mas_dictionary_nodes[iteration][node_name] = P
					return

				if iteration == 0:
					#node_s_we = self.__get_all_probabilistic_weights__(node_name+'S')
					#self.mas_dictionary_nodes[iteration][node_name] = node_s_we[0]
					self.mas_dictionary_nodes[iteration][node_name] = 0
					return

				if iteration != 0:
					X = 0
					dict_degree = json.loads(self.connect_database.nodes.match(name=node_name).first().get("degree"))
					#print(dict_degree)
					parants = self.__get_node_parants__(node_name)

					for value_id in range(2**len(parants)):
						bin_value=bin(value_id)
						bin_value = bin_value[2:]
						while len(bin_value) != len(parants):
							bin_value='0'+bin_value
						dict_values = {parants[i]:bool(int(bin_value[i])) for i in range(len(parants))}
						#print(dict_values)
						L = 1
						R = 1
						Y = 1
						if dict_values[node_name+'C'] == self.data_dict[node_name][iteration]:
							for i in dict_values:
								if(dict_values[i] == True):
									L*=2**dict_degree[i]
									if not(re.fullmatch(r'\S+C{1}',i)):
										if re.fullmatch(r'\S+S{1}',i):
											Y*=self.mas_dictionary_nodes[iteration-1][i[:-1]]
										else:
											Y*=self.mas_dictionary_nodes[iteration][i]
								else:
									R*=2**dict_degree[i]
									if not(re.fullmatch(r'\S+C{1}',i)):
										if re.fullmatch(r'\S+S{1}',i):
											Y*=1.0-self.mas_dictionary_nodes[iteration-1][i[:-1]]
										else:
											Y*=1.0-self.mas_dictionary_nodes[iteration][i]
						#print(Y)
						#print(X)
							X+=(L/(L+R))*Y
						#print(X)
						#input()

					#for vector in self.__get_weight_value_log__(node_name,self.data_dict[node_name][iteration]):
						#print(vector)
						#Y = vector[1]
						#print(node_name +" Y = "+ str(Y))
						#for parent3 in [node_name+'S']+parents:
							#if not(re.fullmatch(r'\S+C{1}',parent3)):
								#if re.fullmatch(r'\S+S{1}',parent3):
									#if vector[0][parent3] == True:
										#Y*=self.mas_dictionary_nodes[iteration-1][parent3[:-1]]
									#else:
										#Y*=(1.0-self.mas_dictionary_nodes[iteration-1][parent3[:-1]])
								#else:
									#if vector[0][parent3] == True:
										#Y*=self.mas_dictionary_nodes[iteration][parent3]
									#else:
										#Y*=(1.0-self.mas_dictionary_nodes[iteration][parent3])
							#print("Y * P("+ str(parent3) + ") = "+ str(Y))
						#print(node_name)
						#print("Y = " + str(Y))
						#X+=Y
					#print(node_name +" X = "+ str(X))
					self.mas_dictionary_nodes[iteration][node_name] = X
					#print("dict "+ node_name + " = " + str(self.mas_dictionary_nodes[iteration][node_name]))
					return

	def a(self):
		start_time = time.time()
		final_dict_end = {}
		for iteration in range(self.iteration):
			print("ITERATION" + str(iteration))
			for node in [node.get("name") for node in self.connect_database.nodes.match('cpt').all()]:
				self.calculete(iteration,node)
			final_dict = {key:self.mas_dictionary_nodes[iteration][key] for key in self.data_dict.keys()}
			for key in self.data_dict.keys():
				if not(key in self.mas_dictionary_nodes[iteration]) and re.fullmatch(r'\D+\d{1}',key):
					final_dict.update({key:self.mas_dictionary_nodes[iteration][key]})
			final_dict_end.update({str(iteration):final_dict})
		print("--- %s seconds ---" % (time.time() - start_time))
		final_dict_str = json.dumps(final_dict_end)
		return final_dict_str

	def get_plh_and_plhp(self,thres):
		mas_plh = []
		for node in self.mas_dictionary_nodes[self.iteration-1]:
			if self.mas_dictionary_nodes[self.iteration-1][node] < thres and re.fullmatch(r'\D+\d{2}',node):
				mas_plh.append([node,self.mas_dictionary_nodes[self.iteration-1][node]])
		dict_plhp = {}
		for node in mas_plh:
			try:
				str_perent = []
				for perant in self.__get_node_parants__(node[0]):
					if re.fullmatch(r'\D+\d{2}',perant):
						if self.data_dict[perant][self.iteration-1] == False:
							str_perent.append(perant)
				dict_plhp.update({node[0]:",".join(str_perent)})
			except:
				continue
		return json.dumps(mas_plh),json.dumps(dict_plhp)



