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
                    country.ShortName = node.SelectSingleNode(".//td/a").InnerText;
                    country.OfficialName = node.SelectSingleNode(".//td[@class='official']").InnerText;
                    country.ISO3 = node.SelectSingleNode(".//td[@class='iso3']").InnerText;
                    country.ISO2 = node.SelectSingleNode(".//td[@class='iso2']").InnerText;
                    country.UNI = node.SelectSingleNode(".//td[@class='uni']").InnerText;

                    country.OriginUrl = node.SelectSingleNode(".//td/a").GetAttributeValue("href", "");
                    country.FlagUrl = $"~/content/img/flags/{country.ISO3.ToUpper()}.gif";
                    country.FlagBase64 = "";//todo: download the flag as Base64, so we can do it only once

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
                    country.ShortName = CleanUp(node.SelectSingleNode(".//td/span/a").InnerText);
                    country.FifaAssociation = CleanUp(node.SelectSingleNode(".//td/span/a").Attributes["title"].Value);
                    country.FIFA = CleanUp(node.SelectSingleNode(".//td[2]").InnerText);

                    results.Add(country);
                }
                catch (Exception ex)
                {
                    country.ShortName = "ex";
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
