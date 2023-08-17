using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class TimeFrame
    {
        public TimeFrame()
        {
            PackageItems = new HashSet<PackageItem>();
        }

        public int Id { get; set; }
        public int? SlotId { get; set; }

        public virtual Slot Slot { get; set; }
        public virtual ICollection<PackageItem> PackageItems { get; set; }
    }
}
