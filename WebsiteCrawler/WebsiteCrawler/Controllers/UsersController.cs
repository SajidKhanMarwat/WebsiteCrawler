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
            CrawlerDBContext crawlerDBContext = new CrawlerDBContext();

            var new_user = new User()
            {
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
                Email = signUp.Email,
                Password = signUp.Password,
            };

            crawlerDBContext.Users.Add(new_user);
            crawlerDBContext.SaveChanges();

            return View();
        }

    }
}
