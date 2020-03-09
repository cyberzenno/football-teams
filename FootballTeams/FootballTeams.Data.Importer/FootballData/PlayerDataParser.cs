using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.TeamMembers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface IPlayerDataParser : IDataParser<TeamMember>
    {

    }

    public class PlayerDataParser : IPlayerDataParser
    {
        public List<TeamMember> ParseFile(string rawFileContent)
        {
            List<TeamMember> results = new List<TeamMember>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawFileContent);

            var docNode = doc.DocumentNode;

            var teamName = GetTeamName(docNode);

            var players = new List<TeamMember>();

            var playerNodes = docNode.SelectNodes("//div[@id='rosa-completa']/table[1]/tbody/tr").Skip(1);
            foreach (var node in playerNodes)
            {
                TeamMember player = ParsePlayer(node, teamName);

                results.Add(player);
            }

            return results;
        }

        private TeamMember ParsePlayer(HtmlNode playerNode, string teamName)
        {
            var player = new TeamMember();

            player.Team = new Domain.Teams.Team
            {
                Name = teamName
            };

            player.Number = GetPlayerNumber(playerNode);
            player.Fullname = GetFullName(playerNode);
            player.Role = MemberRole.Player;
            player.Position = GetPlayerPosition(playerNode);
            player.DateOfBirth = GetDateOfbirth(playerNode);

            var nationalityFIFA = GetNationalityFIFA(playerNode);
            var profileUrl = GetProfileUrl(playerNode);
            var goals = GetGoals(playerNode);
            var y = GetYellowCards(playerNode);
            var r = GetRedCards(playerNode);

            var additionalDetails = new AdditionalDetails();

            additionalDetails.AddDetail("Website", "legaseriea.it", "http://www.legaseriea.it/en");
            additionalDetails.AddDetail("Player Profile", player.Fullname, profileUrl);
            //additionalDetails.AddDetail("Original Team", teamName, teamProfileUrl);

            additionalDetails.AddDetail("Nationality", nationalityFIFA);
            additionalDetails.AddDetail("Goals", goals);
            additionalDetails.AddDetail("Yellow", y);
            additionalDetails.AddDetail("Red", r);

            additionalDetails.AddDetail("League", "Serie A");
            additionalDetails.AddDetail("Country", "Italy");
            additionalDetails.AddImportMethod("Local Html Parsing");
            additionalDetails.AddTimeStampNow("Imported On");

            player.AdditionalDetailsJson = additionalDetails.ToJson();

            return player;
        }




        private string GetTeamName(HtmlNode documentNode)
        {
            try
            {
                var dirtyName = documentNode.SelectSingleNode("//title").InnerText;

                var name = Regex.Match(dirtyName, @"Squad - (.*) \| Lega Serie A").Groups[1].Value;

                return Capitalize(name);
            }
            catch (Exception ex)
            {
                return "ex";
            }
        }

        private int? GetPlayerNumber(HtmlNode playerNode)
        {
            try
            {
                return int.Parse(playerNode.SelectNodes("td")[0].InnerText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private string GetFullName(HtmlNode playerNode)
        {
            try
            {
                var fullName = playerNode.SelectNodes("td")[1].InnerText.Trim();

                return Capitalize(fullName);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private PlayerPosition GetPlayerPosition(HtmlNode playerNode)
        {
            try
            {
                Enum.TryParse<PlayerPosition>(playerNode.SelectNodes("td")[3].InnerText, out var position);

                return position;
            }
            catch (Exception ex)
            {
                return PlayerPosition.None;
            }
        }

        private DateTime? GetDateOfbirth(HtmlNode playerNode)
        {
            try
            {
                var dateText = playerNode.SelectNodes("td")[2].InnerText.Trim();
                return DateTime.Parse(dateText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        private string GetProfileUrl(HtmlNode playerNode)
        {
            try
            {
                var url = playerNode.SelectSingleNode("td/a").Attributes["href"].Value;

                return url;
            }
            catch (Exception ex)
            {
                return "";
            }
        }




        private string GetNationalityFIFA(HtmlNode playerNode)
        {
            try
            {
                var nationalityFIFA = playerNode.SelectSingleNode("td/img").Attributes["title"].Value;

                return nationalityFIFA.ToUpper();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string GetGoals(HtmlNode playerNode)
        {
            try
            {
                return playerNode.SelectNodes("td")[6].InnerText.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string GetYellowCards(HtmlNode playerNode)
        {
            try
            {
                return playerNode.SelectNodes("td")[7].InnerText.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string GetRedCards(HtmlNode playerNode)
        {
            try
            {
                return playerNode.SelectNodes("td")[9].InnerText.Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }




        private string Capitalize(string name)
        {
            var capitalized = "";

            foreach (var word in name.ToLower().Split(' '))
            {
                var firstLetter = word.Substring(0, 1);

                if (Regex.IsMatch(firstLetter, "[a-zA-Z]"))
                {
                    firstLetter = firstLetter.ToUpper();
                }

                capitalized += $" {firstLetter}{word.Substring(1)}";
            }

            return capitalized.Trim();
        }
    }
}
