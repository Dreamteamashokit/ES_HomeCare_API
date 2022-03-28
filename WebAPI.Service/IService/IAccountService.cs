using ES_HomeCare_API.Model.Account;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IAccountService
    {
        Task<ServiceResponse<UserModel>> LoginUser(LoginModel model);
    }
}
