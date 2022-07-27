using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Billing
{
    public class InvoiceView
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public decimal Amounts { get; set; }
        public decimal BalanceAmont { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public short InvoiceStatus { get; set; }
        public List<ScheduleBillingModel> ScheduleList { get; set; }

    }
    public class InvoiceModel : BaseModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int PayerId { get; set; }
        public short InvoiceStatus { get; set; }
        public List<ScheduleBillingModel> ScheduleList { get; set; }
    }

}
