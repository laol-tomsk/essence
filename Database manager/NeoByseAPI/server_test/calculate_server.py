import Calculate_pb2
import Calculate_pb2_grpc
import grpc
from concurrent import futures
import logging
import pyNeoByse
import config
import probabilistic_new_one
import probabilistic_new_2
import probabilistic_new_3
import pickledb
import json

db = pickledb.load('hex.db', False)

class Calculate(Calculate_pb2_grpc.CalculatingService):

	def calculate(self, request, context):
		#print(request._map)
		#return
		#Connection = pyNeoByse.NeoByse(config.URL,config.Account)
		#col = probabilistic_new_one.CalculatingTheProbability('prectice',request._map,request.iter)
		#col = probabilistic_new_2.CalculatingTheProbability('prectice',request._map,request.iter)
		col = probabilistic_new_3.CalculatingTheProbability('prectice',request._map,request.iter)
		req = col.calculete_all()
		#plh,plhp = col.get_plh_and_plhp(request.threshold)
		#with open('col.json','w') as f:
			#f.write(col, indent=4)
		#with open('plh.json','w') as f:
		#	f.write(json.dumps(eval(plh), indent=4))
		#with open('plhp.json','w') as f:
		#	f.write(json.dumps(eval(plhp), indent=4))
		#print(Calculate_pb2.Response(_res = req)._res)
		return Calculate_pb2.Response(_res = req,plh = plh,plhp = plhp)

	def test(self, request, context):
		print(Calculate_pb2.Test(_test = request._test)._test)
		return Calculate_pb2.Test(_test = request._test)

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=1000))
    Calculate_pb2_grpc.add_CalculatingServiceServicer_to_server(Calculate(), server)
    server.add_insecure_port('[::]:30001')
    server.start()
    server.wait_for_termination()

if __name__ == '__main__':
	logging.basicConfig()
	serve()
