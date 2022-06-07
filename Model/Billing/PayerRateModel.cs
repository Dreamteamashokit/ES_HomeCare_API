using System;
using System.ComponentModel.DataAnnotations;

namespace ES_HomeCare_API.Model.Billing
{
    public class PayerRateModel
    {
        [Key]
        public long Rateid { get; set; }
        public long Payerid { get; set; }
        public string ServiceCode { get; set; }
        public long Type { get; set; }
        public string BillCode { get; set; }
        public string RevenueCode { get; set; }

        public decimal TaxRate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Hourly { get; set; }
        public decimal Livein { get; set; }
        public decimal Visit { get; set; }
        public string Unit { get; set; }
        public string Modifiers1 { get; set; }
        public string Modifiers2 { get; set; }
        public string Modifiers3 { get; set; }
        public string Modifiers4 { get; set; }
        public string PlaceOfService { get; set; }
        public bool MutualGroup { get; set; }
        public string Notes { get; set; }
        public long CreatedBy { get; set; }
    }
}
