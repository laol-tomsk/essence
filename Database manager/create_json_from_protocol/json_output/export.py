import json
import easygui

def get_norm_json(file):
	with open(file) as f:
		json_ = json.load(f)

	str_json = "["
	for iter_ in json_:
		str_json+="{"
		for key in iter_.keys():
			str_json += "'"+str(key).replace("'",r"\'")+"':"
			if type(iter_[key]) == str:
				str_json+="'"+str(iter_[key]).replace("'",r"\'")+"',"
			else:
				str_json+=str(iter_[key])+","
		str_json =str_json[:len(str_json)-1]
		str_json +="},"


	return str_json[:len(str_json)-1]+ "]"

def main():
	input_file = easygui.diropenbox()
	if input_file != None:
		mas_file_json = ['activities.json','alphaContainments.json','alphaCriterions.json','alphas.json','checkpoints.json','degreesOfEvidence.json','levelOfDetails.json','workProductCriterions.json','states.json','workProductManifests.json','workProducts.json']
		str_json_practice = "[{"

		for prop in mas_file_json:
			str_json_practice+="'"+prop.split('.')[0]+"':"+get_norm_json(prop)+','

		str_json_practice = str_json_practice[:len(str_json_practice)-1]
		str_json_practice+='}]'
		f = open(str(input_file)+'/prectice.json','w')
		f.write(json.dumps(eval(str_json_practice), indent=4))
		f.close()
	else:
		easygui.msgbox("The folder is not selected!", "Error", "ОК")

if __name__ == '__main__':
	main()