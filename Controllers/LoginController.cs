using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Service.IService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_SAMPLE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _ul;

        public LoginController(ILoginService ul)
        {
            _ul = ul;
        }

        [HttpGet("validateuserlogin/{uname}/{pwd}")]
        [ProducesResponseType(typeof(ServiceResponse<UserLogin>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<UserLogin>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> loginbymobile(string uname, string pwd)
        {
            if (uname.Length < 3 & pwd.Length < 3)
                return BadRequest();
            return Ok(await _ul.ValidateUserLogin(uname, pwd));
        }
    }
}
