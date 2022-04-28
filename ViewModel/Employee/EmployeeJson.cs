using ES_HomeCare_API.ViewModel.Account;

namespace ES_HomeCare_API.ViewModel.Employee
{ 

    public class EmployeeJson : AccountJson
    {
        public int EmpId { get; set; }
        public string EmpKey { get; set; }
        public short EmpType { get; set; }
        public string EmpTypeName { get; set; }
        public string DateOfHire { get; set; }
        public string DateOfFirstCase { get; set; }
        public short Enthnicity { get; set; }
        public string EnthnicityName { get; set; }
        public short Dependents { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TaxState { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string Notes { get; set; }

    }
}
