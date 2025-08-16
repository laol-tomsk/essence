import socket

import config
import eel
import pyNeoByse
import os
import easygui

Connection = pyNeoByse.NeoByse(config.URL,config.Account)
@eel.expose
def get_bases():
	print(Connection.get_bases())
	pass
@eel.expose
def show_databases_status():
	print(Connection.show_databases_status())
	pass
@eel.expose
def сreate_database():
	Connection.сreate_database(input("Name DATABASE: "))
	pass
@eel.expose
def drop_database():
	Connection.drop_database(input("Name DATABASE: "))
	pass
@eel.expose
def start_database():
	Connection.start_database(input("Name DATABASE: "))
	pass
@eel.expose
def stop_database():
	Connection.stop_database(input("Name DATABASE: "))
	pass
@eel.expose
def send_request():
	Connection.send_request(input("REQUEST: "),input("Name DATABASE: "))
	pass
@eel.expose
def copy_base():
	Connection.copy_base(input("Name DATABASE: "),False,"")
	pass
@eel.expose
def rename_database():
	Connection.rename_database(input("Old name DATABASE: "),input("New name DATABASE: "))
	pass
@eel.expose
def clear():
	os.system('cls')
	pass
@eel.expose
def all_database_remuve():
	Connection.all_database_remuve()
	pass
@eel.expose
def create_new_node():
	Connection.create_new_node(input("identity"),{'name':"asd", 'f':'123'},input("database"))
	pass

@eel.expose
def create_new_databese_prectice():
	input_file = easygui.fileopenbox(default="*.json",filetypes="*.json")
	Connection.create_database_from_prectice(input("Name DATABASE: "),input_file)
	pass

@eel.expose
def create_new_databese_protocol():
	input_file = easygui.fileopenbox(default="*.xlsx",filetypes="*.xlsx")
	Connection.create_database_from_protocol('protocol',input_file,'Полный протокол')
	pass

@eel.expose
def create_new_databese_XML():
	input_file = easygui.fileopenbox(default="*.xdsl",filetypes="*.xdsl")
	Connection.create_database_from_file('main',input_file)
	pass


def StartWeb():
	eel.init("web")
	eel.start("main.html", size=(310,310), port=get_free_port())

def main():
	StartWeb()
	pass

def get_free_port():
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind(('', 0))
        free_port = s.getsockname()[1]
    return free_port

if __name__ == '__main__':
	main()