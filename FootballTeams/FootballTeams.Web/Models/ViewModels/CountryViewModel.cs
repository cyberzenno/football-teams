namespace FootballTeams.Web.Models.ViewModels
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string OfficialName { get; set; }
        public string ISO3 { get; set; }
        public string ISO2 { get; set; }
        public string UNI { get; set; }

        public string FIFA { get; set; }
        public string FifaAssociation { get; set; }

        public string OriginUrl { get; set; }
        public string FlagUrl { get; set; }
        public string FlagBase64 { get; set; }
    }
}