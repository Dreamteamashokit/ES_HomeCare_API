using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Meeting
{
    public class MeetingModel
    {
        public int MeetingId { get; set; }
        public string ClientId { get; set; }
        public string MeetingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MeetingNote { get; set; }
        public int EmpId { get; set; }


    }
}
