using FootballTeams.Domain.TeamMembers;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Domain.Teams
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }

        public List<TeamMember> Members { get; set; }

        public Team()
        {
            Members = new List<TeamMember>();
        }
    }

    public static class TeamExtensions
    {
        public static TeamMember Manager(this Team team)
        {
            return team.Members?.FirstOrDefault(x => x.Role == MemberRole.Manager);
        }
        public static void AddManager(this Team team, TeamMember manager)
        {
            if (manager.Role == MemberRole.Manager)
            {
                team.RemoveManager();

                team.Members.Add(manager);
            }

            //todo: throw exception  if issues
        }
        public static void RemoveManager(this Team team)
        {
            if (team.Manager() != null)
            {
                team.Members.Remove(team.Manager());
            }

            //todo: throw exception  if issues
        }

        public static TeamMember Player(this Team team, int playerId)
        {
            return team.Members.FirstOrDefault(x => x.Role == MemberRole.Player && x.Id == playerId);
        }

        public static IEnumerable<TeamMember> Players(this Team team)
        {
            return team.Members.Where(x => x.Role == MemberRole.Player);
        }


        public static bool HasPlayer(this Team team, int playerId)
        {
            return team.Player(playerId) != null;
        }
        public static void AddPlayer(this Team team, TeamMember player)
        {
            if (player.Role == MemberRole.Player)
            {
                //todo: let's assume EF is taking care if the Id already exist
                team.Members.Add(player);
            }
        }
        public static void RemovePlayer(this Team team, TeamMember player)
        {
            team.Members.Remove(player);
        }
    }
}
