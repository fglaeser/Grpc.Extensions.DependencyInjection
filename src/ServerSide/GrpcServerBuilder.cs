using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Grpc.Extensions.DependencyInjection.ServerSide
{
  public sealed class GrpcServerBuilder : IGrpcServerBuilder
  {
    public GrpcServerBuilder(IServiceCollection services)
    {
      Services = services;
    }

    public IServiceCollection Services { get; }

    public IGrpcServerBuilder AddPort(string host, int port, ServerCredentials credentials)
    {
      Services.Configure<GrpcServerOptions>(options =>
      {
        options.Ports.Add(new ServerPort(host, port, credentials));
      });
      return this;
    }

    public IGrpcServerBuilder AddService<TImplementation>(Type serviceType) where TImplementation : class
    {

      Services.Configure<GrpcServerOptions>(options =>
      {
        options.Services.Add(new ServiceRegistration(serviceType, typeof(TImplementation)));
      });
      Services.AddSingleton<TImplementation>();
      return this;
    }
  }

}
