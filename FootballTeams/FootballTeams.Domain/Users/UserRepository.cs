using System.Linq;

namespace FootballTeams.Domain.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        //utilities
        bool IsGlobalManager(string username);
        bool DoesPasswordMatch(string username, string password);
    }

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDatabaseContext db) : base(db)
        {

        }

        public bool IsGlobalManager(string username)
        {
            var user = Get(x => x.Email == username).SingleOrDefault();

            return user.Role == UserRole.GlobalManager;
        }

        public bool DoesPasswordMatch(string username, string password)
        {
            var user = Get(x => x.Email == username && x.Password == password)
               .SingleOrDefault();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
