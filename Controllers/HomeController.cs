using Microsoft.AspNetCore.Mvc;
using Login.Models;

namespace Login.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string actions ,string email, string password, string status)
        {
            UserCount userCount = new UserCount(email, password);

            switch(actions)
            {
                case "get in":
                    string userStatus = userCount.status == 0 ? "Jefe" : "empleado";

                    if (userCount.email != null) ViewData["Success"] = "Todo bien " + userStatus;
                    else ViewData["Danger"] = "No es un usuario valido";
                    break;
                case "delete":
                    if (new UserCount().DeleteUserCount(userCount.idUserCount, null))
                        ViewData["Success"] = "Se han eliminado el usuario";
                    break;
                case "update":

                    if (new UserCount().UpdateUserCount(userCount))
                        ViewData["Success"] = "Se ha actualizado el usuario";
                    break;
                case "add":
                    userCount.email = email;
                    userCount.password = password;
                    userCount.status = int.Parse(status);

                    if (new UserCount().AddUserCount(userCount))
                        ViewData["Success"] = "Se ha agregado un usuario";
                    break;
            }

            return View();
        }

        public IActionResult About()
        { 
            return View();
        }

    }
}
