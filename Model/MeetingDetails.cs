using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class MeetingDetails : MeetingInfo
    {
        public string MeetingStartHrsTime { get; set; }
        public string MeetingStartMinsTime { get; set; }
        public string MeetingEndHrsTime { get; set; }
        public string MeetingEndMinsTime { get; set; }
        public string StartTimeType { get; set; }
        public string EndTimeType { get; set; }
    }
}
