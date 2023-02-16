using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class PaymentStatus
    {
        public int PaymentId { get; set; }
        public string? Free { get; set; }
        public string? Paid { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
