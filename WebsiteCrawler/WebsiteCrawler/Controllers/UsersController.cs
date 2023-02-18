using BusinessLogics;
using DataAccess.DataContext;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using WebsiteCrawler.Models;

namespace WebsiteCrawler.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User auth)
        {
            
            if(auth.Email != null && auth.Password != null)
            {
                DBBusinessLogic dBBusinessLogic = new DBBusinessLogic();
                var hasFound = dBBusinessLogic.UserAuthentication(auth.Email, auth.Password);

                if (hasFound)
                {

                    List<Claim> claims = new List<Claim>()
                   {
                       new Claim(ClaimTypes.NameIdentifier, auth.Email, auth.Password)
                   };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                    };

                   await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                        new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Crawler");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        //public IActionResult Logout()
        //{
        //    return View();
        //}

        public IActionResult SignUp(SignUp signUp)
        {
            DBBusinessLogic dBBusinessLogic = new DBBusinessLogic();

            if (signUp.FirstName != null && signUp.Email != null && signUp.Password != null)
            {
                dBBusinessLogic.NewUserAdding(signUp.FirstName, signUp.LastName, signUp.Email, signUp.Password);
                return RedirectToAction("Login", "Users");
            }
            else
            {
                return View();
            }
        }

    }
}
