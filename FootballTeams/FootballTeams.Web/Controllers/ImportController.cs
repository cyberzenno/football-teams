using FootballTeams.Data.Importer.CountryData;
using FootballTeams.Data.Importer.FootballData;
using FootballTeams.Domain.Countries;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Web.Models.Helpers;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class ImportController : BaseController
    {
        private readonly ICountryDataImporter countryDataImporter;
        private readonly ITeamDataImporter teamDataImporter;
        private readonly IPlayerDataImporter playerDataImporter;
        private readonly IManagerDataImporter managerDataImporter;


        public ImportController(
            ICountryDataImporter countryDataImporter,
            ITeamDataImporter teamDataImporter,
            IPlayerDataImporter playerDataImporter,
            IManagerDataImporter managerDataImporter)
        {
            this.countryDataImporter = countryDataImporter;
            this.teamDataImporter = teamDataImporter;
            this.playerDataImporter = playerDataImporter;
            this.managerDataImporter = managerDataImporter;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FifaCountry()
        {
            var fifaCountries = countryDataImporter.GetAllFifaRecords();

            return View("Country", fifaCountries.ToViewModel());
        }

        public ActionResult Country()
        {
            var countries = countryDataImporter.GetAllRecords();

            return View(countries.ToViewModel());
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalManager")]
        public ActionResult CountryImport()
        {
            var countries = countryDataImporter.GetAllRecords();

            countryDataImporter.SaveRecords(countries);

            AlertSuccess("Countries Imported", "It should be all good :-)");

            return RedirectToAction("Index", "Country");
        }

        public ActionResult Team()
        {
            var teams = teamDataImporter.GetAllRecords();

            return View(teams.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalManager")]
        public ActionResult TeamImport()
        {
            var teams = teamDataImporter.GetAllRecords();

            teamDataImporter.SaveRecords(teams);

            AlertSuccess("Teams Imported", "Keep rockin'!");
            
            return RedirectToAction("Index", "Team");
        }

        public ActionResult Player()
        {
            var players = playerDataImporter.GetAllRecords();

            return View(players.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalManager")]
        public ActionResult PlayerImport()
        {
            var players = playerDataImporter.GetAllRecords();

            playerDataImporter.SaveRecords(players);

            AlertSuccess("Players Imported", "Roll Baby, roll.");

            return RedirectToAction("Index", "Player");
        }

        public ActionResult Manager()
        {
            var managers = managerDataImporter.GetAllRecords();

            return View(managers.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GlobalManager")]
        public ActionResult ManagerImport()
        {
            var managers = managerDataImporter.GetAllRecords();

            managerDataImporter.SaveRecords(managers);

            AlertSuccess("Managers Imported", "Well Done! Here some tits: (. )( .)");

            return RedirectToAction("Index", "Manager");
        }
    }
}