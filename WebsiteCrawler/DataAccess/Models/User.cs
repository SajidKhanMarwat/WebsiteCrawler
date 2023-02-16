using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class User
    {
        public User()
        {
            PaymentStatuses = new HashSet<PaymentStatus>();
            UsersData = new HashSet<UsersDatum>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<PaymentStatus> PaymentStatuses { get; set; }
        public virtual ICollection<UsersDatum> UsersData { get; set; }
    }
}
