namespace FootballTeams.Domain.Matches
{
    public interface IMatchRepository : IGenericRepository<Match>
    {

    }
    public class MatchRepository : GenericRepository<Match>, IMatchRepository
    {
        public MatchRepository(IDatabaseContext db) : base(db)
        {
        }
    }
}
