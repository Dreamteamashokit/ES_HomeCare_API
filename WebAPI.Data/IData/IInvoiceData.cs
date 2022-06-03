using ES_HomeCare_API.Model;
using ES_HomeCare_API.ViewModel.Invoice;
using Stripe;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IInvoiceData
    {
        Task<ServiceResponse<StripeList<Invoice>>> GetInvoiceList();
        Task<ServiceResponse<Invoice>> GetInvoicebyId(string InvId);
        Task<ServiceResponse<Invoice>> GenerateInvoice(GenerateInvoice invoice);
        Task<ServiceResponse<Invoice>> PayInvoice(string InvId);


    }
}
