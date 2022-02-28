using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class MeetingInfo
    {
        public int MeetingId { get; set; }
        public int MClientId { get; set; }
        public string MeetingNote { get; set; }
        public string MeetingDate { get; set; }
        public int TotalMeetingHrs { get; set; }
        public int TotalMeetingMins { get; set; }
        public string MeetingStartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int MeetingOrder { get; set; }
    }
}
