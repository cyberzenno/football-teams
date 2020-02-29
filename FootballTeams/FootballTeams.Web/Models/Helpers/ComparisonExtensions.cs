using FootballTeams.Domain.TeamMembers;
using FootballTeams.Web.Models.ViewModels;

namespace FootballTeams.Web.Models.Helpers
{
    public static class ComparisonExtensions
    {
        public static  bool HasTeam(this TeamMember member)
        {
            return member.TeamId.HasValue;
        }

        public static bool HasTeam(this TeamMemberViewModel vmTeamMember)
        {
            return vmTeamMember.TeamId.HasValue;
        }

        public static  bool TeamHasBeenAdded(this TeamMember existingTeamMember, TeamMemberViewModel vmTeamMember)
        {
            return !existingTeamMember.TeamId.HasValue && vmTeamMember.TeamId.HasValue;
        }

        public static  bool TeamHasChanged(this TeamMember existingTeamMember, TeamMemberViewModel vmTeamMember)
        {
            return existingTeamMember.TeamId.HasValue && vmTeamMember.TeamId.HasValue
                && existingTeamMember.TeamId != vmTeamMember.TeamId;
        }

        public static  bool TeamHasBeenRemoved(this TeamMember existingTeamMember, TeamMemberViewModel vmTeamMember)
        {
            return existingTeamMember.TeamId.HasValue && !vmTeamMember.TeamId.HasValue;
        }

        public static  bool IsStillManager(this TeamMember existingTeamMember, TeamMemberViewModel vmTeamMember)
        {
            return existingTeamMember.Role == MemberRole.Manager && vmTeamMember.Role == MemberRole.Manager;
        }

        public static bool IsNone(this TeamMemberViewModel memberViewModel)
        {
            return memberViewModel.Role == MemberRole.None;
        }

        public static  bool IsPlayer(this TeamMemberViewModel memberViewModel)
        {
            return memberViewModel.Role == MemberRole.Player;
        }

        public static bool IsManager(this TeamMemberViewModel memberViewModel)
        {
            return memberViewModel.Role == MemberRole.Manager;
        }

        public static  bool HasChangedFromManagerToPlayer(this TeamMemberViewModel memberViewModel, TeamMember existingTeamMember)
        {
            return
                existingTeamMember.Role == MemberRole.Manager &&
                memberViewModel.Role == MemberRole.Player;
        }

        public static  bool HasChangedFromPlayerToManager(this TeamMemberViewModel memberViewModel, TeamMember existingTeamMember)
        {
            return
               existingTeamMember.Role == MemberRole.Player &&
               memberViewModel.Role == MemberRole.Manager;
        }
    }
}