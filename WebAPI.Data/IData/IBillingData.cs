using ES_HomeCare_API.Model.Billing;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IBillingData
    {
        Task<ServiceResponse<string>> AddPayer(PayerModel _model);

        Task<ServiceResponse<BillingSummaryInfoModel>> GetBillingSummaryInfo(int userId);
    }
}
