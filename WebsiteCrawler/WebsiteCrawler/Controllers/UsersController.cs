using BusinessLogics;
using Microsoft.AspNetCore.Mvc;


namespace WebsiteCrawler.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(DBBusinessLogic businessLogic)
        {

            

            return View();
        }
    }
}
