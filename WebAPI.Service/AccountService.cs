using ES_HomeCare_API.Model.Account;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class AccountService : IAccountService
    {

        private readonly IAccountData data;
        public AccountService(IAccountData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<UserModel>> LogInUser(LoginModel model)
        {
            return await data.LogInUser(model);
        }

        public async Task<ServiceResponse<string>> LogOutUser(int userId)
        {

            return await data.LogOutUser(userId);
        }

        public async Task<ServiceResponse<string>> AddUser(AccountUserModel _model)
        {
            return await data.AddUser(_model);
        }

        public async Task<ServiceResponse<IEnumerable<AccountUserModel>>> GetUser(int userType)
        {
            return await data.GetUser(userType);
        }





    }
}
