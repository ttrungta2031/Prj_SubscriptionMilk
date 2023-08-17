using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Station
    {
        public Station()
        {
            Accounts = new HashSet<Account>();
            DeliveryTrips = new HashSet<DeliveryTrip>();
            Workings = new HashSet<Working>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int? SlotId { get; set; }

        public virtual Slot Slot { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<DeliveryTrip> DeliveryTrips { get; set; }
        public virtual ICollection<Working> Workings { get; set; }
    }
}
