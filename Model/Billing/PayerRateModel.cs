using System;
using System.ComponentModel.DataAnnotations;

namespace ES_HomeCare_API.Model.Billing
{
    public class PayerRateModel
    {
        [Key]
        public int @Rateid { get; set; }
        public int @Payerid { get; set; }
        public string @ServiceCode { get; set; }
        public string @BillCode { get; set; }
        public string @RevenueCode { get; set; }
        public decimal @TaxRate { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public decimal Hourly { get; set; }
        public decimal Livein { get; set; }
        public decimal Visit { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
    }
}
