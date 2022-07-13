using ES_HomeCare_API.Model.Billing;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IBillingService
    {
        Task<ServiceResponse<string>> AddPayer(PayerModel _model);
    }
}
