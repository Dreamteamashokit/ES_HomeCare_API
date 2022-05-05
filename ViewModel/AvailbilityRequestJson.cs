namespace ES_HomeCare_API.ViewModel
{
    public class AvailbilityRequestJson
    {
        public int CaseId { get; set; }
        public int EmpTypeId { get; set; }
        public int PayTypeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public int[] ProvisionsList { get; set; }
    }
}
