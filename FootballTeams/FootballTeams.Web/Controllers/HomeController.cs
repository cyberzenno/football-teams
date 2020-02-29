using FootballTeams.Domain.Users;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUserRepository userRepository)
        {
            ViewBag.DbName = userRepository.DbName;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VddDiagram()
        {
            var vddDiagramPath = Server.MapPath("~/App_Data/FootballTeams_VDD.pdf");

            return File(vddDiagramPath, "application/pdf");
        }
    }
}