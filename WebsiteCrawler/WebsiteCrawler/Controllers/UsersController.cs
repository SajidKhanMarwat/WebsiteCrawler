using BusinessLogics;
using Microsoft.AspNetCore.Mvc;
using WebsiteCrawler.Models;

namespace WebsiteCrawler.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(DBBusinessLogic authentication)
        {
            
            return View();
        }
    }
}
