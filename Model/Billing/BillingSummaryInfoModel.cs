namespace ES_HomeCare_API.Model.Billing
{
    public class BillingSummaryInfoModel
    {
        public int BillingId { get; set; }
        public int PayerId { get; set; }
        public string Type { get; set; }
        public string DueDate { get; set; }
        public string BillToName { get; set; }
    }
}
