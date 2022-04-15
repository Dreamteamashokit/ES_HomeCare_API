using ES_HomeCare_API.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Client
{
    public class ClientModel : AccountUserModel
    {
        public int ClientId { get; set; }
        public string BillTo { get; set; }
        public string Contact { get; set; }
        public int NurseId { get; set; }
        public short OfChild { get; set; }
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
        public string CoordinatorName { get; set; }
        public string NurseName { get; set; }
        public string GenderName { get; set; }
        public string EthnicityName { get; set; }
        public string MaritalStatusName { get; set; }

    }
}
