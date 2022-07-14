using ES_HomeCare_API.Model.Billing;
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

    }
}
