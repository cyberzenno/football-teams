using System.Linq;

namespace FootballTeams.Domain
{
    public interface IDatabaseContext
    {
        string DbName { get; }

        IQueryable<T> Table<T>() where T : class;

        IQueryable<T> Table<T>(string includePath) where T : class;

        T Find<T>(int id) where T : class;

        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        int SaveChanges();
    }
}
