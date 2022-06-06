using System;

namespace ES_HomeCare_API.Model.Billing
{
    public class BillingModel
    {
        public long BillingId { get; set; }
        public long PayerId { get; set; }
        public long ContractClientId { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string HoursAuthorizedPerWeek { get; set; }
        public string HoursAuthorizedPerMonth { get; set; }
        public string HoursAuthorizedEntirePeriod { get; set; }
        public long ServiceCode { get; set; }
        public string OccurencesAuthorizedPerWeek { get; set; }
        public string OccurencesAuthorizedPerMonth { get; set; }
        public string OccurencesAuthorizedEntirePeriod { get; set; }
        public string DaysOfWeekNotes { get; set; }
        public long BRServiceCode_SAT { get; set; }
        public long BRServiceCode_SUN { get; set; }
        public long BRServiceCode_MON { get; set; }
        public long BRServiceCode_TUE { get; set; }
        public long BRServiceCode_WED { get; set; }
        public long BRServiceCode_THU { get; set; }
        public long BRServiceCode_FRI { get; set; }
        public long Quantity_SAT { get; set; }
        public long Quantity_SUN { get; set; }
        public long Quantity_MON { get; set; }
        public long Quantity_TUE { get; set; }
        public long Quantity_WED { get; set; }
        public long Quantity_THU { get; set; }
        public long Quantity_FRI { get; set; }
        public string PeriodEpisode_Notes { get; set; }
        public long CreatedBy { get; set; }
    }
}
