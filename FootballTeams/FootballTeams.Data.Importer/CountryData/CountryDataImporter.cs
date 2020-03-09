using FootballTeams.Data.Importer.Importers;
using FootballTeams.Domain.Countries;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace FootballTeams.Data.Importer.CountryData
{
    public interface ICountryDataImporter : IDataImporter<Country>
    {
        List<Country> GetAllFifaRecords();
    }

    public class CountryDataImporter : DataImporter<Country>, ICountryDataImporter
    {
        public CountryDataImporter(
            IRawDataPathsProvider rawDataPathsProvider,
            ICountryDataParser rawDataParser,
            ICountryRepository dataRepository)
            : base(rawDataPathsProvider, rawDataParser, dataRepository)
        {
        }

        public override List<Country> GetAllRecords()
        {
            var rawFileContent = GetFile(_rawDataPaths.CountryCodesFilePath);
            var countries = _rawDataParser.ParseFile(rawFileContent);

            var rawFifaFileContent = GetFile(_rawDataPaths.FifaCountryCodesFilePath);
            var fifaCountries = (_rawDataParser as ICountryDataParser).ParseFileFifa(rawFifaFileContent);

            foreach (var c in countries)
            {
                //1: try by ISO3
                //there are around 100 exceptions between FIFA and ISO
                var fc = fifaCountries.FirstOrDefault(x => x.FIFA == c.ISO3);
                if (fc == null)
                {
                    //2: if not, try by name
                    fc = fifaCountries.FirstOrDefault(x =>
                    {
                        var nameFromFifaFile = Regex.Replace(x.ShortName.ToLower(), "[^a-z]", "");
                        var nameFromFaoFile = Regex.Replace(c.ShortName.ToLower(), "[^a-z]", "");

                        return
                        nameFromFaoFile == nameFromFifaFile ||
                        nameFromFaoFile.Contains(nameFromFifaFile);
                    });
                }

                if (fc != null)
                {
                    c.FIFA = fc.FIFA;
                    c.FifaAssociation = fc.FifaAssociation;

                    fifaCountries.Remove(fc);
                }
            }

            return countries;
        }

        public List<Country> GetAllFifaRecords()
        {
            var rawFifaFileContent = GetFile(_rawDataPaths.FifaCountryCodesFilePath);
            var fifaCountries = (_rawDataParser as ICountryDataParser).ParseFileFifa(rawFifaFileContent);

            return fifaCountries;
        }
    }
}
