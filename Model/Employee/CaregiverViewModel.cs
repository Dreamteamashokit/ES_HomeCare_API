using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class CaregiverViewModel
    {
        public string providerTaxId { get; set; }
        public string qualifier { get; set; }
        public string externalID { get; set; }
        public int npi { get; set; }
        public int ssn { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string type { get; set; }
        public int stateRegistrationID { get; set; }
        public int professionalLicenseNumber { get; set; }
        public DateTime hireDate { get; set; }

        public address address { get; set; }
    }

    public class address
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
    }
}
