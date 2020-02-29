using AutoMapper;
using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Domain.Users;
using FootballTeams.Web.Models.ViewModels;

namespace FootballTeams.Web.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<TeamMember, TeamMemberViewModel>()
               .ReverseMap();

            CreateMap<Team, TeamViewModel>()
               .ReverseMap();

            CreateMap<Match, MatchViewModel>()
              .ReverseMap();
        }
    }
}