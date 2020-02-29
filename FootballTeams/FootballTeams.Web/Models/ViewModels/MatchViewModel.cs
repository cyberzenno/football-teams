namespace FootballTeams.Web.Models.ViewModels
{
    public class MatchViewModel
    {
        public int? Id { get; set; }

        public int? TeamHomeId { get; set; }
        public int? TeamHomeScore { get; set; }
        public TeamViewModel TeamHome { get; set; }

        public int? TeamAwayId { get; set; }
        public int? TeamAwayScore { get; set; }
        public TeamViewModel TeamAway { get; set; }
    }
}