namespace FootballTeams.Domain.Teams
{
    public interface ITeamRepository : IGenericRepository<Team>
    {
       
    }

    public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(IDatabaseContext db) : base(db)
        {

        }
    }
}
