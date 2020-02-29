using FootballTeams.Domain.Teams;
using System;

namespace FootballTeams.Domain.TeamMembers
{
    public enum MemberRole
    {
        None,
        Player,
        Manager
    }

    public enum PlayerPosition
    {
        None,
        Goalkeeper,
        Defender,
        Midfielder,
        Forward
    }

    public class TeamMember
    {
        public int Id { get; set; }

        public string Fullname { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }


        public MemberRole Role { get; set; }
        public PlayerPosition Position { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
