using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballTeams.Web.Test.Unit.TeamControllerServiceTests
{
    [TestFixture]
    public class TeamControllerServiceFixture
    {
        private readonly ITeamControllerService _teamControllerService;

        private readonly IMock<ITeamMemberRepository> _memberRepository;
        private readonly IMock<ITeamRepository> _teamRepository;
        private readonly IMock<IMatchRepository> _matchRepository;


        public TeamControllerServiceFixture()
        {
            _memberRepository = new Mock<ITeamMemberRepository>();
            _teamRepository = new Mock<ITeamRepository>();
            _matchRepository = new Mock<IMatchRepository>();

            _teamControllerService = new TeamControllerService(_memberRepository.Object, _teamRepository.Object, _matchRepository.Object);
        }

        [Test]
        public void SmokeTest()
        {
            //arrange


            //act
            var actual = _teamControllerService.GetTeam(0);

            //assert
            Assert.IsNull(actual);
        }

        //todo: add relevant unit tests
    }
}
