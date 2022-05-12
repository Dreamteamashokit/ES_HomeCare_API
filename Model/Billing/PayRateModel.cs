namespace ES_HomeCare_API.Model.Billing
{
    public class PayRateModel :BaseModel
    {
        public int Payer { get; set; }
        public int PayType { get; set; }
        public short Unit { get; set; }
        public decimal Rate { get; set; }
        public decimal TaxRate { get; set; }
        public string EffectiveFrom { get; set; }
        public string EffectiveTo { get; set; }    
        public string ServiceCode { get; set; }
        public string BillCode { get; set; }
        public string RevenueCode { get; set; }
        public string Notes { get; set; }

        
    }
}
