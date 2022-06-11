using System;

namespace ES_HomeCare_API.Model.Employee
{
    public class ComplianceModel : BaseModel
    {
        public int ComplianceId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CompletedOn { get; set; }
        public string Category { get; set; }
        public string Code { get; set; }
        public string Result { get; set; }
        public int Nurse { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
    }
}
