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
        private readonly IEnumerable<CrawlerDBContext>? _userAuth;
        CrawlerDBContext crawlerDBContext = new CrawlerDBContext();


        // User Authentication for login
        public bool UserAuthentication(string email, string password)
        {
            var authData = crawlerDBContext.Users.Any(x => x.Email == email && x.Password == password);
            return authData;
        }

        // Adding new user to Database
        public void NewUserAdding(UserDbData newUserData)
        {
            CrawlerDBContext crawlerDBContext = new CrawlerDBContext();

            var new_user = new User()
            {
                FirstName = newUserData.FirstName,
                LastName = newUserData.LastName,
                Email = newUserData.Email,
                Password = newUserData.Password,
            };

            crawlerDBContext.Users.Add(new_user);
            crawlerDBContext.SaveChanges();
        }
        
    }
}
