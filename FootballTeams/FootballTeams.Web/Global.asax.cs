using FootballTeams.Web.IoC;
using FootballTeams.Web.Models.Mappings;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FootballTeams.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        [System.Obsolete]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Confingure Castle Windsor Container
            IocContainer.Setup();

            //Configure AutoMapper [Obsolete]
            //todo: upgrade to AutoMapper 9
            AutoMapperConfiguration.Configure();
        }

        //note: inspiration from:
        //http://rahulrajatsingh.com/2014/11/a-beginners-tutorial-on-custom-forms-authentication-in-asp-net-mvc-application/
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            //todo: implement the proper Roles feature
            if (User.Identity.Name == "global@manager.com")
            {
                try
                {
                    //note:
                    //for every request, this is setting the generic Current.User with the given roles
                    //I find this helpful to understand better the process of Authentication / Authorization
                    var username = User.Identity.Name;
                    var roles = new[] { "GlobalManager" };

                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                      new System.Security.Principal.GenericIdentity(username, "Forms"), roles);
                }
                catch (Exception)
                {
                    //somehting went wrong
                }
            }
        }
    }
}
