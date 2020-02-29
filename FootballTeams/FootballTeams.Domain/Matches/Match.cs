using FootballTeams.Domain.Teams;

namespace FootballTeams.Domain.Matches
{
    public class Match
    {
        public int Id { get; set; }

        public int? TeamHomeId { get; set; }
        public int? TeamHomeScore { get; set; }
        public virtual Team TeamHome { get; set; }

        public int? TeamAwayId { get; set; }
        public int? TeamAwayScore { get; set; }
        public virtual Team TeamAway { get; set; }
    }
}
