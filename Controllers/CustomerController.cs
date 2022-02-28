using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService service)
        {
            this.service = service;
        }

        [HttpGet("getcustomerlist")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getclientlist()
        {
            return Ok(await service.GetCustomerList());
        }

        [HttpGet("getcustomerById/{CId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getcustomerById(string CId)
        {
            return Ok(await service.GetCustomerbyId(CId));
        }
    }
}
