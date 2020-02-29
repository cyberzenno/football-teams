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

            //controller services
            container.Register(Component.For<ITeamControllerService>().ImplementedBy<TeamControllerService>());
        }
    }
}