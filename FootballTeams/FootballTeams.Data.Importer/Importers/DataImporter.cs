using System.Collections.Generic;
using System.IO;
using FootballTeams.Domain;

namespace FootballTeams.Data.Importer.Importers
{
    public interface IDataImporter<T> where T : class
    {
        List<T> GetAllRecords();
        void SaveRecords(List<T> records);
    }
    public abstract class DataImporter<T> : IDataImporter<T> where T : class
    {
       protected readonly IRawDataPathsProvider _rawDataPaths;
       protected readonly IDataParser<T> _rawDataParser;
       protected readonly IGenericRepository<T> _dataRepository;

        public DataImporter(IRawDataPathsProvider rawDataProvider, IDataParser<T> rawDataParser, IGenericRepository<T> dataRepository)
        {
            _rawDataPaths = rawDataProvider;
            _rawDataParser = rawDataParser;
            _dataRepository = dataRepository;
        }

        public abstract List<T> GetAllRecords();      

        public void SaveRecords(List<T> records)
        {
            foreach (var record in records)
            {
                _dataRepository.Add(record);
            }
        }

        public string GetFile(string rawDataPath)
        {
            var fileContent = File.ReadAllText(rawDataPath);

            return fileContent;
        }

        public List<string> GetFiles(string rawDataDirectory)
        {
            var filesContent = new List<string>();

            foreach (var filePath in Directory.GetFiles(rawDataDirectory))
            {
                filesContent.Add(File.ReadAllText(filePath));
            }

            return filesContent;
        }
    }
}
