using FootballTeams.Domain;
using System.Linq;

namespace FootballTeams.Data
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly EF_DatabaseContext _db;

        public string DbName
        {
            get
            {
                return _db.Database.Connection.Database;
            }
        }

        public DatabaseContext(EF_DatabaseContext db)
        {
            _db = db;
        }

        public IQueryable<T> Table<T>() where T : class
        {
            return _db.Set<T>();
        }

        public IQueryable<T> Table<T>(string includePath) where T : class
        {
            return _db.Set<T>().Include(includePath);
        }

        public T Find<T>(int id) where T : class
        {
            return _db.Set<T>().Find(id);
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Set<T>().Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _db.Set<T>().Attach(entity);

            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }


    }
}
