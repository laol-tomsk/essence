import Calculate_pb2
import Calculate_pb2_grpc
import grpc
from concurrent import futures
import logging
import pyNeoByse
import config
import probabilistic

class Calculate(Calculate_pb2_grpc.CalculatingService):

	def calculate(self, request, context):
		Connection = pyNeoByse.NeoByse(config.URL,config.Account)
		col = probabilistic.CalculatingTheProbability(Connection,'main2'#db.get(request.key)
			,request._map,request.iter)
		req = col.a()
		plh,plhp = col.get_plh_and_plhp(request.threshold)
		#print(Calculate_pb2.Response(_res = req)._res)
		return Calculate_pb2.Response(_res = req,plh = plh,plhp = plhp)

	def test(self, request, context):
		print(Calculate_pb2.Test(_test = request._test)._test)
		return Calculate_pb2.Test(_test = request._test)

def serve():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    Calculate_pb2_grpc.add_CalculatingServiceServicer_to_server(Calculate(), server)
    server.add_insecure_port('[::]:30001')
    server.start()
    server.wait_for_termination()

if __name__ == '__main__':
	logging.basicConfig()
	serve()