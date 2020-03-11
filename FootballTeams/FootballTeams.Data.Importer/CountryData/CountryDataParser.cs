using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.Countries;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeams.Data.Importer.CountryData
{
    public interface ICountryDataParser : IDataParser<Country>
    {
        List<Country> ParseFileFifa(string rawFileContent);
    }

    public class CountryDataParser : ICountryDataParser
    {
        public List<Country> ParseFile(string rawFileContent)
        {
            List<Country> results = new List<Country>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawFileContent);

            var countryNodes = doc.DocumentNode.SelectNodes("//tr[@class='showpanel']");

            //Exit if no results
            if (countryNodes == null) return results;


            foreach (var node in countryNodes)
            {
                var country = new Country();

                try
                {
                    country.ExtendedName = node.SelectSingleNode(".//td/a").InnerText;
                    country.OfficialName = node.SelectSingleNode(".//td[@class='official']").InnerText;
                    country.ISO3 = node.SelectSingleNode(".//td[@class='iso3']").InnerText;
                    country.ISO2 = node.SelectSingleNode(".//td[@class='iso2']").InnerText;
                    country.UNI = node.SelectSingleNode(".//td[@class='uni']").InnerText;

                    country.OriginUrl = node.SelectSingleNode(".//td/a").GetAttributeValue("href", "");

                    results.Add(country);
                }
                catch (Exception ex)
                {
                    //todo: handle exception
                }
            }

            return results;
        }

        public List<Country> ParseFileFifa(string rawFileContent)
        {
            List<Country> results = new List<Country>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(rawFileContent);

            var countryNodes = doc.DocumentNode.SelectNodes("//table/tbody/tr");

            foreach (var node in countryNodes)
            {
                var country = new Country();

                try
                {
                    country.Name = CleanUp(node.SelectSingleNode(".//td/span/a").InnerText);
                    country.FIFA = CleanUp(node.SelectSingleNode(".//td[2]").InnerText);
                    country.FifaAssociation = CleanUp(node.SelectSingleNode(".//td/span/a").Attributes["title"].Value);
                    country.FifaAssociationUrl = $"https://www.fifa.com/associations/association/{country.FIFA}/";


                    results.Add(country);
                }
                catch (Exception ex)
                {
                    country.Name = "ex";
                }
            }

            return results;
        }

        private string CleanUp(string value)
        {
            var trimmed = value.Trim();

            var splitBySpaceAndTrimmed = trimmed.Split(' ').Select(x => x.Trim());

            var cleanName = string.Join(" ", splitBySpaceAndTrimmed);

            return cleanName;
        }
    }
}
