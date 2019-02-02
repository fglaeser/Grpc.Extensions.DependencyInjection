using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Extensions.DependencyInjection.ServerSide
{
  public interface IGrpcServerBuilder
  {
    IGrpcServerBuilder AddPort(string host, int port, ServerCredentials credentials);
    IGrpcServerBuilder AddService<TImplementation>(Type serviceType) where TImplementation : class;
  }
}
