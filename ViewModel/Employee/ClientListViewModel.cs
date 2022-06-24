using System;

namespace ES_HomeCare_API.ViewModel.Employee
{
    public class ClientListViewModel
    {
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Diagnosis { get; set; }
        public bool BedBath { get; set; }
        public bool Footcare { get; set; }
        public bool Skincare { get; set; }
        public bool SpongeBath { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingStartTime { get; set; }
        public string MeetingEndTime { get; set; }
        //public ClientAddressViewModel clientAddress { get; set; }
        public string FlatNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int MeetingId { get; set; }
    }

    //public class ClientAddressViewModel
    //{
    //    public int AddressId { get; set; }
    //    public string FlatNo { get; set; }
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string Country { get; set; }
    //    public string State { get; set; }
    //    public string ZipCode { get; set; }
    //    public decimal Longitude { get; set; }
    //    public decimal Latitude { get; set; }
    //}
}
