using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model.Employee
{
    public class EmployeeList
    {

        public int EmpId { get; set; }
        public string EmpKey { get; set; }
        public string EmpName { get; set; }
        public string CellPhone { get; set; }
        public string EmpType { get; set; }
        public short TypeId { get; set; }
        public string SSN { get; set; }
        public string TaxState { get; set; }
        public short IsActive { get; set; }
        public bool HasDOB { get; set; }
        public bool HasEmail { get; set; }
        public bool Medical { get; set; }
        public bool InService { get; set; }
        public int ManagerId { get; set; }        
        public string Coordinator { get; set; }
     
    

    }
}
