using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Grpc.Core;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Grpc.Extensions.DependencyInjection.ServerSide
{
  public class GrpcServer : IHostedService
  {
    private readonly Server _server;
    private readonly IServiceProvider _serviceProvider;
    private readonly GrpcServerOptions _options;
    public GrpcServer(IServiceProvider serviceProvider, IOptions<GrpcServerOptions> options, Server server)
    {
      _serviceProvider = serviceProvider;
      _options = options.Value;
      _server = server;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      _options.Ports.ToList().ForEach(p => _server.Ports.Add(p));
      _options.Services.ToList().ForEach(r => 
      {
        var implementation = _serviceProvider.GetService(r.ImplType);
        var serviceDescription = r.ServiceType.GetMethod("BindService", BindingFlags.Public | BindingFlags.Static)
        .Invoke(null, new object[] { implementation });
        _server.Services.Add((ServerServiceDefinition)serviceDescription);
      });

      _server.Start();
      return Task.CompletedTask;

    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
      await _server.ShutdownAsync();
    }
  }
}
