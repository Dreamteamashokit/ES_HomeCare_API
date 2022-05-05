using System;

namespace ES_HomeCare_API.Model
{
    public class AvailbilityRequest
    {
        public int CaseId { get; set; }
        public int EmpTypeId { get; set; }
        public int PayTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public int[] ProvisionsList { get; set; }

    }
}
