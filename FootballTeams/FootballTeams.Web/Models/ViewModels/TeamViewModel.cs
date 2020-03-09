using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.TeamMembers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace FootballTeams.Web.Models.ViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string AdditionalDetailsJson { get; set; }

        public AdditionalDetails AdditionalDetails
        {
            get
            {
                return AdditionalDetailsJson.ToAdditionalDetailsDataModel();
            }
        }

        public int? ManagerId { get; set; }
        public IEnumerable<int> SelectedPlayerIds { get; set; }

        public List<TeamMemberViewModel> Members { get; set; }

        public List<MatchViewModel> Matches { get; set; }

        public TeamViewModel()
        {
            SelectedPlayerIds = new List<int>();
            Members = new List<TeamMemberViewModel>();
        }
    }

    public static class TeamExtensions
    {
        public static TeamMemberViewModel Manager(this TeamViewModel team)
        {
            return team.Members?.FirstOrDefault(x => x.Role == MemberRole.Manager);
        }

        public static bool HasPlayer(this TeamViewModel team, int id)
        {
            return team.Players().Any(x => x.Id == id);
        }

        public static bool HasSelectedPlayer(this TeamViewModel team, int id)
        {
            return team.SelectedPlayerIds.Any(x => x == id);
        }

        public static IEnumerable<TeamMemberViewModel> Players(this TeamViewModel team)
        {
            return team.Members?.Where(x => x.Role == MemberRole.Player);
        }

        public static void AddManager(this TeamViewModel team, TeamMemberViewModel manager)
        {
            if (team.Members == null)
            {
                team.Members = new List<TeamMemberViewModel>();
            }

            if (manager.Role == MemberRole.Manager)
            {
                if (team.Manager() != null)
                {
                    team.Members.Remove(team.Manager());
                }

                team.Members.Add(manager);
            }

            //todo: throw exception  if issues
        }

        public static int NumberOfMatchesPlayed(this TeamViewModel team)
        {
            if (team.Matches != null)
            {
                return team.Matches.Count();
            }

            return 0;
        }

        public static int Points(this TeamViewModel team)
        {
            if (team.Matches == null)
            {
                return 0;
            }

            //todo: refactor
            //this is Business Logic and should be in a more appropriate place

            var points = team.Matches.Sum(x =>
            {
                var hasWon =
                (team.Id == x.TeamHomeId && x.TeamHomeScore > x.TeamAwayScore) ||
                (team.Id == x.TeamAwayId && x.TeamHomeScore < x.TeamAwayScore);

                if (hasWon)
                {
                    return 3;
                }

                var hasDraw = x.TeamHomeScore == x.TeamAwayScore;

                return hasDraw ? 1 : 0;
            });

            return points;
        }

        public static string MatchResult(this TeamViewModel team, MatchViewModel match)
        {
            if (team.Matches == null)
            {
                return "";
            }

            var hasWon =
                 (team.Id == match.TeamHomeId && match.TeamHomeScore > match.TeamAwayScore) ||
                 (team.Id == match.TeamAwayId && match.TeamHomeScore < match.TeamAwayScore);

            if (hasWon)
            {
                return "w";
            }

            var hasDraw = match.TeamHomeScore == match.TeamAwayScore;

            return hasDraw ? "d" : "l";
        }

        public static string ImageUrlOrDefault(this TeamViewModel team, UrlHelper urlHelper)
        {
            return urlHelper.Content(team.ImageUrl ?? $"~/content/img/teams/{team.Name.ToLower().Replace(" ", "-")}.png");
        }

        public static string ImageUrlOrDefault(this string teamName, UrlHelper urlHelper)
        {
            return urlHelper.Content($"~/content/img/teams/{teamName.ToLower().Replace(" ", "-")}.png");
        }

        public static string ImageUrlOrNoTeam(this TeamViewModel team, UrlHelper urlHelper)
        {
            return urlHelper.Content(team.ImageUrl ?? "~/content/img/teams/no-team.png");
        }
    }
}