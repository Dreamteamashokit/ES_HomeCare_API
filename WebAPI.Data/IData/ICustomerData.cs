using ES_HomeCare_API.Model;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface ICustomerData
    {
        Task<ServiceResponse<StripeList<Customer>>> GetCustomerList();
        Task<ServiceResponse<Customer>> GetCustomerbyId(string CId);
    }
}
