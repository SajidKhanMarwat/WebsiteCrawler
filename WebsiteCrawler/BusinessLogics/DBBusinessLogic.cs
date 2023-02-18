using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repo;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics
{
    public class DBBusinessLogic
    {
        //private readonly IEnumerable<CrawlerDBContext>? _userAuth;
        CrawlerDBContext crawlerDBContext = new CrawlerDBContext();


        // User Authentication for login
        public bool UserAuthentication(string email, string password)
        {
            var authData = crawlerDBContext.Users.Any(x => x.Email == email && x.Password == password);
            return authData;
        }

        // Adding new user to Database
        public void NewUserAdding(string fn, string ln, string email, string password)
        {
            CrawlerDBContext crawlerDBContext = new CrawlerDBContext();

            var adduser = new User()
            {
                FirstName = fn,
                LastName = ln,
                Email = email,
                Password = password
            };

            if (adduser.FirstName != null || adduser.Email != null || adduser.Password != null)
            {
                crawlerDBContext.Users.Add(adduser);
                crawlerDBContext.SaveChanges();
            }
        }
    }
}
