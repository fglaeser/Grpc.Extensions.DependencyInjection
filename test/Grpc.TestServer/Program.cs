using Grpc.Core;
using Grpc.Extensions.DependencyInjection;
using Helloworld;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Grpc.TestServer
{
  class GreeterImpl : Greeter.GreeterBase
  {
    // Server side handler of the SayHello RPC
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
      return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
    }
  }

  class Program
  {
    static async Task Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      var host = new HostBuilder()
        .ConfigureServices(s =>
        {
          s.AddGrpcServer()
          .AddPort("localhost", 50051, Core.ServerCredentials.Insecure)
          .AddService<GreeterImpl>(typeof(Greeter));
        });
        await host.RunConsoleAsync();
    }
  }
}
