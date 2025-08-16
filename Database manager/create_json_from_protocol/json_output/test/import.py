import json
import easygui


def save_norm_json(key_,json_):
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

	if str_json[len(str_json)-1] == ',':
		str_json = str_json[:len(str_json)-1]+"]"
	else:
		str_json+="]"

	f = open(str(key_)+'.json','w')
	f.write(json.dumps(eval(str_json), indent=4))
	f.close()


def main():
	input_file = easygui.fileopenbox(default="*.json",filetypes="*.json")
	if input_file != None:
		with open(str(input_file)) as f:
			prectice = json.load(f)

		for key in prectice[0].keys():
			save_norm_json(key,prectice[0][key])
	else:
		easygui.msgbox("The folder is not selected!", "Error", "ОК")


if __name__ == '__main__':
	main()