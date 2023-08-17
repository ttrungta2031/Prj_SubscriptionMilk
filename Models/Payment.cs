using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Payment
    {
        public Payment()
        {
            PackageOrders = new HashSet<PackageOrder>();
        }

        public int Id { get; set; }

        public virtual ICollection<PackageOrder> PackageOrders { get; set; }
    }
}
