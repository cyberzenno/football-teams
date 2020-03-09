using FootballTeams.Domain.Countries;
using FootballTeams.Domain.Matches;
using FootballTeams.Domain.TeamMembers;
using FootballTeams.Domain.Teams;
using FootballTeams.Domain.Users;
using System.Data.Entity;

namespace FootballTeams.Data
{
    public class EF_DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Country> Countires { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(x => x.Members)
                .WithOptional(x => x.Team);
        }
    }
}
