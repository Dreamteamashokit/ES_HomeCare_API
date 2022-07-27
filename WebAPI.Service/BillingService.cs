using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Billing;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class BillingService : IBillingService
    {

        private readonly IBillingData data;
        public BillingService(IBillingData ldata)
        {
            data = ldata;
        }
        public async Task<ServiceResponse<string>> AddPayer(PayerModel _model)
        {
            return await data.AddPayer(_model);
        }

        public async Task<ServiceResponse<string>> UpdatePayer(PayerModel _model)
        {
            return await data.UpdatePayer(_model);
        }
        public async Task<ServiceResponse<IEnumerable<PayerModel>>> GetPayerList()
        {
            return await data.GetPayerList();
        }

        public async Task<ServiceResponse<BillingSummaryInfoModel>> GetBillingSummaryInfo(int userId)
        {
            return await data.GetBillingSummaryInfo(userId);
        }

        public async Task<ServiceResponse<string>> DelPayer(int PayerId)
        {
            return await data.DelPayer(PayerId);
        }

        public async Task<ServiceResponse<IEnumerable<BillingStatusViewModel>>> GetBillingStatusList()
        {
            return await data.GetBillingStatusList();
        }

        public async Task<ServiceResponse<IEnumerable<PayrollStatusViewModel>>> GetPayrollStatusList()
        {
            return await data.GetPayrollStatusList();
        }



        public async Task<ServiceResponse<IEnumerable<ClientSchedule>>> GetScheduleBilling()
        {
            return await data.GetScheduleBilling();
        }

        public async Task<ServiceResponse<IEnumerable<ClientSchedule>>> GetScheduleBilling(SearchSchedule model)
        {
            return await data.GetScheduleBilling(model);
        }

        public async Task<ServiceResponse<BillingPayerRateViewModel>> GetBillingPayerRate(long payerId, long clientId, long meetingId)
        {
            return await data.GetBillingPayerRate(payerId,clientId,meetingId);
        }





        public async Task<ServiceResponse<int>> UpdateSchedule(UpdateBillingSchedule model)
        {
            return await data.UpdateSchedule(model);
        }
        public async Task<ServiceResponse<int>> CreateInvoice(InvoiceModel model)
        {
            return await data.CreateInvoice(model);
        }


        public async Task<ServiceResponse<IEnumerable<InvoiceView>>> GetScheduleInvoice()
        {
            return await data.GetScheduleInvoice();
        }


    }
}
