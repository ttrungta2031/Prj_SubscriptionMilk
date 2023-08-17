using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Account
    {
        public Account()
        {
            PackageOrders = new HashSet<PackageOrder>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public string Address { get; set; }
        public int? StationId { get; set; }
        public string Avatar { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual Station Station { get; set; }
        public virtual ICollection<PackageOrder> PackageOrders { get; set; }
    }
}
