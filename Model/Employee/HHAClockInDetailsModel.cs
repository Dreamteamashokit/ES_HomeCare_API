using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class HHAClockInDetailsModel
    {
        public int ClockId { get; set; }
        public int UserId { get; set; }
        public DateTime? ClockInTime { get; set; }
        public DateTime? ClockOutTime { get; set; }
        public string Notes { get; set; }
    }
}
