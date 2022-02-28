using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<StripeList<Customer>>> GetCustomerList();
        Task<ServiceResponse<Customer>> GetCustomerbyId(string CId);
    }
}
