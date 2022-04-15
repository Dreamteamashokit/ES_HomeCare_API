using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Meeting
{




    public class ClientMeeting
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public IEnumerable<ClMeeting> Meetings { get; set; }
      


    }

    public class ClMeeting
    {
        

        public long MeetingId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }


    }








}
