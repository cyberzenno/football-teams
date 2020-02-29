using FootballTeams.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace FootballTeams.Web.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }

    public static class UserViewModelExtensions
    {
        public static bool IsGlobalManager(this UserViewModel vmUser)
        {
            return vmUser.Role == UserRole.GlobalManager;
        }
    }
}