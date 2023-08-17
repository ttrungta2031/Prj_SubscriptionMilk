using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class DeliveryMan
    {
        public DeliveryMan()
        {
            DeliveryTrips = new HashSet<DeliveryTrip>();
            Workings = new HashSet<Working>();
        }

        public int Id { get; set; }
        public string Img { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<DeliveryTrip> DeliveryTrips { get; set; }
        public virtual ICollection<Working> Workings { get; set; }
    }
}
