using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Meeting
{
    public class MeetingStatus:BaseModel
    {
        public int MeetingId { get; set; }
        public string MeetingNote { get; set; }
        public short IsStatus { get; set; }
    }
}
