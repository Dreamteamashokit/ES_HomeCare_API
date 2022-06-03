using ES_HomeCare_API.Model;
using ES_HomeCare_API.ViewModel.Invoice;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}
