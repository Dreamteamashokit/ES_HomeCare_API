using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.ViewModel
{
    public class MeetingJson
    {
        public int MeetingId { get; set; }
        public int ClientId { get; set; }
        public List<int> EmpList { get; set; }
        public string MeetingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MeetingNote { get; set; }

        public int UserId { get; set; }
    }
}
