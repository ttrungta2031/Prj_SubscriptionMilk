using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Working
    {
        public int StationId { get; set; }
        public int DeliveryManId { get; set; }

        public virtual DeliveryMan DeliveryMan { get; set; }
        public virtual Station Station { get; set; }
    }
}
