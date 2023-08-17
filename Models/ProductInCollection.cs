using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class ProductInCollection
    {
        public int ProductId { get; set; }
        public int CollectionId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Product Product { get; set; }
    }
}
