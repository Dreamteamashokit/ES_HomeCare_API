using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Client
{
    public class ClientModel : BaseModel
    {
        public int ClientId { get; set; }
        public string BillTo { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public int? Coordinator  { get; set; }
        public int? Nurse { get; set; }
        public short OfChild { get; set; }
        public short Gender { get; set; }
        public short Ethnicity { get; set; }
        public short MaritalStatus { get; set; }
        public string EmgContact { get; set; }
        public string AltId { get; set; }
        public string ID2 { get; set; }
        public string ID3 { get; set; }
        public string InsuranceID { get; set; }
        public string WorkerName { get; set; }
        public string WorkerContact { get; set; }
        public string ReferredBy { get; set; }
        public short PriorityCode { get; set; }
        public bool TimeSlip { get; set; }
        public bool IsHourly { get; set; }
        public short ClientStatus { get; set; }
        public bool IsDeleted { get; set; }

    }
}
