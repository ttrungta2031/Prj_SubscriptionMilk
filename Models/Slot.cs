using System;
using System.Collections.Generic;

#nullable disable

namespace SubscriptionMilk.Models
{
    public partial class Slot
    {
        public Slot()
        {
            Orders = new HashSet<Order>();
            Stations = new HashSet<Station>();
            TimeFrames = new HashSet<TimeFrame>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
        public virtual ICollection<TimeFrame> TimeFrames { get; set; }
    }
}
