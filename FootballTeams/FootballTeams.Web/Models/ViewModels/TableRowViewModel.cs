namespace FootballTeams.Web.Models.ViewModels
{
    public class TableRowViewModel
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int Matches { get; set; }
    }
}