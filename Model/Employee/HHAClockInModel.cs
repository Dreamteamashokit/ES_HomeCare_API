using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class HHAClockInModel
    {
        public int UserId { get; set; }
        public string ClockInTime { get; set; }
        public string ClockOutTime { get; set; }
        public int Type { get; set; }
        public string Notes { get; set; }
        public bool BedBath { get; set; }
        public bool SpongeBath { get; set; }
        public bool Footcare { get; set; }
        public bool Skincare { get; set; }
        public string ClientSignature { get; set; }
        public string HHAUserSignature { get; set; }
        public int MeetingId { get; set; }
    }
}
