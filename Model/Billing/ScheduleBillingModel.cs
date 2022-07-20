using System;

namespace ES_HomeCare_API.Model.Billing
{
    public class SearchSchedule
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PayerId { get; set; }
        public int EmpId { get; set; }
        public int ClientId { get; set; }
    }

    public class ScheduleBillingModel
    {
        public int ScheduleRateId { get; set; }
        public int PayerId { get; set; }
        public int EmpId { get; set; }
        public int ClientId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string PayerName { get; set; }
        public string EmpName { get; set; }
        public string ClientName { get; set; }
        public string BillingCode { get; set; }
        public int BillingUnits { get; set; }
        public decimal BillingRate { get; set; }
        public decimal BillingTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public short ScheduleStatus { get; set; }//MeetingStatus
        public short BillingStatus { get; set; }
        public short PayrollStatus { get; set; }

    }
}
