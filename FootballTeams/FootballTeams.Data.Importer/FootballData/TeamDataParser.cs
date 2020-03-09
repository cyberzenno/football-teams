using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.AdditionalDetail;
using FootballTeams.Domain.Teams;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FootballTeams.Data.Importer.FootballData
{
    public interface ITeamDataParser : IDataParser<Team>
    {

    }

    public class TeamDataParser : ITeamDataParser
    {
        public List<Team> ParseFile(string rawFileContent)
        {
            List<Team> results = new List<Team>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawFileContent);

            var docNode = doc.DocumentNode;

            var team = new Team();

            var additionalDetails = new AdditionalDetails();

            var teamName = GetName(docNode);
            var teamLongName = GetLongName(docNode);
            var teamProfileUrl = GetProfileUrl(docNode, teamName);
            var teamWebsiteName = GetWebsite(docNode);
            var teamWebsiteUrl = GetWebsiteUrl(docNode);
            var stadiumName = GetStadium(docNode);
            var stadiumUrl = "https://www.bing.com/images/search?q=" + HttpUtility.UrlEncode(stadiumName + " stadium");
            var address = GetAddress(docNode);

            additionalDetails.AddDetail("Website", "legaseriea.it", "http://www.legaseriea.it/en");
            additionalDetails.AddDetail("Original Team", teamName, teamProfileUrl);
            additionalDetails.AddDetail("Official Website", teamWebsiteName, teamWebsiteUrl);
            additionalDetails.AddDetail("Original Stadium", stadiumName, stadiumUrl);
            additionalDetails.AddDetail("Full Name", teamLongName);
            additionalDetails.AddDetail("Imported Address", address);

            additionalDetails.AddDetail("League", "Serie A");
            additionalDetails.AddDetail("Country", "Italy");
            additionalDetails.AddImportMethod("Local Html Parsing");
            additionalDetails.AddTimeStampNow("Imported On");

            team.AdditionalDetailsJson = additionalDetails.ToJson();

            team.Name = teamName;
            team.Address = address;


            results.Add(team);

            return results;
        }

        private string GetName(HtmlNode documentNode)
        {
            try
            {
                var dirtyName = documentNode.SelectSingleNode("//title").InnerText;

                var name = Regex.Match(dirtyName, @"The Club - (.*?) \| Lega Serie A").Groups[1].Value;

                return Capitalize(name).Trim();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetProfileUrl(HtmlNode docNode, string teamName)
        {
            try
            {
                var teamNodes = docNode.SelectNodes("//div[@class='container-fluid'][1]/a");

                var team = teamNodes.First(x => x.InnerHtml.ToLower().Contains(teamName.ToLower()));

                return team.Attributes["href"].Value;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetWebsite(HtmlNode docNode)
        {
            try
            {
                var websiteNode = docNode.SelectNodes("//aside/section/article")
                    .FirstOrDefault(x => x.InnerText.ToLower().Contains("website"))
                    .Descendants("a")
                    .FirstOrDefault();

                var name = websiteNode.InnerText;

                return name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetWebsiteUrl(HtmlNode docNode)
        {
            try
            {
                var websiteNode = docNode.SelectNodes("//aside/section/article")
                    .FirstOrDefault(x => x.InnerText.ToLower().Contains("website"))
                    .Descendants("a")
                    .FirstOrDefault();

                var url = websiteNode.Attributes["href"].Value;

                return url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetStadium(HtmlNode docNode)
        {
            try
            {
                var node = docNode.SelectNodes("//aside/section/article")
                  .FirstOrDefault(x => x.InnerText.ToLower().Contains("stadium"))
                  .Descendants("p")
                  .FirstOrDefault();

                var stadiumName = node.InnerText;

                return Capitalize(stadiumName).Trim();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetLongName(HtmlNode docNode)
        {
            try
            {
                var relevantNodes = docNode.SelectNodes("//aside/section[@class='logo']/article/p");

                var name = relevantNodes[0].InnerText;

                return Capitalize(name).Trim();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private string GetAddress(HtmlNode docNode)
        {
            try
            {
                var relevantNodes = docNode.SelectNodes("//aside/section[2]/article/p");

                return relevantNodes[0].InnerText + " " + relevantNodes[1].InnerText;
            }
            catch (Exception ex)
            {
                return ex.Message;
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

            return capitalized;
        }
    }
}
