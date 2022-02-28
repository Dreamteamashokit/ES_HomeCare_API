using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE.WebAPI.Service
{
    public class LoginService : ILoginService
    {
        private ILoginData data;
        public LoginService(ILoginData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<UserLogin>> ValidateUserLogin(string uname, string password)
        {
            return await data.ValidateUserLogin(uname, password);
        }
    }
}
