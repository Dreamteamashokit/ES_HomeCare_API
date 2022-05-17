using ES_HomeCare_API.Model.Account;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{

    public interface IAccountData
    {
        Task<ServiceResponse<UserModel>> LogInUser(LoginModel model);
        Task<ServiceResponse<string>> LogOutUser(int userId);
        Task<ServiceResponse<string>> AddUser(AccountUserModel _model);
        Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType);
    }
}
