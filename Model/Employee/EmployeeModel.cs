using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Employee
{


    public class EmployeeModel : BaseModel
    {
        public int EmpId { get; set; }
        public string EmpKey { get; set; }
        public string SSN { get; set; }
        public short EmpType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string DateOfHire { get; set; }
        public string HomePhone { get; set; }
        public string DateOfFirstCase { get; set; }
        public short Gender { get; set; }
        public int? HRSupervisor { get; set; }
        public short Enthnicity { get; set; }
        public short MaritalStatus { get; set; }
        public short Dependents { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TaxState { get; set; }
        public string ZipCode { get; set; }       
        public string EmgContact { get; set; }
        public string EmgPhone { get; set; }
        public string Municipality { get; set; }
        public string Notes { get; set; }
        public short EmpStatus { get; set; }
        public bool IsDeleted { get; set; }


    }
}


