using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class UsersDatum
    {
        public int Id { get; set; }
        public string? Urls { get; set; }
        public int? StatusCode { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
