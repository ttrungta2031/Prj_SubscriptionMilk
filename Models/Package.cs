using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Package
    {
        public Package()
        {
            CategoryInPackages = new HashSet<CategoryInPackage>();
            PackageItems = new HashSet<PackageItem>();
            PackageOrders = new HashSet<PackageOrder>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<CategoryInPackage> CategoryInPackages { get; set; }
        public virtual ICollection<PackageItem> PackageItems { get; set; }
        public virtual ICollection<PackageOrder> PackageOrders { get; set; }
    }
}
