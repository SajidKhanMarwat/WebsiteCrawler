using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogics
{
    public class DBBusinessLogic
    {
        private string DBemail { get; set; }
        private string DBpassword { get; set; }


        public string Email
        {
            get { return DBemail; }
            set { DBemail = value; }
        }

        public string Password
        {
            get { return DBpassword; }
            set { DBpassword = value; }
        }
    }
}
