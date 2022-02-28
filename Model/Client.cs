using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_SAMPLE.Model
{
    public class Client
    {
        public int ClientId { get; set; }
        public string BillTo { get; set; }
        public string ClientName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Ethnicity { get; set; }
        public string Email { get; set; }
        public string InsurenceId { get; set; }
        public string OfChild { get; set; }
        public string SSN { get; set; }
        public string ExtClientId { get; set; }
        public string Gender { get; set; }
        public string Nurse { get; set; }
        public string ClassCordinator { get; set; }
        public string MaritalStatus { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CState { get; set; }
        public string ZipCode { get; set; }
        public string ReferredBy { get; set; }
        public string EmgContact { get; set; }
        public string CaseWorkerPhone { get; set; }
        public string CaseWorkerEmail { get; set; }
        public int IsActive { get; set; }

        public string EmgPhone { get; set; }
        public string EmgEmail { get; set; }

    }
}
