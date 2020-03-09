using System;
using System.Collections.Generic;
using System.IO;

namespace FootballTeams.Data.Importer.Importers
{
    public interface IRawDataPathsProvider
    {
        string CountryCodesFilePath { get; }
        string FifaCountryCodesFilePath { get; }
        string ItalianTeamsDirectory { get; }
        string ItalianPlayersDirectory { get; }
        string ItalianManagersDirectory { get; }
    }

    public static class RawDataPathsProviderExtensions
    {
        public static string GetFullPath(this string relativePath)
        {
            var appFolder = AppDomain.CurrentDomain.BaseDirectory;

            return Path.Combine(appFolder, relativePath);
        }
    }
}
