using FootballTeams.Domain.Teams;
using FootballTeams.Web.Models.Helpers;
using FootballTeams.Web.Models.ViewModels;
using System.Web.Mvc;
using FootballTeams.Domain.TeamMembers;

namespace FootballTeams.Web.Controllers
{
    public class ManagerController : BaseController
    {
        private readonly ITeamMemberRepository _memberRepository;
        private readonly ITeamRepository _teamRepository;

        public ManagerController(ITeamMemberRepository memeberRespository, ITeamRepository teamRepository)
        {
            _memberRepository = memeberRespository;
            _teamRepository = teamRepository;
        }

        public ActionResult Index()
        {
            var managers = _memberRepository.GetManagers();

            return View(managers.ToViewModel());
        }

        public ActionResult Details(int id)
        {
            var vmManager = _memberRepository.GetManager(id).ToViewModel();

            return View(vmManager);
        }

        [Authorize(Roles = "GlobalManager")]
        public ActionResult Register()
        {
            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            return View();
        }

        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TeamMemberViewModel vmManager)
        {
            if (ModelState.IsValid)
            {
                var manager = vmManager.ToDataModel();

                manager.Role = MemberRole.Manager;

                _memberRepository.Add(manager);

                AlertSuccess("Saved", "Manager registered");
                return RedirectToAction("Index");
            }

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            AlertModelStateErrors();

            return View(vmManager);
        }


        [Authorize(Roles = "GlobalManager")]
        public ActionResult Edit(int id)
        {
            var vmManager = _memberRepository.GetManager(id).ToViewModel();

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            return View(vmManager);
        }

        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamMemberViewModel vmManager)
        {
            if (ModelState.IsValid)
            {
                var existingManager = _memberRepository.GetManager(vmManager.Id);

                existingManager.Fullname = vmManager.Fullname;
                existingManager.Nationality = vmManager.Nationality;
                existingManager.DateOfBirth = vmManager.DateOfBirth;

                existingManager.Position = vmManager.Position;
                existingManager.Role = vmManager.Role;
                existingManager.TeamId = vmManager.TeamId;

                _memberRepository.Update(existingManager);

                if (vmManager.TeamId.HasValue)
                {
                    var existingTeam = _teamRepository.Get(vmManager.TeamId.Value);

                    existingTeam.AddManager(existingManager);

                    _teamRepository.Update(existingTeam);
                }


                AlertSuccess("Saved", "Manager have been changed to Player");
                return RedirectToAction("Index");

            }

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            AlertDanger("User Role error", "Unknown use role path");
            return RedirectToAction("Index");
        }


    }
}