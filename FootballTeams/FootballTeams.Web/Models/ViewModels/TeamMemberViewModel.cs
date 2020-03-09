using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.TeamMembers;
using System;
using System.Web.Mvc;
using FootballTeams.Domain.AdditionalDetail;

namespace FootballTeams.Web.Models.ViewModels
{
    public class TeamMemberViewModel
    {
        public int Id { get; set; }

        public string Fullname { get; set; }
        public string Nationality { get; set; }
        public string NationalityISO2 { get; set; }
        public string NationalityISO3 { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int? TeamId { get; set; }
        public TeamViewModel Team { get; set; }

        public MemberRole Role { get; set; }
        public PlayerPosition Position { get; set; }

        public int? Number { get; set; }
        public string AdditionalDetailsJson { get; set; }
        public AdditionalDetails AdditionalDetails
        {
            get
            {
                return AdditionalDetailsJson.ToAdditionalDetailsDataModel();
            }
        }
    }

    public static class TeamMemberViewModelExtensions
    {
        public static int? Age(this TeamMemberViewModel vmTeamMember)
        {
            if (vmTeamMember.DateOfBirth.HasValue)
            {
                var today = DateTime.Today;

                var age = today.Year - vmTeamMember.DateOfBirth.Value.Year;

                // Go back to the year the person was born in case of a leap year
                if (vmTeamMember.DateOfBirth.Value.Date > today.AddYears(-age)) age--;

                return age;
            }

            return null;
        }

        public static bool IsWithNoRole(this TeamMemberViewModel vmTeamMember)
        {
            return vmTeamMember.Role == MemberRole.None;
        }

        public static bool IsPlayer(this TeamMemberViewModel vmTeamMember)
        {
            return vmTeamMember.Role == MemberRole.Player;
        }

        public static bool IsManager(this TeamMemberViewModel vmTeamMember)
        {
            return vmTeamMember.Role == MemberRole.Manager;
        }

        public static string NationalityFlagUrl(this TeamMemberViewModel vmTeamMember, UrlHelper url)
        {
            return url.Content($"~/content/img/flags/{vmTeamMember.NationalityISO3}.gif");
        }
    }
}