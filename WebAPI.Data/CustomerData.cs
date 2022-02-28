using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class CustomerData : ICustomerData
    {
        private IConfiguration configuration;
        public CustomerData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<StripeList<Customer>>> GetCustomerList()
        {
            ServiceResponse<StripeList<Customer>> obj = new ServiceResponse<StripeList<Customer>>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                var options = new CustomerListOptions
                {
                    Limit = 30,
                };
                var service = new CustomerService();
                StripeList<Customer> customers = service.List(
                  options);
                obj.Data = customers;
                obj.Result = true;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        public async Task<ServiceResponse<Customer>> GetCustomerbyId(string CId)
        {
            ServiceResponse<Customer> obj = new ServiceResponse<Customer>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                var service = new CustomerService();
                string response = (service.Get(CId)).StripeResponse.Content.ToString();
                var customer = JsonConvert.DeserializeObject<Customer>(response);
                obj.Data = customer;
                obj.Result = true;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }
    }
}
