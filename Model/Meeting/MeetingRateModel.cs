using System;

namespace ES_HomeCare_API.Model.Meeting
{
    public class MeetingRateModel
    {
        public long MeetingRateId { get; set; }
        public long BillingId { get; set; }
        public long PayRollRateId { get; set; }
        public long MeetingId { get; set; }
        public string BillingCode { get; set; }
        public int BillingUnits { get; set; }
        public decimal? BillingRate { get; set; }
        public decimal? BillingTotal { get; set; }
        public int BillingStatus { get; set; }
        public int BillingTravelTime { get; set; }

        public decimal? PayrollUnitsPaid { get; set; }
        public decimal? PayrollPayRate { get; set; }
        public decimal? PayrollPayTotal { get; set; }
        public int PayrollPayStatus { get; set; }
        public decimal? PayrollMileage { get; set; }
        public decimal? PayrollPublicTrans { get; set; }
        public decimal? PayrollMisc { get; set; }
        public bool PayrollDoNotPay { get; set; }
        public string SentPayrollDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
