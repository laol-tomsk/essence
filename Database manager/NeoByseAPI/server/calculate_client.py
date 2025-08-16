import Calculate_pb2
import Calculate_pb2_grpc
import grpc

def run():
	channel = grpc.insecure_channel('localhost:30001')
	stub = Calculate_pb2_grpc.CalculatingServiceStub(channel)
	response = stub.test(Calculate_pb2.Test(_test = 'asd'))

run()