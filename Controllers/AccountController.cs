﻿using ES_HomeCare_API.Model.Account;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

   


        [HttpPost("Login")]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<UserModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {

                model.IsActive = true;
                return Ok(await accountSrv.LoginUser(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }







    }
}
