using DataAccess.DataContext;
using DataAccess.Models;
using DataAccess.Repo;
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

        public bool UserAuthentication(string email, string password)
        {
            var authData = crawlerDBContext.Users.Any(x => x.Email == email && x.Password == password);
            return authData;
        }
        
    }
}
