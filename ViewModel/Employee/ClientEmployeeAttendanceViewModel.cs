using System;

namespace ES_HomeCare_API.ViewModel.Employee
{
    public class ClientEmployeeAttendanceViewModel
    {
        public long MeetingId { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime? ClientClockInOutTime { get; set; }
        public decimal? ClientLatitude { get; set; }
        public decimal? ClientLongitude { get; set; }
        public string clientSignature { get; set; }
        public long EmpId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? EmployeeClockInOutTime { get; set; }
        public decimal? EmployeeLatitude { get; set; }
        public decimal? EmployeeLongitude { get; set; }
        public string hhaSignature { get; set; }
    }
}
