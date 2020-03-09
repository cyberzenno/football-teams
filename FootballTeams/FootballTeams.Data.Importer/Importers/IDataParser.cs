using System.Collections.Generic;

namespace FootballTeams.Data.Importer.Importers
{
    /// <summary>
    /// Implemented only by specifc Data Model Parser (i.e. Country)
    /// </summary>
    public interface IDataParser<T>
    {
        List<T> ParseFile(string rawFileContent);
    }
}
