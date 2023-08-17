using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class PackageItem
    {
        public int PackageId { get; set; }
        public int CollectionId { get; set; }
        public int? DayMode { get; set; }
        public int? TimeFramId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual Package Package { get; set; }
        public virtual TimeFrame TimeFram { get; set; }
    }
}
