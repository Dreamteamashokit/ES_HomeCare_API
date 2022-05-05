using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class AddressModel:BaseModel
    {       
        public int AddressId { get; set; }
        public int AddressType { get; set; }
        public decimal Latitude { get; set; }   
        public decimal Longitude { get; set; }
        public string Owner { get; set; }
        public string FlatNo { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
            
    }
}
