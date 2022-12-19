using Grpc.Core;
using GrpcService;
using System.Text;

namespace GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            await Task.CompletedTask;
            return GetHelloReply(request.Name);
        }

        public override async Task SayHelloStreamingFromServer(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            const int times = 10;
            const int msDelay = 100;
            for (int i = 0; i < times; i++)
            {
                await responseStream.WriteAsync(GetHelloReply(request.Name));
                await Task.Delay(msDelay);
            }
        }

        public override async Task<HelloReply> SayHelloStreamingFromClient(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
        {
            var names = new StringBuilder();
            await foreach (var request in requestStream.ReadAllAsync()) 
            {
                names.Append($"{request.Name}, ");
            }
            names.Remove(names.Length - 2, 2);
            return GetHelloReply(names.ToString());
        }

        public override async Task SayHelloStreamingFromBoth(IAsyncStreamReader<HelloRequest> requestStream, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            var names = new StringBuilder();
            await foreach (var request in requestStream.ReadAllAsync())
            {
                names.Append($"{request.Name}, ");
            }
            names.Remove(names.Length - 2, 2);

            const int times = 10;
            const int msDelay = 100;
            for (int i = 0; i < times; i++)
            {
                await responseStream.WriteAsync(GetHelloReply(names.ToString()));
                await Task.Delay(msDelay);
            }
        }

        private HelloReply GetHelloReply(string name)
        {
            return new HelloReply
            {
                Message = $"Hello {name}"
            };
        }
    }
}