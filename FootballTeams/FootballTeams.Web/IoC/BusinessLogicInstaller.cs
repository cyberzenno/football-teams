using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using FootballTeams.Domain.Users;
using FootballTeams.Domain;
using FootballTeams.Data;
using FootballTeams.Web.Controllers;
using FootballTeams.Domain.Teams;
using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Countries;
using FootballTeams.Data.Importer.Importers;
using FootballTeams.Data.Importer.CountryData;
using FootballTeams.Web.Models.Importer;
using FootballTeams.Data.Importer.FootballData;

namespace FootballTeams.Web.IoC
{
    public class BusinessLogicInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //repos and data
            container.Register(Component.For<EF_DatabaseContext>());
            container.Register(Component.For<IDatabaseContext>().ImplementedBy<DatabaseContext>());

            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>());

            container.Register(Component.For<ITeamRepository>().ImplementedBy<TeamRepository>());
            container.Register(Component.For<ITeamMemberRepository>().ImplementedBy<TeamMemberRepository>());
            container.Register(Component.For<IMatchRepository>().ImplementedBy<MatchRepository>());

            container.Register(Component.For<ICountryRepository>().ImplementedBy<CountryRepository>());

            //controller services
            container.Register(Component.For<ITeamControllerService>().ImplementedBy<TeamControllerService>());


            //IMPORTERS
            container.Register(Component.For<IRawDataPathsProvider>().ImplementedBy<RealRawDataPathsProvider>());

            container.Register(Component.For<ICountryDataParser>().ImplementedBy<CountryDataParser>());
            container.Register(Component.For<ICountryDataImporter>().ImplementedBy<CountryDataImporter>());

            container.Register(Component.For<ITeamDataParser>().ImplementedBy<TeamDataParser>());
            container.Register(Component.For<ITeamDataImporter>().ImplementedBy<TeamDataImporter>());

            container.Register(Component.For<IPlayerDataParser>().ImplementedBy<PlayerDataParser>());
            container.Register(Component.For<IPlayerDataImporter>().ImplementedBy<PlayerDataImporter>());

            container.Register(Component.For<IManagerDataParser>().ImplementedBy<ManagerDataParser>());
            container.Register(Component.For<IManagerDataImporter>().ImplementedBy<ManagerDataImporter>());
        }
    }
}