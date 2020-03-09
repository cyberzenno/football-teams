using FootballTeams.Web.Models.ViewModels;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class TeamController : BaseController
    {
        ITeamControllerService _teamControllerService;

        public TeamController(ITeamControllerService teamControllerService)
        {
            _teamControllerService = teamControllerService;
        }

        public ActionResult Index()
        {
            var vmTeams = _teamControllerService.GetAllTeams();

            return View(vmTeams);
        }
        public ActionResult Tables()
        {
            var vmTableRows = _teamControllerService.GetTableRows();

            return View(vmTableRows);
        }
        public ActionResult Details(int id)
        {
            var vmTeam = _teamControllerService.GetTeam(id);

            return View(vmTeam);
        }

        [Authorize(Roles = "GlobalManager")]
        public ActionResult Create()
        {
            ViewBag.AvailableManagers = _teamControllerService.GetAllManagers();
            ViewBag.AvailablePlayers = _teamControllerService.GetAvailablePlayers();

            return View();
        }


        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel vmTeam)
        {
            if (ModelState.IsValid)
            {
                TeamViewModel createdTeam = _teamControllerService.Create(vmTeam);

                AlertSuccess("Saved", "Team created correctly");

                return RedirectToAction("Details", new { id = createdTeam.Id });
            }

            //todo: optimize to not ask for everytime for the same data
            ViewBag.AvailableManagers = _teamControllerService.GetAllManagers();
            ViewBag.AvailablePlayers = _teamControllerService.GetAvailablePlayers();

            AlertModelStateErrors();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "GlobalManager")]
        public ActionResult Edit(int id)
        {
            var vmTeamToEdit = _teamControllerService.GetTeam(id);

            if (vmTeamToEdit != null)
            {
                ViewBag.AvailableManagers = _teamControllerService.GetAllManagers();
                ViewBag.AvailablePlayers = _teamControllerService.GetAvailablePlayers(id);

                return View(vmTeamToEdit);
            }

            AlertDanger("Not Found", "Team not found.");

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamViewModel vmTeam)
        {
            if (ModelState.IsValid)
            {
                TeamViewModel updatedTeam = _teamControllerService.Update(vmTeam);

                AlertSuccess("Saved", "Team saved correctly");

                return RedirectToAction("Index");
            }

            AlertModelStateErrors();

            return RedirectToAction("Edit", new { id = vmTeam.Id });
        }
    }
}