using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FootballTeams.Data.Test.Integration
{
    [TestClass]
    public class CrudTeams
    {
        private ITeamRepository _teamRepository;
        private ITeamMemberRepository _memberRepository;

        [TestInitialize]
        public void Setup()
        {
            var ef = new EF_DatabaseContext();

            var db = new DatabaseContext(ef);

            _teamRepository = new TeamRepository(db);
            _memberRepository = new TeamMemberRepository(db);
        }

        //note: as it is an Integration Test, we prefer to avoid being ran by mistake
        //with the Ignore attribute, it runs only if specifically requested
        [Ignore]
        [TestMethod]
        public void AddTeamThenManagerAndPlayers()
        {
            //arrange
            var team = new Team
            {
                Name = "Team 1"
            };
            _teamRepository.Add(team);

            var manager = new TeamMember
            {
                Fullname = "Manager 1",
                Role = MemberRole.Manager

            };
            _memberRepository.Add(manager);

            var player = new TeamMember
            {
                Fullname = "Player 1",
                Role = MemberRole.Player

            };
            _memberRepository.Add(player);

            var insertedTeam = _teamRepository.Get(team.Id);
            var insertedManager = _memberRepository.GetManager(manager.Id);
            var insertedPlayer = _memberRepository.GetPlayer(player.Id);

            insertedTeam.AddManager(insertedManager);
            insertedTeam.AddPlayer(insertedPlayer);

            //act
            _teamRepository.Update(insertedTeam);

            var updatedTeam = _teamRepository.Get(team.Id);
            var updatedManager = _memberRepository.GetManager(manager.Id);
            var updatedPlayer = _memberRepository.GetPlayer(player.Id);

            //assert
            Assert.IsTrue(updatedTeam.Manager().Id == manager.Id);
            Assert.IsTrue(updatedManager.Team.Id == team.Id);

            Assert.IsTrue(updatedTeam.Player(player.Id) != null);
            Assert.IsTrue(updatedPlayer.Team.Id == team.Id);
        }

    }
}
