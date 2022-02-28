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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerData data;
        public CustomerService(ICustomerData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<StripeList<Customer>>> GetCustomerList()
        {
            return await data.GetCustomerList();
        }

        public async Task<ServiceResponse<Customer>> GetCustomerbyId(string CId)
        {
            return await data.GetCustomerbyId(CId);
        }
    }
}
