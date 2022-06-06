using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class EmpDeclinedCase : BaseModel
    {

        
        public long DeclinedCaseId { get; set; }
        public string ReportedDate { get; set; }
        public string AssignmentStart { get; set; }
        public long ClientId { get; set; }
        public long CaseTypeId { get; set; }
        public string DeclineReason { get; set; }
  
        public string Note { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public string ClientName { get; set; }
        public string CaseTypeName { get; set; }

        public DateTime ReportedDateTime { get; set; }
        public DateTime AssignmentStartDateTime { get; set; }


    }
}
