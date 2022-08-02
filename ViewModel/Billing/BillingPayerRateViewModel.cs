using System;

namespace ES_HomeCare_API.ViewModel.Billing
{
    public class BillingPayerRateViewModel
    {
        public long BillingId { get; set; }
        public long PayerId { get; set; }
        public string PayerName { get; set; }
        public long ClientId { get; set; }
        public long RateId { get; set; }
        public string BillCode { get; set; }
        public decimal? TaxRate { get; set; }
        public int Type { get; set; }
        public string Unit { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? MeetingDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public long CalculateUnit { get; set; }
        public decimal? BillTotal { get; set; }
        public string BillingStatus { get; set; }
        public long EmpRateId { get; set; }
        public long EmpId { get; set; }
        public decimal? EmpHourly { get; set; }
        public decimal? EmpLiveIn { get; set; }
        public decimal? EmpMileage { get; set; }
        public decimal? EmpVisit { get; set; }
    }

    public class BillingPayRollRateViewModel
    {
        public long BillingId { get; set; }
        public long PayerId { get; set; }
        public string PayerName { get; set; }
        public long ClientId { get; set; }
        public long EmpId { get; set; }
        public long MeetingId { get; set; }
        public long RateId { get; set; }
        public decimal? TaxRate { get; set; }
        public DateTime? MeetingDate { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
