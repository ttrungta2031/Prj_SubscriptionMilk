using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class CategoryInPackage
    {
        public int CategoryId { get; set; }
        public int PackageId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Package Package { get; set; }
    }
}
