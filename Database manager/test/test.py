import config
import probabilistic
import pyNeoByse
import json

def create_databese():
	connect = pyNeoByse.NeoByse(config.URL,config.Account)
	connect.сreate_database("test")
	parent_str = ""
	degree_={}
	for i in range(300):
		connect.create_new_node("cpt_not",{'name': "S"+str(i)},"test")
		parent_str+="S"+str(i)+" "
		degree_.update({"S"+str(i):5})

	parent_str+="AC"
	degree_.update({"AC":2})
	connect.create_new_node("cpt",{'name':"A",'parents':parent_str,'degree':json.dumps(degree_)},"test")
	connect.create_new_node("decision",{'name':"AC"},"test")

	for parent in parent_str.split(" "):
		connect.create_new_relationship(connect.searth_node(parent,"test"),connect.searth_node("A","test"),"test")

def main():
	pb = probabilistic.CalculatingTheProbability(pyNeoByse.NeoByse(config.URL,config.Account),"test",'{"A":[false,true]}',2)
	print(pb.a())
if __name__ == "__main__":
	create_databese()
	main()