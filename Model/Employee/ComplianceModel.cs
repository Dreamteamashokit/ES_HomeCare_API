namespace ES_HomeCare_API.Model.Employee
{
    public class ComplianceModel:BaseModel
    {
        public int ComplianceId { get; set; }
        public int EmpId { get; set; }   
        public string DueDate { get; set; }
        public string CompletedOn { get; set; }
        public string Category { get; set; }

        public string Code { get; set; }

        public string Result { get; set; }

        public int Nurse { get; set; }

        public string Notes { get; set; }

        




    }
}
