using ES_HomeCare_API.Model.Account;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class AccountService: IAccountService
    {

        private readonly IAccountData data;
        public AccountService(IAccountData ldata)
        {
            data = ldata;
        }
        public async Task<ServiceResponse<UserModel>> LoginUser(LoginModel model)
        {
            return await data.LoginUser(model);
        }

        
       
    }
}
