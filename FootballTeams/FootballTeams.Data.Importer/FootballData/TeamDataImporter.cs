using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.Teams;
using System.Collections.Generic;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface ITeamDataImporter : IDataImporter<Team>
    {

    }

    public class TeamDataImporter : DataImporter<Team>, ITeamDataImporter
    {
        public TeamDataImporter(
            IRawDataPathsProvider rawDataProvider,
            ITeamDataParser rawDataParser,
            ITeamRepository dataRepository)
            : base(rawDataProvider, rawDataParser, dataRepository)
        {
        }

        public override List<Team> GetAllRecords()
        {
            var rawfiles = GetFiles(_rawDataPaths.ItalianTeamsDirectory);

            var teams = new List<Team>();

            foreach (var file in rawfiles)
            {
                teams.AddRange(_rawDataParser.ParseFile(file));
            }

            return teams;
        }
    }
}
