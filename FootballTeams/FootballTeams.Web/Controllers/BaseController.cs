using FootballTeams.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class BaseController : Controller
    {
        internal void AlertSuccess(string title, string message)
        {
            TempData["Alert"] = new AlertViewModel
            {
                Type = "success",
                Title = title,
                Message = message
            };
        }

        internal void AlertDanger(string title, string message)
        {
            TempData["Alert"] = new AlertViewModel
            {
                Type = "danger",
                Title = title,
                Message = message
            };
        }

        /// <summary>
        /// Complete the sentence:
        /// You don't have the authorization [to do something]
        /// </summary>
        /// <param name="toDoSomething"></param>
        internal void AlertUnauthorized(string toDoSomething)
        {
            AlertDanger("Unauthorized", $"You don't have the authorization {toDoSomething}.");
        }

        internal void AlertModelStateErrors()
        {
            foreach (var error in GetErrorsFromModelState())
            {
                AlertDanger("Error", error);
            }
        }

        internal IEnumerable<string> GetErrorsFromModelState()
        {
            var errors = ModelState.Values
                .Where(x => x.Errors.Count > 0)
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            return errors;
        }
    }
}