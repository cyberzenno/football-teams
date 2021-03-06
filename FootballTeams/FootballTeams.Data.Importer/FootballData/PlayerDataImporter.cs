﻿using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.Countries;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using System.Collections.Generic;
using System.Linq;
using FootballTeams.Domain.AdditionalDetail;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface IPlayerDataImporter : IDataImporter<TeamMember>
    {
    }

    public class PlayerDataImporter : DataImporter<TeamMember>, IPlayerDataImporter
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICountryRepository _countryRepository;

        public PlayerDataImporter(
            IRawDataPathsProvider rawDataProvider,
            IPlayerDataParser rawDataParser,
            ITeamMemberRepository dataRepository,
            ITeamRepository teamRepository,
            ICountryRepository countryRepository)
            : base(rawDataProvider, rawDataParser, dataRepository)
        {
            _teamRepository = teamRepository;
            _countryRepository = countryRepository;
        }


        public override List<TeamMember> GetAllRecords()
        {
            var rawfiles = GetFiles(_rawDataPaths.ItalianPlayersDirectory);

            var players = new List<TeamMember>();

            var allTeams = _teamRepository.GetAll().ToList();
            var allCountries = _countryRepository.GetAll().ToList();

            foreach (var file in rawfiles)
            {
                var filePlayers = _rawDataParser.ParseFile(file);
                var team = allTeams.FirstOrDefault(x => x.Name == filePlayers[0].Team.Name);

                foreach (var p in filePlayers)
                {
                    //trying to set the actual domain team for the player

                    //todo: re-enable the team
                    p.Team = team;

                    //trying to get the coutry
                    var country = TryGetCountry(p.AdditionalDetailsJson, allCountries);
                    if (country != null)
                    {
                        p.Nationality = country.Name;
                        p.NationalityFIFA = country.FIFA;
                    }

                }

                players.AddRange(filePlayers);
            }

            return players;
        }


        private Country TryGetCountry(string additionalDetailsJson, List<Country> allCountries)
        {
            try
            {
                var nationalityFIFA = additionalDetailsJson.ToAdditionalDetailsDataModel().GetDetailValueOrDefault("Nationality");

                return allCountries.FirstOrDefault(x => x.FIFA == nationalityFIFA);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
