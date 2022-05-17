using ES_HomeCare_API.Model.Account;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService accountSrv;

        public AccountController(IAccountService _accountSrv)
        {
            accountSrv = _accountSrv;
        }   


        [HttpPost("logIn")]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            try
            {

                model.IsActive = true;
                return Ok(await accountSrv.LogInUser(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("logOut")]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogOut(int UserId)
        {
            try
            {
          
                return Ok(await accountSrv.LogOutUser(UserId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        [HttpPost("addUser")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser(AccountUserModel model)
        {
            try
            {

                model.IsActive = 1;
                model.UserName = model.Email;
                model.UserPassword = model.SSN;
                model.CreatedOn = DateTime.Now;


                return Ok(await accountSrv.AddUser(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("getUser/{userType}")]
        [ProducesResponseType(typeof(ServiceResponse<List<AccountUserModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<AccountUserModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUser(int userType)
        {
            return Ok(await accountSrv.GetUser(userType));
        }









    }
}
