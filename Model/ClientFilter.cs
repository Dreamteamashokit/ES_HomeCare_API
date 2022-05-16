using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class ClientFilter
    {
        public int Status { get; set; }
        public string State { get; set; }
        public int Coordinator { get; set; }
        public int Payer { get; set; }
    }

    public class ClientResult
    {
        public int ClientId { get; set; }
        public string UserKey { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }

    }
}
