using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class MeetingDBClass
    {
        public int ClientId { get; set; }
        public string ExtClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string ClassCordinator { get; set; }
        public string Country { get; set; }
        public string CState { get; set; }
        public string ZipCode { get; set; }
        public int MeetingId { get; set; }
        public int MClientId { get; set; }
        public string MeetingNote { get; set; }
        public DateTime MeetingDate { get; set; }
        public int TotalMeetingHrs { get; set; }
        public int TotalMeetingMins { get; set; }
        public DateTime MeetingStartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int MeetingOrder { get; set; }
    }
}
