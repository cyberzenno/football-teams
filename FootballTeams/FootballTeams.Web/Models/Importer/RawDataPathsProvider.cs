using FootballTeams.Data.Importer.Importers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace FootballTeams.Web.Models.Importer
{
    public class SampleRawDataPathsProvider : IRawDataPathsProvider
    {
        public string CountryCodesFilePath => "Content/RawData/CountryData/CountryCodes.html".GetFullPath();

        public string FifaCountryCodesFilePath => "Content/RawData/CountryData/FifaCountryCodes.html".GetFullPath();

        public string ItalianTeamsDirectory => "Content/RawData/LeagueData/Italy/_SampleData/Teams".GetFullPath();

        public string ItalianPlayersDirectory => "Content/RawData/LeagueData/Italy/_SampleData/Squad".GetFullPath();

        public string ItalianManagersDirectory => "Content/RawData/LeagueData/Italy/_SampleData/Managers".GetFullPath();
    }

    public class RealRawDataPathsProvider : IRawDataPathsProvider
    {
        public string CountryCodesFilePath => "Content/RawData/CountryData/CountryCodes.html".GetFullPath();

        public string FifaCountryCodesFilePath => "Content/RawData/CountryData/FifaCountryCodes.html".GetFullPath();

        public string ItalianTeamsDirectory => "Content/RawData/LeagueData/Italy/Teams".GetFullPath();

        public string ItalianPlayersDirectory => "Content/RawData/LeagueData/Italy/Squad".GetFullPath();

        public string ItalianManagersDirectory => "Content/RawData/LeagueData/Italy/Managers".GetFullPath();
    }
}