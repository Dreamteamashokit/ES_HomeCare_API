namespace ES_HomeCare_API.Model.Employee
{
    public class AttendanceModel : BaseModel
    {
        //Create Reason Master
        public int AttendanceId { get; set; }
        public int ReasonId { get; set; }
        public string ReasonName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Notes { get; set; }
    }
}
