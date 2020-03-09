using Castle.Windsor;
using Castle.Windsor.Installer;
using System.Web.Http;

namespace FootballTeams.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            GlobalConfiguration.Configuration.DependencyResolver = new FootballTeams.Web.Api.IoC.DependencyResolver(container.Kernel);
        }
    }
}
