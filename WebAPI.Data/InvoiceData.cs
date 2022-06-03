using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class InvoiceData : IInvoiceData
    {
        private IConfiguration configuration;
        public InvoiceData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<StripeList<Invoice>>> GetInvoiceList()
        {
            ServiceResponse<StripeList<Invoice>> obj = new ServiceResponse<StripeList<Invoice>>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                var options = new InvoiceListOptions
                {
                    Limit = 10,
                };
                var service = new InvoiceService();
                StripeList<Invoice> invoices = service.List(
                  options);
                obj.Data = invoices;
                obj.Result = true;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        public async Task<ServiceResponse<Invoice>> GetInvoicebyId(string InvId)
        {
            ServiceResponse<Invoice> obj = new ServiceResponse<Invoice>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                var service = new InvoiceService();
                string response = (service.Get(InvId)).StripeResponse.Content.ToString();
                var invoice = JsonConvert.DeserializeObject<Invoice>(response);
                obj.Data = invoice;
                obj.Result = true;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        public async Task<ServiceResponse<Invoice>> GenerateInvoice(GenerateInvoice ginvoice)
        {
            ServiceResponse<Invoice> obj = new ServiceResponse<Invoice>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                // STEP 1 : Create Product

                var prdoptions = new ProductCreateOptions { Name = ginvoice.description };
                var prdservice = new ProductService();
                Product product = prdservice.Create(prdoptions);

                // STEP 2 : Create Price for Product

                var priceoptions = new PriceCreateOptions
                {
                    Product = product.Id,
                    UnitAmount = (long?)Convert.ToDouble(ginvoice.amount),
                    Currency = "usd",
                };
                var priceservice = new PriceService();
                Price price = priceservice.Create(priceoptions);

                // STEP 3 : Create Invoice for Price & Customer

                var options = new InvoiceItemCreateOptions
                {
                    Customer = ginvoice.custId,
                    Price = price.Id,
                };
                var service = new InvoiceItemService();
                var invoiceItem = service.Create(options);

                var invoiceOptions = new InvoiceCreateOptions
                {
                    Customer = ginvoice.custId,
                    CollectionMethod = "send_invoice", // Use your email address for testing purposes
                    AutoAdvance = true,
                    DueDate = DateTime.Now.AddDays(60)
                };
                var invoiceService = new InvoiceService();
                var response = (invoiceService.Create(invoiceOptions)).StripeResponse.Content.ToString();
                Invoice invoice = JsonConvert.DeserializeObject<Invoice>(response);
                // STEP 4 : Send Invoice

                var sendservice = new InvoiceService();
                sendservice.SendInvoice(invoice.Id);

                obj.Data = invoice;
                obj.Result = true;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }

        public async Task<ServiceResponse<Invoice>> PayInvoice(string InvId)
        {
            ServiceResponse<Invoice> obj = new ServiceResponse<Invoice>();
            try
            {
                StripeConfiguration.ApiKey = configuration.GetConnectionString("StripeAPIKeys").ToString();

                var service = new InvoiceService();
                var payoptions = new InvoicePayOptions
                {
                    PaymentMethod = "card_1KHBbDC6Bk31hqIY7F5g0TIx"
                };
                var response = (service.Pay(InvId, payoptions)).StripeResponse.Content.ToString();
                var invoice = JsonConvert.DeserializeObject<Invoice>(response);
                obj.Data = invoice;
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
