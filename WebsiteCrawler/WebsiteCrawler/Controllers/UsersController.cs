﻿using BusinessLogics;
using DataAccess.DataContext;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using WebsiteCrawler.Models;

namespace WebsiteCrawler.Controllers
{
    public class UsersController : Controller
    {

        public IActionResult Login(User auth)
        {
            
            if(auth.Email != null && auth.Password != null)
            {
                DBBusinessLogic dBBusinessLogic = new DBBusinessLogic();
                var hasFound = dBBusinessLogic.UserAuthentication(auth.Email, auth.Password);

                if (hasFound)
                {
                    return RedirectToAction("Index", "Crawler");
                }
                else
                {
                    //var ex = new Exception("User Not Found, Try Again.");
                    return View();
                }
            }
            else
            {
                return View();
            }
                
        }
        public IActionResult Logout()
        {
            return View();
        }

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
