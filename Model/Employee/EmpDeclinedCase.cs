


using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class EmpDeclinedCase :BaseModel
    {

        public int EmpId { get; set; }
        public string RepotedDate { get; set; }
        public long clientId { get; set; }
        public long Casetypeid { get; set; }
        public string DeclineReason { get; set; }
        public string AssignmentStart { get; set; }
        public string Note { get; set; }
        public int Day { get; set; }
        public int week { get; set; }        
        public string ClientName { get; set; }
        public string CasetypeName { get; set; }


    }
}
