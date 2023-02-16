using BusinessLogics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebsiteCrawler.Models;

namespace WebsiteCrawler.Controllers
{
    public class UsersController : Controller
    {
        //private readonly DBBusinessLogic _dBBusinessLogic;

        //public UsersController(DBBusinessLogic dBBusinessLogic)
        //{
        //    dBBusinessLogic = _dBBusinessLogic;
        //}

        public IActionResult Login(Auth auth)
        {
            //var authData = _dBBusinessLogic.GetAll();
            //return View(authData);

            if (auth.Email == User.Equals(auth.Email).ToString() && auth.Password == User.Equals(auth.Password).ToString())
            {
                return RedirectToAction("/Crawler/Index");
            }
            else
            {
               var ex = new Exception("User Not Found, Try Again.");
                return View(ex);
            }

            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
    }
}
