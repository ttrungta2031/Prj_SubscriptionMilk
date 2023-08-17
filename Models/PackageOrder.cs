using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class PackageOrder
    {
        public PackageOrder()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string FullName { get; set; }
        public int? StationId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int? PaymentId { get; set; }
        public int? CustomerId { get; set; }
        public int? PackageId { get; set; }
        public decimal? Total { get; set; }

        public virtual Account Customer { get; set; }
        public virtual Package Package { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
