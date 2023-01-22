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
        public IActionResult Login(Authentication authentication)
        {
            DBBusinessLogic dBBusiness=new DBBusinessLogic();
            dBBusiness.Email = authentication.Email;
            dBBusiness.Password = authentication.Password;
            return View();
        }
    }
}
