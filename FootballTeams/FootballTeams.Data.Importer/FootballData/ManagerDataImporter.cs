using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface IManagerDataImporter : IDataImporter<TeamMember>
    {
    }

    public class ManagerDataImporter : DataImporter<TeamMember>, IManagerDataImporter
    {
        private readonly ITeamRepository _teamRepository;

        public ManagerDataImporter(
            IRawDataPathsProvider rawDataProvider,
            IManagerDataParser rawDataParser,
            ITeamMemberRepository dataRepository,
            ITeamRepository teamRepository)
            : base(rawDataProvider, rawDataParser, dataRepository)
        {
            _teamRepository = teamRepository;
        }


        public override List<TeamMember> GetAllRecords()
        {
            var rawfiles = GetFiles(_rawDataPaths.ItalianManagersDirectory);

            var managers = new List<TeamMember>();

            var allTeams = _teamRepository.GetAll().ToList();

            foreach (var file in rawfiles)
            {
                var fileManager = _rawDataParser.ParseFile(file);
                var team = allTeams.FirstOrDefault(x => x.Name == fileManager[0].Team.Name);

                foreach (var m in fileManager)
                {
                    //trying to set the actual domain team for the player

                    //todo: re-enable the team
                    m.Team = team;
                }

                managers.AddRange(fileManager);
            }

            return managers;
        }
    }
}
