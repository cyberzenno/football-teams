using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;

namespace FootballTeams.Web.Api.IoC
{
    internal class DependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public DependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new DependencyScope(kernel);
        }

        public object GetService(Type type)
        {
            return kernel.HasComponent(type) ? kernel.Resolve(type) : null;
        }

        public IEnumerable<object> GetServices(Type type)
        {
            return kernel.ResolveAll(type).Cast<object>();
        }

        public void Dispose()
        {
        }
    }
}