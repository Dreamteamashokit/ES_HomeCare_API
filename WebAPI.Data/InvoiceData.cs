using Dapper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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


        public async Task<ServiceResponse<string>> AddUpdatePayerRate(PayerRateModel payerRateModel)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("AddUpdateRates", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Rateid", payerRateModel.Rateid);
                    cmd.Parameters.AddWithValue("@Payerid", payerRateModel.Payerid);
                    cmd.Parameters.AddWithValue("@ServiceCode", payerRateModel.ServiceCode);
                    cmd.Parameters.AddWithValue("@Type", payerRateModel.Type);
                    cmd.Parameters.AddWithValue("@BillCode", payerRateModel.BillCode);
                    if (payerRateModel.RevenueCode != null)
                    {
                        cmd.Parameters.AddWithValue("@RevenueCode", payerRateModel.RevenueCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RevenueCode", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@TaxRate", payerRateModel.TaxRate);

                    if (payerRateModel.ValidFrom != null)
                    {
                        cmd.Parameters.AddWithValue("@ValidFrom", payerRateModel.ValidFrom);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ValidFrom", DBNull.Value);
                    }
                    if (payerRateModel.ValidTo != null)
                    {
                        cmd.Parameters.AddWithValue("@ValidTo", payerRateModel.ValidTo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ValidTo", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@Hourly", payerRateModel.Hourly);
                    cmd.Parameters.AddWithValue("@Livein", payerRateModel.Livein);
                    cmd.Parameters.AddWithValue("@Visit", payerRateModel.Visit);
                    if (payerRateModel.Unit != null)
                    {
                        cmd.Parameters.AddWithValue("@Unit", payerRateModel.Unit);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Unit", "");
                    }
                    cmd.Parameters.AddWithValue("@Modifiers1", payerRateModel.Modifiers1 == null? "" : payerRateModel.Modifiers1);
                    cmd.Parameters.AddWithValue("@Modifiers2", payerRateModel.Modifiers2 == null ? "" : payerRateModel.Modifiers2);
                    cmd.Parameters.AddWithValue("@Modifiers3", payerRateModel.Modifiers3 == null ? "" : payerRateModel.Modifiers3);
                    cmd.Parameters.AddWithValue("@Modifiers4", payerRateModel.Modifiers4 == null ? "" : payerRateModel.Modifiers4);
                    cmd.Parameters.AddWithValue("@PlaceOfService", payerRateModel.PlaceOfService == null ? "" : payerRateModel.PlaceOfService);
                    cmd.Parameters.AddWithValue("@MutualGroup", payerRateModel.MutualGroup);
                    cmd.Parameters.AddWithValue("@Notes", payerRateModel.Notes == null ? "" : payerRateModel.Notes);
                    cmd.Parameters.AddWithValue("@CreatedBy", payerRateModel.CreatedBy);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (payerRateModel.Rateid > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Rate Updated Successfull.";
                    }
                    else
                    {
                        sres.Result = true;
                        sres.Data = "New Rate Created.";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return sres;
        }


        public async Task<ServiceResponse<string>> AddUpdateBilling(BillingModel billingModel)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("AddUpdateBilling", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillingId", billingModel.BillingId);
                    cmd.Parameters.AddWithValue("@PayerId", billingModel.PayerId);
                    cmd.Parameters.AddWithValue("@ContractClientId", billingModel.ContractClientId == null ? "" : billingModel.ContractClientId);
                    cmd.Parameters.AddWithValue("@AuthorizationNumber", billingModel.AuthorizationNumber == null ? "" : billingModel.AuthorizationNumber);
                    if (billingModel.FromDate != null)
                    {
                        cmd.Parameters.AddWithValue("@FromDate", billingModel.FromDate);
                    }
                    else {
                        cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                    }
                    if (billingModel.ToDate != null)
                    {
                        cmd.Parameters.AddWithValue("@ToDate", billingModel.ToDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@HoursAuthorizedPerWeek", billingModel.HoursAuthorizedPerWeek == null ? "" : billingModel.HoursAuthorizedPerWeek);
                    cmd.Parameters.AddWithValue("@HoursAuthorizedPerMonth", billingModel.HoursAuthorizedPerMonth == null ? "" : billingModel.HoursAuthorizedPerMonth);
                    cmd.Parameters.AddWithValue("@HoursAuthorizedEntirePeriod", billingModel.HoursAuthorizedEntirePeriod == null ? "" : billingModel.HoursAuthorizedEntirePeriod);
                    cmd.Parameters.AddWithValue("@ServiceCode", billingModel.ServiceCode);
                    cmd.Parameters.AddWithValue("@OccurencesAuthorizedPerWeek", billingModel.OccurencesAuthorizedPerWeek == null ? "" : billingModel.OccurencesAuthorizedPerWeek);
                    cmd.Parameters.AddWithValue("@OccurencesAuthorizedPerMonth", billingModel.OccurencesAuthorizedPerMonth == null ? "" : billingModel.OccurencesAuthorizedPerMonth);
                    cmd.Parameters.AddWithValue("@OccurencesAuthorizedEntirePeriod", billingModel.OccurencesAuthorizedEntirePeriod == null ? "" : billingModel.OccurencesAuthorizedEntirePeriod);
                    cmd.Parameters.AddWithValue("@DaysOfWeekNotes", billingModel.DaysOfWeekNotes == null ? "" : billingModel.DaysOfWeekNotes);
                    cmd.Parameters.AddWithValue("@BRServiceCode_SAT", billingModel.BRServiceCode_SAT);
                    cmd.Parameters.AddWithValue("@BRServiceCode_SUN", billingModel.BRServiceCode_SUN);
                    cmd.Parameters.AddWithValue("@BRServiceCode_MON", billingModel.BRServiceCode_MON);
                    cmd.Parameters.AddWithValue("@BRServiceCode_TUE", billingModel.BRServiceCode_THU);
                    cmd.Parameters.AddWithValue("@BRServiceCode_WED", billingModel.BRServiceCode_WED);
                    cmd.Parameters.AddWithValue("@BRServiceCode_THU", billingModel.BRServiceCode_THU);
                    cmd.Parameters.AddWithValue("@BRServiceCode_FRI", billingModel.BRServiceCode_FRI);
                    cmd.Parameters.AddWithValue("@Quantity_SAT", billingModel.Quantity_SAT);
                    cmd.Parameters.AddWithValue("@Quantity_SUN", billingModel.Quantity_SUN);
                    cmd.Parameters.AddWithValue("@Quantity_MON", billingModel.Quantity_MON);
                    cmd.Parameters.AddWithValue("@Quantity_TUE", billingModel.Quantity_TUE);
                    cmd.Parameters.AddWithValue("@Quantity_WED", billingModel.Quantity_WED);
                    cmd.Parameters.AddWithValue("@Quantity_THU", billingModel.Quantity_THU);
                    cmd.Parameters.AddWithValue("@Quantity_FRI", billingModel.Quantity_FRI);
                    cmd.Parameters.AddWithValue("@PeriodEpisode_Notes", billingModel.PeriodEpisode_Notes == null ? "" : billingModel.PeriodEpisode_Notes);
                    cmd.Parameters.AddWithValue("@CreatedBy", billingModel.CreatedBy);

                    con.Open();
                    int value = cmd.ExecuteNonQuery();
                    if (billingModel.BillingId > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Billing details updated Successfull.";
                    }
                    else
                    {
                        sres.Result = true;
                        sres.Data = "New Billing Created.";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return sres;
        }


        public async Task<ServiceResponse<string>> DeleteBillng(long billingId)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                var existsBilling = await GetBillingDetailsByBillingId(billingId);
                if (existsBilling != null && existsBilling.Result == true && existsBilling.Data != null)
                {
                    using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                    {
                        SqlCommand cmd = new SqlCommand("DeleteBilling", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BillingId", billingId);
                        con.Open();
                        int value = cmd.ExecuteNonQuery();
                        sres.Result = true;
                        sres.Data = "Billing detail Deleted successfull.";
                    }
                }
                else
                {
                    sres.Result = false;
                    sres.Data = "Billing does not exists.";
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return sres;
        }


        public async Task<ServiceResponse<BillingViewModel>> GetBillingDetailsByBillingId(long billingId)
        {
            ServiceResponse<BillingViewModel> obj = new ServiceResponse<BillingViewModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetBillingDetailsByBillingId", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BillingId", billingId);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            obj.Data = new BillingViewModel
                            {
                                BillingId = Convert.ToInt32(table.Rows[i]["BillingId"]),
                                PayerId = Convert.ToInt32(table.Rows[i]["PayerId"]),
                                ContractClientId = table.Rows[i]["ContractClientId"] == null ? "" :  table.Rows[i]["ContractClientId"].ToString(),
                                AuthorizationNumber = table.Rows[i]["AuthorizationNumber"].ToString(),
                                FromDate = Convert.ToDateTime(table.Rows[i]["FromDate"]),
                                ToDate = Convert.ToDateTime(table.Rows[i]["ToDate"]),
                                HoursAuthorizedPerWeek = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                HoursAuthorizedPerMonth = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                HoursAuthorizedEntirePeriod = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                ServiceCode = Convert.ToInt32(table.Rows[i]["ServiceCode"]),
                                OccurencesAuthorizedPerWeek = table.Rows[i]["OccurencesAuthorizedPerWeek"].ToString(),
                                OccurencesAuthorizedPerMonth = table.Rows[i]["OccurencesAuthorizedPerMonth"].ToString(),
                                OccurencesAuthorizedEntirePeriod = table.Rows[i]["OccurencesAuthorizedEntirePeriod"].ToString(),
                                DaysOfWeekNotes = table.Rows[i]["DaysOfWeekNotes"].ToString(),
                                BRServiceCode_SAT = Convert.ToInt32(table.Rows[i]["BRServiceCode_SAT"]),
                                BRServiceCode_SUN = Convert.ToInt32(table.Rows[i]["BRServiceCode_SUN"]),
                                BRServiceCode_MON = Convert.ToInt32(table.Rows[i]["BRServiceCode_MON"]),
                                BRServiceCode_TUE = Convert.ToInt32(table.Rows[i]["BRServiceCode_TUE"]),
                                BRServiceCode_WED = Convert.ToInt32(table.Rows[i]["BRServiceCode_WED"]),
                                BRServiceCode_THU = Convert.ToInt32(table.Rows[i]["BRServiceCode_THU"]),
                                BRServiceCode_FRI = Convert.ToInt32(table.Rows[i]["BRServiceCode_FRI"]),
                                Quantity_SAT = Convert.ToInt32(table.Rows[i]["Quantity_SAT"]),
                                Quantity_SUN = Convert.ToInt32(table.Rows[i]["Quantity_SUN"]),
                                Quantity_MON = Convert.ToInt32(table.Rows[i]["Quantity_MON"]),
                                Quantity_TUE = Convert.ToInt32(table.Rows[i]["Quantity_TUE"]),
                                Quantity_WED = Convert.ToInt32(table.Rows[i]["Quantity_WED"]),
                                Quantity_THU = Convert.ToInt32(table.Rows[i]["Quantity_THU"]),
                                Quantity_FRI = Convert.ToInt32(table.Rows[i]["Quantity_FRI"]),
                                PeriodEpisode_Notes = table.Rows[i]["PeriodEpisode_Notes"].ToString()
                            };
                            obj.Result = true;
                            obj.Message = "Billing details retrive successfull.";
                        }
                    }
                    else
                    {
                        obj.Result = true;
                        obj.Message = "Billing details does not exists.";
                    }
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }


        public async Task<ServiceResponse<IList<BillingViewModel>>> GetActiveBillAndExpiredBill(bool status)
        {
            ServiceResponse<IList<BillingViewModel>> obj = new ServiceResponse<IList<BillingViewModel>>();
            IList<BillingViewModel> objBillingList = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetActiveBillAndExpiredBill", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", status);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        objBillingList = new List<BillingViewModel>();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            objBillingList.Add(new BillingViewModel
                            {
                                BillingId = Convert.ToInt32(table.Rows[i]["BillingId"]),
                                PayerId = Convert.ToInt32(table.Rows[i]["PayerId"]),
                                ContractClientId = table.Rows[i]["ContractClientId"].ToString(),
                                AuthorizationNumber = table.Rows[i]["AuthorizationNumber"].ToString(),
                                FromDate = string.IsNullOrWhiteSpace(table.Rows[i]["FromDate"].ToString()) ? (DateTime?)null : DateTime.Parse(table.Rows[i]["FromDate"].ToString()),
                                ToDate = string.IsNullOrWhiteSpace(table.Rows[i]["ToDate"].ToString()) ? (DateTime?)null : DateTime.Parse(table.Rows[i]["ToDate"].ToString()),
                                HoursAuthorizedPerWeek = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                HoursAuthorizedPerMonth = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                HoursAuthorizedEntirePeriod = table.Rows[i]["HoursAuthorizedPerWeek"].ToString(),
                                ServiceCode = Convert.ToInt32(table.Rows[i]["ServiceCode"]),
                                OccurencesAuthorizedPerWeek = table.Rows[i]["OccurencesAuthorizedPerWeek"].ToString(),
                                OccurencesAuthorizedPerMonth = table.Rows[i]["OccurencesAuthorizedPerMonth"].ToString(),
                                OccurencesAuthorizedEntirePeriod = table.Rows[i]["OccurencesAuthorizedEntirePeriod"].ToString(),
                                DaysOfWeekNotes = table.Rows[i]["DaysOfWeekNotes"].ToString(),
                                BRServiceCode_SAT = Convert.ToInt32(table.Rows[i]["BRServiceCode_SAT"]),
                                BRServiceCode_SUN = Convert.ToInt32(table.Rows[i]["BRServiceCode_SUN"]),
                                BRServiceCode_MON = Convert.ToInt32(table.Rows[i]["BRServiceCode_MON"]),
                                BRServiceCode_TUE = Convert.ToInt32(table.Rows[i]["BRServiceCode_TUE"]),
                                BRServiceCode_WED = Convert.ToInt32(table.Rows[i]["BRServiceCode_WED"]),
                                BRServiceCode_THU = Convert.ToInt32(table.Rows[i]["BRServiceCode_THU"]),
                                BRServiceCode_FRI = Convert.ToInt32(table.Rows[i]["BRServiceCode_FRI"]),
                                Quantity_SAT = Convert.ToInt32(table.Rows[i]["Quantity_SAT"]),
                                Quantity_SUN = Convert.ToInt32(table.Rows[i]["Quantity_SUN"]),
                                Quantity_MON = Convert.ToInt32(table.Rows[i]["Quantity_MON"]),
                                Quantity_TUE = Convert.ToInt32(table.Rows[i]["Quantity_TUE"]),
                                Quantity_WED = Convert.ToInt32(table.Rows[i]["Quantity_WED"]),
                                Quantity_THU = Convert.ToInt32(table.Rows[i]["Quantity_THU"]),
                                Quantity_FRI = Convert.ToInt32(table.Rows[i]["Quantity_FRI"]),
                                PeriodEpisode_Notes = table.Rows[i]["PeriodEpisode_Notes"].ToString()
                            });
                        }

                        obj.Data = objBillingList;
                        obj.Result = true;
                        if (status)
                        {
                            obj.Message = "Active billing list retrive successfull.";
                        }
                        else
                        {
                            obj.Message = "Expired billing list retrive successfull.";
                        }
                    }
                    else
                    {
                        obj.Result = true;
                        obj.Message = "Billing records not exists.";
                    }
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }


        public async Task<ServiceResponse<IList<PayerServiceCodeModel>>> GetServiceCodeByPayerId(long payerId)
        {
            ServiceResponse<IList<PayerServiceCodeModel>> obj = new ServiceResponse<IList<PayerServiceCodeModel>>();
            IList<PayerServiceCodeModel> objServiceCOdeList = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetServiceCodeByPayerId", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PayerId", payerId);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        objServiceCOdeList = new List<PayerServiceCodeModel>();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            objServiceCOdeList.Add(new PayerServiceCodeModel
                            {
                                RateId = Convert.ToInt32(table.Rows[i]["RateId"]),
                                PayerId = Convert.ToInt32(table.Rows[i]["PayerId"]),
                                ServiceCode = table.Rows[i]["ServiceCode"].ToString(),
                            });
                        }

                        obj.Data = objServiceCOdeList;
                        obj.Result = true;
                        obj.Message = "ServiceCode list retrive successfull.";

                    }
                    else
                    {
                        obj.Result = true;
                        obj.Message = "ServiceCode does not exists.";
                    }
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }


        public async Task<ServiceResponse<IList<PayerRateViewModel>>> GetPayerRateList()
        {
            ServiceResponse<IList<PayerRateViewModel>> obj = new ServiceResponse<IList<PayerRateViewModel>>();
            IList<PayerRateViewModel> objPayerRateList = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetPayerRateList", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        objPayerRateList = new List<PayerRateViewModel>();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            PayerRateViewModel objPayerRate = new PayerRateViewModel();
                            objPayerRate.RateId = Convert.ToInt32(table.Rows[i]["RateId"]);
                            objPayerRate.PayerId = table.Rows[i]["PayerId"] == null ? 0 : Convert.ToInt32(table.Rows[i]["PayerId"]);
                            objPayerRate.PayerName = table.Rows[i]["PayerName"].ToString();
                            objPayerRate.ServiceCode = table.Rows[i]["ServiceCode"].ToString();
                            if (table.Rows[i]["Type"] != null && table.Rows[i]["Type"] != DBNull.Value)
                            {
                                objPayerRate.Type = table.Rows[i]["Type"].ToString();
                            }
                            else { objPayerRate.Type = ""; }

                            objPayerRate.BillCode = table.Rows[i]["BillCode"].ToString();
                            objPayerRate.RevenueCode = table.Rows[i]["RevenueCode"].ToString();
                            if (table.Rows[i]["Rate"] != null && table.Rows[i]["Rate"] != DBNull.Value)
                            {
                                objPayerRate.TaxRate = Convert.ToInt32(table.Rows[i]["Rate"]);
                            }
                            else { objPayerRate.TaxRate = 0; }

                            if (table.Rows[i]["ValidFrom"] != null && table.Rows[i]["ValidFrom"] != DBNull.Value)
                            {
                                objPayerRate.ValidFrom = table.Rows[i]["ValidFrom"].ToString();
                            }
                            else { objPayerRate.ValidFrom = ""; }
                            if (table.Rows[i]["ValidTo"] != null && table.Rows[i]["ValidTo"] != DBNull.Value)
                            {
                                objPayerRate.ValidTo = table.Rows[i]["ValidTo"].ToString();
                            }
                            else { objPayerRate.ValidTo = ""; }

                            objPayerRate.Hourly = table.Rows[i]["Hourly"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["Hourly"]);
                            objPayerRate.LiveIn = table.Rows[i]["LiveIn"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["LiveIn"]);
                            objPayerRate.Visit = table.Rows[i]["Visit"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["Visit"]);
                            objPayerRate.Unit = table.Rows[i]["Unit"].ToString();
                            objPayerRate.Modifiers1 = table.Rows[i]["Modifiers1"].ToString();
                            objPayerRate.Modifiers2 = table.Rows[i]["Modifiers2"].ToString();
                            objPayerRate.Modifiers3 = table.Rows[i]["Modifiers3"].ToString();
                            objPayerRate.Modifiers4 = table.Rows[i]["Modifiers4"].ToString();
                            objPayerRate.MutualGroup = table.Rows[i]["MutualGroup"].ToString();
                            objPayerRate.Notes = table.Rows[i]["Notes"].ToString();
                            objPayerRate.IsActive = Convert.ToInt16(table.Rows[i]["IsActive"]);
                            objPayerRate.CreatedBy = table.Rows[i]["CreatedBy"].ToString();
                            objPayerRate.CreatedOn = Convert.ToDateTime(table.Rows[i]["CreatedOn"].ToString());
                            objPayerRate.PlaceOfService = table.Rows[i]["PlaceOfService"].ToString();
                            objPayerRateList.Add(objPayerRate);
                        }

                        obj.Data = objPayerRateList;
                        obj.Result = true;
                        obj.Message = "Payer Rate list retrive successfull.";

                    }
                    else
                    {
                        obj.Result = true;
                        obj.Message = "Payer Rate does not exists.";
                    }
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }


        public async Task<ServiceResponse<PayerRateViewModel>> GetPayerRateDetails(int rateId)
        {
            ServiceResponse<PayerRateViewModel> obj = new ServiceResponse<PayerRateViewModel>();
            PayerRateViewModel objPayerRate = null;
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("GetPayerRateDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RateId", rateId);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        objPayerRate = new PayerRateViewModel();
                        for (int i = 0; i < table.Rows.Count; i++)
                        {

                            objPayerRate.RateId = Convert.ToInt32(table.Rows[i]["RateId"]);
                            objPayerRate.PayerId = table.Rows[i]["PayerId"] == null ? 0 : Convert.ToInt32(table.Rows[i]["PayerId"]);
                            objPayerRate.PayerName = table.Rows[i]["PayerName"].ToString();
                            objPayerRate.ServiceCode = table.Rows[i]["ServiceCode"].ToString();
                            if (table.Rows[i]["Type"] != null && table.Rows[i]["Type"] != DBNull.Value)
                            {
                                objPayerRate.Type = table.Rows[i]["Type"].ToString();
                            }
                            else { objPayerRate.Type = ""; }

                            objPayerRate.BillCode = table.Rows[i]["BillCode"].ToString();
                            objPayerRate.RevenueCode = table.Rows[i]["RevenueCode"].ToString();
                            if (table.Rows[i]["Rate"] != null && table.Rows[i]["Rate"] != DBNull.Value)
                            {
                                objPayerRate.TaxRate = Convert.ToInt32(table.Rows[i]["Rate"]);
                            }
                            else { objPayerRate.TaxRate = 0; }

                            if (table.Rows[i]["ValidFrom"] != null && table.Rows[i]["ValidFrom"] != DBNull.Value)
                            {
                                objPayerRate.ValidFrom = table.Rows[i]["ValidFrom"].ToString();
                            }
                            else { objPayerRate.ValidFrom = ""; }
                            if (table.Rows[i]["ValidTo"] != null && table.Rows[i]["ValidTo"] != DBNull.Value)
                            {
                                objPayerRate.ValidTo = table.Rows[i]["ValidTo"].ToString();
                            }
                            else { objPayerRate.ValidTo = ""; }

                            objPayerRate.Hourly = table.Rows[i]["Hourly"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["Hourly"]);
                            objPayerRate.LiveIn = table.Rows[i]["LiveIn"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["LiveIn"]);
                            objPayerRate.Visit = table.Rows[i]["Visit"] == null ? 0 : Convert.ToDecimal(table.Rows[i]["Visit"]);
                            objPayerRate.Unit = table.Rows[i]["Unit"].ToString();
                            objPayerRate.Modifiers1 = table.Rows[i]["Modifiers1"].ToString();
                            objPayerRate.Modifiers2 = table.Rows[i]["Modifiers2"].ToString();
                            objPayerRate.Modifiers3 = table.Rows[i]["Modifiers3"].ToString();
                            objPayerRate.Modifiers4 = table.Rows[i]["Modifiers4"].ToString();
                            objPayerRate.MutualGroup = table.Rows[i]["MutualGroup"].ToString();
                            objPayerRate.Notes = table.Rows[i]["Notes"].ToString();
                            objPayerRate.IsActive = Convert.ToInt16(table.Rows[i]["IsActive"]);
                            objPayerRate.CreatedBy = table.Rows[i]["CreatedBy"].ToString();
                            objPayerRate.PlaceOfService = table.Rows[i]["PlaceOfService"].ToString();
                            objPayerRate.CreatedOn = Convert.ToDateTime(table.Rows[i]["CreatedOn"].ToString());
                        }

                        obj.Data = objPayerRate;
                        obj.Result = true;
                        obj.Message = "Payer Rate details retrive successfull.";

                    }
                    else
                    {
                        obj.Result = true;
                        obj.Message = "Payer Rate does not exists.";
                    }
                    return obj;
                }
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }
    }
}
