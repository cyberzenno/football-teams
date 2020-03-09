using AutoMapper;
using FootballTeams.Domain.Countries;
using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Domain.Users;
using FootballTeams.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Web.Models.Helpers
{
    public static class MappingExtensions
    {
        //User
        public static User ToDataModel(this UserViewModel viewModel)
        {
            return Mapper.Map<User>(viewModel);
        }

        public static UserViewModel ToViewModel(this User dataModel)
        {
            return Mapper.Map<UserViewModel>(dataModel);
        }


        //TeamMember
        public static TeamMember ToDataModel(this TeamMemberViewModel viewModel)
        {
            return Mapper.Map<TeamMember>(viewModel);
        }

        public static TeamMemberViewModel ToViewModel(this TeamMember dataModel)
        {
            return Mapper.Map<TeamMemberViewModel>(dataModel);
        }

        public static IEnumerable<TeamMember> ToDataModel(this IEnumerable<TeamMemberViewModel> viewModel)
        {
            return Mapper.Map<IEnumerable<TeamMember>>(viewModel);
        }

        public static IEnumerable<TeamMemberViewModel> ToViewModel(this IEnumerable<TeamMember> dataModel)
        {
            return Mapper.Map<IEnumerable<TeamMemberViewModel>>(dataModel);
        }


        //Team
        public static Team ToDataModel(this TeamViewModel viewModel)
        {
            return Mapper.Map<Team>(viewModel);
        }

        public static TeamViewModel ToViewModel(this Team dataModel)
        {
            var vmTeam = Mapper.Map<TeamViewModel>(dataModel);

            vmTeam.ManagerId = dataModel.Manager()?.Id;

            return vmTeam;
        }

        public static IEnumerable<Team> ToDataModel(this IEnumerable<TeamViewModel> viewModel)
        {
            return Mapper.Map<IEnumerable<Team>>(viewModel);
        }

        public static IEnumerable<TeamViewModel> ToViewModel(this IEnumerable<Team> dataModel)
        {
            return dataModel.Select(x => x.ToViewModel());
        }


        //Match
        public static Match ToDataModel(this MatchViewModel viewModel)
        {
            return Mapper.Map<Match>(viewModel);
        }

        public static MatchViewModel ToViewModel(this Match dataModel)
        {
            return Mapper.Map<MatchViewModel>(dataModel);
        }

        public static IEnumerable<Match> ToDataModel(this IEnumerable<MatchViewModel> viewModel)
        {
            return Mapper.Map<IEnumerable<Match>>(viewModel);
        }

        public static IEnumerable<MatchViewModel> ToViewModel(this IEnumerable<Match> dataModel)
        {
            return Mapper.Map<IEnumerable<MatchViewModel>>(dataModel);
        }


        //Country
        public static IEnumerable<Country> ToDataModel(this IEnumerable<CountryViewModel> viewModel)
        {
            return Mapper.Map<IEnumerable<Country>>(viewModel);
        }

        public static IEnumerable<CountryViewModel> ToViewModel(this IEnumerable<Country> dataModel)
        {
            return Mapper.Map<IEnumerable<CountryViewModel>>(dataModel);
        }

    }
}