namespace ES_HomeCare_API.Model.Employee
{
 
    public class StatusModel
    {
        public string Reason { get; set; }
        public bool Resume { get; set; }
        public bool Rehire { get; set; }
        public string EffectiveDate { get; set; }
        public string ReturnDate { get; set; }
        public string Note { get; set; }
        public int Scheduling { get; set; }
        public long OfficeUserId { get; set; }
        public long EmployeeId { get; set; }
        public long TypeStatusId { get; set; }
        public bool Text { get; set; }
        public bool Screen { get; set; }
        public bool Email { get; set; }
    }
}
