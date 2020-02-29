using FootballTeams.Domain.Users;
using System.Web.Mvc;
using System.Web.Security;

namespace FootballTeams.Web.Controllers
{
    public class AccountController : BaseController
    {
        IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Obsolete]
        public ActionResult Login(string email, string password)
        {
            //todo: implement Not Obsolete Authentication / Authorization
            //however, despite is Deprecated, it is a very quick way to hash a password for basic security
            var hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            if (_userRepository.DoesPasswordMatch(email, hashedPassword))
            {
                FormsAuthentication.SetAuthCookie(email, false);

                return RedirectToAction("Index", "Team");
            }

            AlertDanger("Unauthorised", "The Username or Password provided is incorrect.");

            return RedirectToAction("Index", "home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            AlertSuccess("Log Out", "You have successfully logged out.");

            return RedirectToAction("Index", "home");
        }
    }
}