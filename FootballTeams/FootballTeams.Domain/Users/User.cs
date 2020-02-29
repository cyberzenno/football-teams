namespace FootballTeams.Domain.Users
{
    public enum UserRole
    {
        None,
        GlobalManager
    }


    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
