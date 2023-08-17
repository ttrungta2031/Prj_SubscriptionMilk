using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryInPackages = new HashSet<CategoryInPackage>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<CategoryInPackage> CategoryInPackages { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
