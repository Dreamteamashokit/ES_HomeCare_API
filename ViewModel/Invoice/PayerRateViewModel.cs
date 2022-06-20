using System;

namespace ES_HomeCare_API.ViewModel.Invoice
{
    public class PayerRateViewModel
    {
        public int RateId { get; set; }
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public string ServiceCode { get; set; }
        public string Type { get; set; }
        public string BillCode { get; set; }
        public string RevenueCode { get; set; }
        public decimal TaxRate { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public decimal Hourly { get; set; }
        public decimal LiveIn { get; set; }
        public decimal Visit { get; set; }
        public string Unit { get; set; }
        public string Modifiers1 { get; set; }
        public string Modifiers2 { get; set; }
        public string Modifiers3 { get; set; }
        public string Modifiers4 { get; set; }
        public string PlaceOfService { get; set; }
        public string MutualGroup { get; set; }
        public string Notes { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
