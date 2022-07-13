using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model
{
    public class AvailbilityReponse
    {
        public long EmpId { get; set; }
        public string EmpName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string FlatNo { get; set; }
        public List<EmpAppointment> MeetingList { get; set; }

    }

    public class EmpAppointment
    {
        public long MeetingId { get; set; }
        public long ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime MeetingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }




    public class ClientGeoProvisions
    {
        public long ClientId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int[] Provisions { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string FlatNo { get; set; }
    }



}
