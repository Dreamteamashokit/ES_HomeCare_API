using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceData data;
        public InvoiceService(IInvoiceData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<Invoice>> GenerateInvoice(GenerateInvoice invoice)
        {
            return await data.GenerateInvoice(invoice);
        }

        public async Task<ServiceResponse<Invoice>> GetInvoicebyId(string InvId)
        {
            return await data.GetInvoicebyId(InvId);
        }

        public async Task<ServiceResponse<StripeList<Invoice>>> GetInvoiceList()
        {
            return await data.GetInvoiceList();
        }

        public async Task<ServiceResponse<Invoice>> PayInvoice(string InvId)
        {
            return await data.PayInvoice(InvId);
        }

        public async Task<ServiceResponse<string>> AddUpdatePayerRate(PayerRateModel payerRateModel)
        {
            return await data.AddUpdatePayerRate(payerRateModel);
        }

        public async Task<ServiceResponse<string>> AddUpdateBilling(BillingModel billingModel)
        {
            return await data.AddUpdateBilling(billingModel);
        }

        public async Task<ServiceResponse<string>> DeleteBillng(long billingId)
        {
            return await data.DeleteBillng(billingId);
        }

        public async Task<ServiceResponse<BillingViewModel>> GetBillingDetailsByBillingId(long billingId)
        {
            return await data.GetBillingDetailsByBillingId(billingId);
        }
    }
}
