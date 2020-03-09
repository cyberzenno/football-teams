using FootballTeams.Domain.Countries;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FootballTeams.Web.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CountryController : ApiController
    {
        private ICountryRepository _countryRepository;
        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [Route("api/pulse")]
        public IHttpActionResult GetPulse()
        {
            return Ok("Pulse OK");
        }


        [HttpGet]
        [Route("api/country/lookup/{search}")]
        public IHttpActionResult Lookup(string search)
        {
            var countries = _countryRepository
                .Get(x => x.ShortName.Contains(search) || x.ISO3.Contains(search) || x.ISO2.Contains(search))
                .Select(x => new
                {
                    x.ShortName,
                    x.ISO2,
                    x.ISO3
                }).ToList();

            return Ok(countries);
        }

        [HttpGet]
        [Route("api/country/search/{search}")]
        public IHttpActionResult Search(string search)
        {
            var countries = _countryRepository
                .Get(x => x.ShortName.Contains(search) || x.ISO3.Contains(search) || x.ISO2.Contains(search))
                .ToList();

            return Ok(countries);
        }

        [HttpGet]
        [Route("api/country/all")]
        public IHttpActionResult All()
        {
            var countries = _countryRepository.GetAll();

            return Ok(countries);
        }
    }
}