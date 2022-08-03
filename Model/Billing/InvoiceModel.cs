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
        public List<ScheduleInvoiceModel> ScheduleList { get; set; }

    }






    public class SearchInvoice
    {

        public DateTime InvoiceDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PayerId { get; set; }
        public short InvoiceStatus { get; set; }
        public int ClientId { get; set; }
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







    public class ScheduleInvoiceModel
    {
        public int InvoiceId { get; set; }
        public int ScheduleRateId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public short InvoiceStatus { get; set; }
        public short PaymentStatus { get; set; }
        public string InvoiceNo { get; set; }
        public decimal ScheduleCost { get; set; }
        public int MeetingId { get; set; }
        public int EmpId { get; set; }
        public int PayerId { get; set; }
        public string PayerName { get; set; }
        public string PayerFax { get; set; }
        public string PayerEmail { get; set; }
        public string PayerPhone { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientCountry { get; set; }
        public string ClientState { get; set; }
        public string ClientCity { get; set; }
        public string ClientZipCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime ServiceDate { get; set; }
        public string EmpName { get; set; }
        public string BillingCode { get; set; }
        public int BillingUnits { get; set; }
        public decimal BillingRate { get; set; }
        public decimal BillingTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public short ScheduleStatus { get; set; }
        public short BillingStatus { get; set; }
        public short PayrollStatus { get; set; }

    }


}
