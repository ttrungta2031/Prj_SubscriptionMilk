using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class DeliveryTrip
    {
        public DeliveryTrip()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? DeliveryManId { get; set; }
        public int? StoreId { get; set; }
        public int? StationId { get; set; }

        public virtual DeliveryMan DeliveryMan { get; set; }
        public virtual Station Station { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
