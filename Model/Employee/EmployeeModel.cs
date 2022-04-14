using ES_HomeCare_API.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Employee
{
    public class EmployeeModel : AccountUserModel
    {
        public int EmpId { get; set; }  
        public string EmpKey { get; set; }  
        public short EmpType { get; set; }  
        public string DateOfHire { get; set; }
        public string DateOfFirstCase { get; set; }   
        public short Enthnicity { get; set; }
        public short Dependents { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TaxState { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string Notes { get; set; }

    }
}


