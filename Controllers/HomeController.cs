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
                case "Get in":
                    string userStatus = userCount.status == 0 ? "Jefe" : "empleado";

                    if (userCount.email != null) ViewData["Success"] = "Todo bien " + userStatus;
                    else ViewData["Danger"] = "No es un usuario valido";
                    break;
            }

            return View();
        }
        [HttpGet]
        public IActionResult HandleUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HandleUser(string actions, string email, string password, string status)
        {
            UserCount user = new UserCount(email, password);
            switch (actions)
            {
                case "Add":
                    if (!string.IsNullOrEmpty(user.email))
                    {
                        ViewData["Success"] = "El usuario ya existe";
                        return View();
                    } else
                    {
                        UserCount userCount = new UserCount();
                        userCount.email = email;
                        userCount.password = password;
                        userCount.status = int.Parse(status);

                        if(new UserCount().AddUserCount(userCount))
                        {
                            ViewData["Success"] = "Se ha agregado el usuario de forma correcta";
                            return View();
                        }
                    }
                    break;
                case "Update":
                    if (string.IsNullOrEmpty(user.email))
                    {
                        ViewData["Success"] = "El usuario no existe";
                        return View();
                    } else
                    {
                        UserCount userCount = new UserCount();
                        userCount.email = email;
                        userCount.password = password;
                        userCount.status = int.Parse(status);
                        userCount.idUserCount = user.idUserCount;

                        if (new UserCount().UpdateUserCount(userCount))
                        {
                            ViewData["Success"] = "Se ha actualzado de forma correcta";
                            return View();
                        }
                    }
                    break;
                case "Delete":
                    if (string.IsNullOrEmpty(user.email))
                    {
                        ViewData["Success"] = "El usuario no existe";
                        return View();
                    }
                    else
                    {
                        if(new UserCount().DeleteUserCount(user.idUserCount, user.email))
                        {
                            ViewData["Success"] = "El usuario ha sido eliminado de forma correcta";
                            return View();
                        }
                    }
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
