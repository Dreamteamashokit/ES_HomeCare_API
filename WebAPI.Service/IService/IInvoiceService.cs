using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<StripeList<Invoice>>> GetInvoiceList();
        Task<ServiceResponse<Invoice>> GetInvoicebyId(string InvId);
        Task<ServiceResponse<Invoice>> GenerateInvoice(GenerateInvoice invoice);
        Task<ServiceResponse<Invoice>> PayInvoice(string InvId);

        Task<ServiceResponse<string>> AddUpdatePayerRate(PayerRateModel payerRateModel);
        Task<ServiceResponse<string>> AddUpdateBilling(BillingModel billingModel);
        Task<ServiceResponse<string>> DeleteBillng(long billingId);
        Task<ServiceResponse<BillingViewModel>> GetBillingDetailsByBillingId(long billingId);
        Task<ServiceResponse<IList<BillingViewModel>>> GetActiveBillAndExpiredBill(bool status);
        Task<ServiceResponse<IList<PayerServiceCodeModel>>> GetServiceCodeByPayerId(long payerId);
    }
}
