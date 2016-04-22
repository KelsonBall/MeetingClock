using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingClock.Models
{
    public class MeetingModel
    {
        public double BillingRate { get; set; }
        public int Participants { get; set; }
        public int AlertInterval { get; set; }
        public int AlertCount { get; set; }
        public double MeetingCost { get; set; }        
    }
}
