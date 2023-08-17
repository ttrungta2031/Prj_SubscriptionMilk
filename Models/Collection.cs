using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Collection
    {
        public Collection()
        {
            PackageItems = new HashSet<PackageItem>();
            ProductInCollections = new HashSet<ProductInCollection>();
        }

        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PackageItem> PackageItems { get; set; }
        public virtual ICollection<ProductInCollection> ProductInCollections { get; set; }
    }
}
