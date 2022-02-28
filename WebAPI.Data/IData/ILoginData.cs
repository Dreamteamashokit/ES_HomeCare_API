using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Data.IData
{
    public interface ILoginData
    {
        Task<ServiceResponse<UserLogin>> ValidateUserLogin(string uname, string password);
    }
}
