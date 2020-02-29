using System;
using System.Linq;
using System.Linq.Expressions;

namespace FootballTeams.Domain
{
    public interface IGenericRepository<T> where T : class
    {
        string DbName { get; }

        void Add(T entity);
        void Update(T entity);

        T Get(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        IQueryable<T> Get(Expression<Func<T, bool>> expression, string includePath);

        IQueryable<T> GetAll();

        IQueryable<T> GetAll(string includePath);

        void Delete(T entity);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDatabaseContext _db;

        public string DbName => _db.DbName;


        public GenericRepository(IDatabaseContext db)
        {
            _db = db;
        }

        public void Add(T entity)
        {
            _db.Add(entity);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            _db.Update(entity);
            _db.SaveChanges();
        }

        public T Get(int id)
        {
            return _db.Find<T>(id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _db.Table<T>().Where(expression);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression, string includePath)
        {
            var debug = _db.Table<T>(includePath).Where(expression).ToList();

            return debug.AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return _db.Table<T>();
        }

        public IQueryable<T> GetAll(string includePath)
        {
            return _db.Table<T>(includePath);
        }

        public void Delete(T entity)
        {
            _db.Delete(entity);
            _db.SaveChanges();
        }
    }
}
