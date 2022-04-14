using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class EmpDeclinedCase : BaseModel
    {
        public string RepotedDate { get; set; }
        public long ClientId { get; set; }
        public long Casetypeid { get; set; }
        public string DeclineReason { get; set; }
        public string AssignmentStart { get; set; }
        public string Note { get; set; }
        public int Day { get; set; }
        public int Week { get; set; }
        public string ClientName { get; set; }
        public string CasetypeName { get; set; }


    }
}
