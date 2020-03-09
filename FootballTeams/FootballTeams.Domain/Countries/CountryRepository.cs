namespace FootballTeams.Domain.Countries
{
    public interface ICountryRepository : IGenericRepository<Country>
    {

    }
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(IDatabaseContext db) : base(db)
        {
        }
    }
}
