using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Store
    {
        public Store()
        {
            DeliveryTrips = new HashSet<DeliveryTrip>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DeliveryTrip> DeliveryTrips { get; set; }
    }
}
