using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Domain.TeamMembers
{
    public interface ITeamMemberRepository : IGenericRepository<TeamMember>
    {
        //players
        TeamMember GetPlayer(int playerId);
        IEnumerable<TeamMember> GetPlayers();
        IEnumerable<TeamMember> GetPlayers(string includePath);
        TeamMember GetPlayerWithNoTeam(int playerId);
        IEnumerable<TeamMember> GetPlayersWithNoTeam();
        IEnumerable<TeamMember> GetPlayersWithNoTeamOrWithGivenTeam(int teamId);


        //managers
        TeamMember GetManager(int managerId);
        IEnumerable<TeamMember> GetManagers();
        IEnumerable<TeamMember> GetManagers(string includePath);
    }

    public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
    {
        public TeamMemberRepository(IDatabaseContext db) : base(db)
        {

        }
        public TeamMember GetPlayer(int playerId)
        {
            return Get(x => x.Role == MemberRole.Player && x.Id == playerId).FirstOrDefault();
        }

        public IEnumerable<TeamMember> GetPlayers()
        {
            return Get(x => x.Role == MemberRole.Player);
        }

        public IEnumerable<TeamMember> GetPlayers(string includePath)
        {
            return Get(x => x.Role == MemberRole.Player, includePath);
        }

        public TeamMember GetPlayerWithNoTeam(int playerId)
        {
            return Get(x => x.Role == MemberRole.Player && x.Team == null && x.Id == playerId).FirstOrDefault();
        }

        public IEnumerable<TeamMember> GetPlayersWithNoTeam()
        {
            return Get(x => x.Role == MemberRole.Player && x.Team == null);
        }

        public IEnumerable<TeamMember> GetPlayersWithNoTeamOrWithGivenTeam(int teamId)
        {
            return Get(x => x.Role == MemberRole.Player && (x.Team == null || x.Team.Id == teamId));
        }

        public TeamMember GetManager(int managerId)
        {
            return Get(x => x.Role == MemberRole.Manager && x.Id == managerId).FirstOrDefault();
        }

        public IEnumerable<TeamMember> GetManagers()
        {
            return Get(x => x.Role == MemberRole.Manager);
        }

        public IEnumerable<TeamMember> GetManagers(string includePath)
        {
            return Get(x => x.Role == MemberRole.Manager, includePath);
        }
    }
}
