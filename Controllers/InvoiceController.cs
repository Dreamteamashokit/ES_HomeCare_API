using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
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

        [HttpPost("AddUpdatePayerRate")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUpdatePayerRate(PayerRateModel payerRateModel)
        {
            return Ok(await service.AddUpdatePayerRate(payerRateModel));
        }


        [HttpPost("AddUpdateBilling")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUpdateBilling(BillingModel billingModel)
        {
            return Ok(await service.AddUpdateBilling(billingModel));
        }


        [HttpDelete("DeleteBillng")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBillng(long billingId)
        {
            return Ok(await service.DeleteBillng(billingId));
        }


        [HttpPost("GetBillingDetailsByBillingId/{billingId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBillingDetailsByBillingId(long billingId)
        {
            return Ok(await service.GetBillingDetailsByBillingId(billingId));
        }


        [HttpPost("GetActiveBillAndExpiredBill/{status}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetActiveBillAndExpiredBill(bool status)
        {
            return Ok(await service.GetActiveBillAndExpiredBill(status));
        }


        [HttpPost("GetServiceCodeByPayerId/{payerId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetServiceCodeByPayerId(long payerId)
        {
            return Ok(await service.GetServiceCodeByPayerId(payerId));
        }


        [HttpGet("GetPayerRateList")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPayerRateList()
        {
            return Ok(await service.GetPayerRateList());
        }


        [HttpPost("GetPayerRateDetails/{rateId}")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPayerRateDetails(int rateId)
        {
            return Ok(await service.GetPayerRateDetails(rateId));
        }

        [HttpDelete("DeleteRate")]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRate(int rateId)
        {
            return Ok(await service.DeleteRate(rateId));
        }
    }
}
