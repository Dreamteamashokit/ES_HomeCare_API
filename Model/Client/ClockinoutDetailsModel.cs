using System;

namespace ES_HomeCare_API.Model.Client
{
    public class ClockinoutDetailsModel
    {
        public long ClockId { get; set; }
        public long ClientId { get; set; }
        public long EmpId { get; set; }
        public long MeetingId { get; set; }
        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
    }
}
