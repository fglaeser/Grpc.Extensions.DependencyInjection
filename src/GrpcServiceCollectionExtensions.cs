using Grpc.Core;
using Grpc.Extensions.DependencyInjection.ServerSide;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Grpc.Extensions.DependencyInjection
{
  public static class GrpcServiceCollectionExtensions
  {
    public static IGrpcServerBuilder AddGrpcServer(this IServiceCollection services)
    {
      services.AddSingleton<Server>();
      services.AddHostedService<GrpcServer>();
      return new GrpcServerBuilder(services);
    }
  }
}
