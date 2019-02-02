using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static Grpc.Core.Server;

namespace Grpc.Extensions.DependencyInjection.ServerSide
{
  public sealed class GrpcServerOptions
  {
    public ICollection<ServerPort> Ports { get; } = new List<ServerPort>();
    public ICollection<ServiceRegistration> Services { get; } = new List<ServiceRegistration>();
  }

  public sealed class ServiceRegistration
  {
    public Type ServiceType { get; }
    public Type ImplType { get; }
    public ServiceRegistration(Type serviceType, Type implType)
    {
      ServiceType = serviceType;
      ImplType = implType;
    }
  }
}
