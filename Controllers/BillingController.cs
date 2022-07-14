﻿using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
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


        [HttpDelete("delPayer/{PayerId}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<ServiceResponse<string>> DelPayer(int PayerId)
        {
            return await billSrv.DelPayer(PayerId);
        }













    }
}
