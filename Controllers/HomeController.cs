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
        public IActionResult Index(string actions ,string email, string password)
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
                    bool deleteUser = new UserCount().DeleteUserCount(userCount.idUserCount, null);

                    if (deleteUser) ViewData["Success"] = "Se han eliminado el usuario";
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
