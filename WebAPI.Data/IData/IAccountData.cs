using ES_HomeCare_API.Model.Account;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{

    public interface IAccountData
    {
        Task<ServiceResponse<UserModel>> LoginUser(LoginModel model);
    }
}
