using FootballTeams.Domain.Matches;
using FootballTeams.Domain.Teams;
using FootballTeams.Web.Models.Helpers;
using FootballTeams.Web.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class MatchController : BaseController
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchRepository _matchRepository;

        public MatchController(ITeamRepository teamRepository, IMatchRepository matchRepository)
        {
            _teamRepository = teamRepository;
            _matchRepository = matchRepository;
        }

        public ActionResult Index()
        {
            var matches = _matchRepository.GetAll().ToList();

            var vmMatches = matches.ToViewModel();

            //todo: move Teams List to separate action and call it via ajax
            //needed to create or update a match
            ViewBag.AvailableTeams = _teamRepository.GetAll().ToViewModel();

            return View(vmMatches);
        }

        [HttpPost]
        [Authorize(Roles = "GlobalManager")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MatchViewModel vmMatch)
        {
            if (ModelState.IsValid)
            {
                var matchToAdd = vmMatch.ToDataModel();

                _matchRepository.Add(matchToAdd);

                AlertSuccess("Match created", "New match added.");

                return RedirectToAction("Index");
            }

            AlertModelStateErrors();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "GlobalManager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MatchViewModel vmMatch)
        {
            var matchToUpdate = _matchRepository.Get(vmMatch.Id.Value);

            matchToUpdate.TeamHomeId = vmMatch.TeamHomeId;
            matchToUpdate.TeamHomeScore = vmMatch.TeamHomeScore;
            matchToUpdate.TeamAwayId = vmMatch.TeamAwayId;
            matchToUpdate.TeamAwayScore = vmMatch.TeamAwayScore;

            _matchRepository.Update(matchToUpdate);

            AlertSuccess("Match updated", "Match updated correctly");

            return RedirectToAction("Index");
        }
    }
}