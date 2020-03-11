namespace FootballTeams.Domain.Countries
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExtendedName { get; set; }
        public string OfficialName { get; set; }
        public string FIFA { get; set; }
        public string ISO3 { get; set; }
        public string ISO2 { get; set; }
        public string UNI { get; set; }

        public string FifaAssociation { get; set; }
        public string FifaAssociationUrl { get; set; }

        public string OriginUrl { get; set; }
    }
}
