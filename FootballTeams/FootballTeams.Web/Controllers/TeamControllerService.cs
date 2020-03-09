using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Web.Models.Helpers;
using FootballTeams.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Web.Controllers
{
    public interface ITeamControllerService
    {
        TeamViewModel GetTeam(int id);
        IEnumerable<TeamViewModel> GetAllTeams();
        IEnumerable<TeamMemberViewModel> GetAllManagers();
        IEnumerable<TeamMemberViewModel> GetAvailablePlayers();
        IEnumerable<TeamMemberViewModel> GetAvailablePlayers(int teamId);

        IEnumerable<TableRowViewModel> GetTableRows();

        TeamViewModel Create(TeamViewModel vmTeam);
        TeamViewModel Update(TeamViewModel vmTeam);
    }

    public class TeamControllerService : ITeamControllerService
    {
        private readonly ITeamMemberRepository _memberRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMatchRepository _matchRepository;

        public TeamControllerService(ITeamMemberRepository memberRepository, ITeamRepository teamRepository, IMatchRepository matchRepository)
        {
            _memberRepository = memberRepository;
            _teamRepository = teamRepository;
            _matchRepository = matchRepository;
        }

        public TeamViewModel GetTeam(int id)
        {
            var team = _teamRepository.Get(id);

            if (team == null)
            {
                return null;
            }

            var vmTeam = _teamRepository.Get(id).ToViewModel();

            var matches = _matchRepository.GetAll().ToList();

            vmTeam.Matches = matches
                .Where(x => x.TeamHomeId == vmTeam.Id || x.TeamAwayId == vmTeam.Id)
                .ToViewModel()
                .ToList();

            return vmTeam;
        }

        public IEnumerable<TeamViewModel> GetAllTeams()
        {
            var teams = _teamRepository.GetAll("Members").ToList();

            var vmTeams = teams.ToViewModel().ToList();

            var matches = _matchRepository.GetAll().ToList();

            foreach (var vmTeam in vmTeams)
            {
                var matchesForTeam = matches.Where(x => x.TeamHomeId == vmTeam.Id || x.TeamAwayId == vmTeam.Id).ToList();

                vmTeam.Matches = matchesForTeam.ToViewModel().ToList();
            }

            return vmTeams;
        }

        public IEnumerable<TeamMemberViewModel> GetAllManagers()
        {
            var managers = _memberRepository.GetManagers();

            var vmManagers = managers.ToViewModel();

            return vmManagers;
        }

        public IEnumerable<TeamMemberViewModel> GetAvailablePlayers()
        {
            var playersWithNoTeam = _memberRepository.GetPlayersWithNoTeam();

            var vmPlayersWithNoTeam = playersWithNoTeam.ToViewModel();

            return vmPlayersWithNoTeam;
        }

        public IEnumerable<TeamMemberViewModel> GetAvailablePlayers(int teamId)
        {
            var playersAvailableForGivenTeam = _memberRepository.GetPlayersWithNoTeamOrWithGivenTeam(teamId);

            var vmPlayersAvailableForGivenTeam = playersAvailableForGivenTeam.ToViewModel();

            return vmPlayersAvailableForGivenTeam;
        }

        public IEnumerable<TableRowViewModel> GetTableRows()
        {
            var position = 1;
            var vmTableRowsViewModel = from team in GetAllTeams()
                                       orderby team.Points() descending,
                                       team.NumberOfMatchesPlayed() ascending
                                       select new TableRowViewModel
                                       {
                                           TeamId = team.Id,
                                           TeamName = team.Name,
                                           TeamImageUrl = team.ImageUrl,
                                           Position = position++,
                                           Points = team.Points(),
                                           Matches = team.NumberOfMatchesPlayed()
                                       };

            return vmTableRowsViewModel;
        }

        public TeamViewModel Create(TeamViewModel vmTeam)
        {
            var team = vmTeam.ToDataModel();

            team.AdditionalDetailsJson = AdditionalDetailsFactory.CreateFootbalTeamsLocalCreationDetails().ToJson();

            if (vmTeam.ManagerId.HasValue)
            {
                var manager = _memberRepository.GetManager(vmTeam.ManagerId.Value);

                team.AddManager(manager);
            }

            foreach (var playerId in vmTeam.SelectedPlayerIds)
            {
                var player = _memberRepository.GetPlayerWithNoTeam(playerId);

                if (player != null)
                {
                    team.AddPlayer(player);
                }
            }

            _teamRepository.Add(team);

            return team.ToViewModel();
        }

        public TeamViewModel Update(TeamViewModel vmTeam)
        {
            var existingTeam = _teamRepository.Get(vmTeam.Id);

            existingTeam.Name = vmTeam.Name;
            existingTeam.Address = vmTeam.Address;
            existingTeam.City = vmTeam.City;
            existingTeam.Country = vmTeam.Country;
            existingTeam.ImageUrl = vmTeam.ImageUrl;

            if (vmTeam.ManagerId != null)
            {
                var managerToUpdate = _memberRepository.GetManager(vmTeam.ManagerId.Value);

                if (managerToUpdate.Role == MemberRole.Manager)
                {
                    existingTeam.AddManager(managerToUpdate);
                }
            }
            else
            {
                existingTeam.RemoveManager();
            }

            //unselect unselected players
            foreach (var player in existingTeam.Players().ToList())
            {
                if (!vmTeam.HasSelectedPlayer(player.Id))
                {
                    existingTeam.RemovePlayer(player);
                }
            }

            //select selected players
            foreach (var selectedPlayerId in vmTeam.SelectedPlayerIds)
            {
                if (!existingTeam.HasPlayer(selectedPlayerId))
                {
                    //todo: investigate EF bulk select
                    //in this way, we try to call less times the user repo
                    var playerToBeAdded = _memberRepository.GetPlayerWithNoTeam(selectedPlayerId);

                    if (playerToBeAdded != null)
                    {
                        existingTeam.AddPlayer(playerToBeAdded);
                    }
                }
            }

            _teamRepository.Update(existingTeam);

            return existingTeam.ToViewModel();
        }
    }
}