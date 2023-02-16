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

        private readonly IGenericRepo<User> _userAuth;

        public DBBusinessLogic(IGenericRepo<User> user)
        {
            user = _userAuth;
        }

        public Task<List<User>> GetAll()
        {

           var auth = _userAuth.DoAuthentication();

            return null;
        }

    }
}
