using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public int? PacakeOrderId { get; set; }
        public int? SlotId { get; set; }
        public int? CollectionId { get; set; }
        public int? DeliveryTripId { get; set; }
        public DateTime? Day { get; set; }
        public string Status { get; set; }

        public virtual DeliveryTrip DeliveryTrip { get; set; }
        public virtual PackageOrder PacakeOrder { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
