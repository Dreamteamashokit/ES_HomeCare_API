using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Meeting
{
    public class MeetingModel : BaseModel
    {
        public int MeetingId { get; set; }
        public int ClientId { get; set; }
        public int EmpId { get; set; }
        public List<int> EmpList { get; set; }
        public DateTime MeetingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string MeetingNote { get; set; }
        public short IsStatus { get; set; }

    }

    public class NotesModel : BaseModel
    {
        public long MeetingId { get; set; }
        public string MeetingPoint { get; set; }
    }



    public class EmpMeeting : BaseModel
    {

        public long MeetingId { get; set; }
        public int EmpId { get; set; }
        public int ClientId { get; set; }
        public string EmpName { get; set; }
        public string ClientName { get; set; }
        public DateTime MeetingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public short IsStatus { get; set; }
    }


}
