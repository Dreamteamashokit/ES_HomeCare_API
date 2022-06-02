using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService service;

        public InvoiceController(IInvoiceService service)
        {
            this.service = service;
        }

        [HttpGet("getinvoicelist")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getinvoicelist()
        {
            return Ok(await service.GetInvoiceList());
        }

        [HttpGet("getinvoicebyid/{InvId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getinvoicebyid(string InvId)
        {
            return Ok(await service.GetInvoicebyId(InvId));
        }

        [HttpPost("generateinvoice")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> generateinvoice(GenerateInvoice invoice)
        {
            return Ok(await service.GenerateInvoice(invoice));
        }

        [HttpPost("payinvoice/{InvId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> payinvoice(string InvId)
        {
            return Ok(await service.PayInvoice(InvId));
        }

        [HttpPost("GetAllActivePayers")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllActivePayers()
        {
            return Ok(await service.GetAllActivePayers());
        }


    }
}
