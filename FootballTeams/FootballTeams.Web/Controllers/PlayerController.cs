using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Web.Models.Helpers;
using FootballTeams.Web.Models.ViewModels;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class PlayerController : BaseController
    {
        private readonly ITeamMemberRepository _memeberRespository;
        private readonly ITeamRepository _teamRepository;

        public PlayerController(ITeamMemberRepository memeberRespository, ITeamRepository teamRepository)
        {
            _memeberRespository = memeberRespository;
            _teamRepository = teamRepository;
        }

        public ActionResult Index()
        {
            var vmPlayers = _memeberRespository.GetPlayers().ToViewModel();

            return View(vmPlayers);
        }

        public ActionResult Details(int id)
        {
            var vmPlayer = _memeberRespository.GetPlayer(id).ToViewModel();

            return View(vmPlayer);
        }

        [Authorize(Roles = "GlobalManager")]
        public ActionResult Register()
        {
            //todo: implement proper functionality into TeamController
            //and get ajax results or so
            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            return View();
        }

        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TeamMemberViewModel vmPlayer)
        {
            if (ModelState.IsValid)
            {
                vmPlayer.Role = MemberRole.Player;

                _memeberRespository.Add(vmPlayer.ToDataModel());

                AlertSuccess("Saved", "Player registered");
                return RedirectToAction("Index");
            }

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            AlertModelStateErrors();

            return View(vmPlayer);
        }

        [Authorize(Roles = "GlobalManager")]
        public ActionResult Edit(int id)
        {
            var vmPlayer = _memeberRespository.GetPlayer(id).ToViewModel();

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            return View(vmPlayer);
        }

        [Authorize(Roles = "GlobalManager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamMemberViewModel vmPlayer)
        {
            var existingPlayer = _memeberRespository.GetPlayer(vmPlayer.Id);

            if (ModelState.IsValid)
            {
                existingPlayer.Fullname = vmPlayer.Fullname;
                existingPlayer.Nationality = vmPlayer.Nationality;
                existingPlayer.DateOfBirth = vmPlayer.DateOfBirth;

                existingPlayer.Position = vmPlayer.Position;
                existingPlayer.Role = vmPlayer.Role;
                existingPlayer.TeamId = vmPlayer.TeamId;

                _memeberRespository.Update(existingPlayer);

                AlertSuccess("Saved", "Player updated");

                return RedirectToAction("Index");
            }

            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            AlertModelStateErrors();

            return View(vmPlayer);
        }
    }
}