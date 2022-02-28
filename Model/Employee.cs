using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_SAMPLE.Model
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string HasDOB { get; set; }
        public string HasEmail { get; set; }
        public string Types { get; set; } = "HHA";
        public string Coordinator { get; set; } = "NA";
        public string Schedule { get; set; } = "NA";
        public string Medical { get; set; } = "NA";
        public string InService { get; set; } = "NA";
        public string Status { get; set; } = "NA";
        public string Profile { get; set; } = "<i class=\"fa fa-american-sign-language-interpreting fa-5x\" aria-hidden=\"true\"> </i>";
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string DateOfHire { get; set; }
        public string DateOfFirstCase { get; set; }
        public string DOB { get; set; }
        public string SSN { get; set; }
        public string ExtEmpId { get; set; }
        public string Gender { get; set; }
        public string HrSupervisor { get; set; }
        public string Enthnicity { get; set; }
        public string MaritalStatus { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CState { get; set; }
        public string ZipCode { get; set; }
        public string EmgContact { get; set; }
        public string EmgPhone { get; set; }
        public string EnteredDate { get; set; }
        public int IsActive { get; set; }
    }
}
