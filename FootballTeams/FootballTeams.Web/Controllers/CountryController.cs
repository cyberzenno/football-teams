using FootballTeams.Domain.Countries;
using FootballTeams.Web.Models.Helpers;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public ActionResult Index()
        {
            var countries = _countryRepository.GetAll();

            return View(countries.ToViewModel());
        }
    }
}