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
        List<Country> GetAllFaoRecords();
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
            var fifaCountries = GetAllFifaRecords();

            var faoCountries = GetAllFaoRecords();

            foreach (var fifaCountry in fifaCountries)
            {
                //1: try by ISO3
                //there are around 100 exceptions between FIFA and ISO
                var faoCountry = faoCountries.FirstOrDefault(x => x.ISO3 == fifaCountry.FIFA);
                if (faoCountry == null)
                {
                    //2: if not, try by name
                    faoCountry = faoCountries.FirstOrDefault(fc =>
                    {
                        var nameFromFaoFile = Regex.Replace(fc.ExtendedName.ToLower(), "[^a-z]", "");

                        var nameFromFifaFile = Regex.Replace(fifaCountry.Name.ToLower(), "[^a-z]", "");

                        return nameFromFaoFile.Contains(nameFromFifaFile);
                    });
                }

                if (faoCountry != null)
                {
                    fifaCountry.ExtendedName = faoCountry.Name;
                    fifaCountry.OfficialName = faoCountry.OfficialName;
                    fifaCountry.ISO3 = faoCountry.ISO3;
                    fifaCountry.ISO2 = faoCountry.ISO2;
                    fifaCountry.UNI = faoCountry.UNI;
                    fifaCountry.OriginUrl = faoCountry.OriginUrl;

                    faoCountries.Remove(faoCountry);
                }
            }

            return fifaCountries;
        }

        public List<Country> GetAllFifaRecords()
        {
            var rawFileContent = GetFile(_rawDataPaths.FifaCountryCodesFilePath);
            var countries = (_rawDataParser as ICountryDataParser).ParseFileFifa(rawFileContent);

            return countries;
        }

        public List<Country> GetAllFaoRecords()
        {
            var rawFileContent = GetFile(_rawDataPaths.FaoCountryCodesFilePath);
            var countries = (_rawDataParser as ICountryDataParser).ParseFile(rawFileContent);

            return countries;
        }
    }
}
