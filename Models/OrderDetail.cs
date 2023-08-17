using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class OrderDetail
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
