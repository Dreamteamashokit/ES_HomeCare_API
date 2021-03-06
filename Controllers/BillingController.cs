using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
using ES_HomeCare_API.ViewModel.Billing;
using ES_HomeCare_API.ViewModel.Invoice;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService billSrv;
        private IConfiguration configuration;

        public BillingController(IBillingService _billSrv, IConfiguration _configuration)
        {
            configuration = _configuration;
            this.billSrv = _billSrv;
        }



        [HttpPost("addPayer")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPayer([FromBody] PayerModel model)
        {
            try
            {

                model.IsActive = (int)Status.Active;
                model.CreatedOn = DateTime.Now;
                return Ok(await billSrv.AddPayer(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("updatePayer")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePayer([FromBody] PayerModel model)
        {
            try
            {

                model.IsActive = (int)Status.Active;
                model.CreatedOn = DateTime.Now;
                return Ok(await billSrv.UpdatePayer(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("getPayerList")]
        [ProducesResponseType(typeof(ServiceResponse<List<PayerModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<PayerModel>>), StatusCodes.Status400BadRequest)]
        public async Task<ServiceResponse<IEnumerable<PayerModel>>> GetPayerList()
        {
            return await billSrv.GetPayerList();
        }


        [HttpDelete("delPayer")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<ServiceResponse<string>> DelPayer(int PayerId)
        {
            return await billSrv.DelPayer(PayerId);
        }


        [HttpGet("GetBillingSummaryInfo/{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBillingSummaryInfo(int userId)
        {
            try
            {
                return Ok(await billSrv.GetBillingSummaryInfo(userId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("GetBillingStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBillingStatusList()
        {
            try
            {
                return Ok(await billSrv.GetBillingStatusList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetPayrollStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPayrollStatusList()
        {
            try
            {
                return Ok(await billSrv.GetPayrollStatusList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getAllScheduleBilling")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientSchedule>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientSchedule>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllScheduleBilling()
        {
            try
            {
                return Ok(await billSrv.GetScheduleBilling());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("getScheduleBilling")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientSchedule>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientSchedule>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleBilling([FromBody] SearchSchedule model)
        {
            try
            {
                return Ok(await billSrv.GetScheduleBilling(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("GetBillingPayerRate/{payerId}/{clientId}/{meetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<BillingPayerRateViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<BillingPayerRateViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBillingPayerRate(long payerId, long clientId, long meetingId)
        {
            try
            {
                return Ok(await billSrv.GetBillingPayerRate(payerId, clientId, meetingId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("updateSchedule")]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSchedule(UpdateBillingSchedule model)
        {
            try
            {
                return Ok(await billSrv.UpdateSchedule(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("createInvoice")]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice(InvoiceModel model)
        {
            try
            {
                model.InvoiceStatus = (short)BillingStatus.Invoiced;
                model.CreatedOn = DateTime.Now;
                return Ok(await billSrv.CreateInvoice(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("getAllScheduleInvoice")]
        [ProducesResponseType(typeof(ServiceResponse<List<InvoiceView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<InvoiceView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllScheduleInvoice()
        {
            try
            {
                return Ok(await billSrv.GetScheduleInvoice());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost("getScheduleInvoice")]
        [ProducesResponseType(typeof(ServiceResponse<List<InvoiceView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<InvoiceView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleInvoice([FromBody] SearchInvoice model)
        {
            try
            {
                return Ok(await billSrv.GetScheduleInvoice(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetPayerListByclientIdAndmeetingId/{clientId}/{meetingId}")]
        [ProducesResponseType(typeof(ServiceResponse<IList<PayerListViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IList<PayerListViewModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPayerListByclientIdAndmeetingId(long clientId, long meetingId)
        {
            try
            {
                return Ok(await billSrv.GetPayerListByclientIdAndmeetingId(clientId, meetingId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("getInvoiceById/{InvoiceId}")]
        [ProducesResponseType(typeof(ServiceResponse<InvoiceView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<InvoiceView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInvoiceById(long InvoiceId)
        {
            try
            {
                return Ok(await billSrv.GetInvoiceById(InvoiceId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }















    }
}
