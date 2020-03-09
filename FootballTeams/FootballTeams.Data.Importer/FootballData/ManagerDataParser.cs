using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.TeamMembers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface IManagerDataParser : IDataParser<TeamMember>
    {

    }

    public class ManagerDataParser : IManagerDataParser
    {
        public List<TeamMember> ParseFile(string rawFileContent)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawFileContent);

            var docNode = doc.DocumentNode;

            var managerNode = docNode.SelectSingleNode("//section[@class='organigramma']/table[1]/tbody/tr/td[2]");

            var manager = new TeamMember();
            manager.Role = MemberRole.Manager;
            manager.Fullname = Capitalize(managerNode.InnerText);
            manager.Team = new Domain.Teams.Team
            {
                Name = GetTeamName(docNode)
            };

            var additionalDetails = new AdditionalDetails();

            additionalDetails.AddDetail("Website", "legaseriea.it", "http://www.legaseriea.it/en");
            additionalDetails.AddDetail("Manager Profile", manager.Fullname, GetProfileUrl(manager.Fullname));

            additionalDetails.AddDetail("League", "Serie A");
            additionalDetails.AddDetail("Country", "Italy");
            additionalDetails.AddImportMethod("Local Html Parsing");
            additionalDetails.AddTimeStampNow("Imported On");

            manager.AdditionalDetailsJson = additionalDetails.ToJson();

            return new List<TeamMember> { manager };
        }


        private string GetTeamName(HtmlNode documentNode)
        {
            try
            {
                var dirtyName = documentNode.SelectSingleNode("//title").InnerText;

                var name = Regex.Match(dirtyName, @"Coaching Staff - (.*) \| Lega Serie A").Groups[1].Value;

                return Capitalize(name);
            }
            catch (Exception ex)
            {
                return "ex";
            }
        }

        private string GetProfileUrl(string managerFullName)
        {
            return "https://www.bing.com/search?q=" + HttpUtility.UrlEncode(managerFullName + " manager");
        }

        private string Capitalize(string name)
        {
            var capitalized = "";

            name = name.Trim();

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
