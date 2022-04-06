﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService service;

        public ClientController(IClientService service)
        {
            this.service = service;
        }


        [HttpPost("addClient")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClient([FromBody] ClientModel model)
        {
            if (model.Nurse == 0)
            {
                model.Nurse = null;
            }
 
            model.TimeSlip = true;
            model.IsHourly = true;
            model.IsActive = 1;
            model.UserName = model.Email;
            model.UserPassword = model.SSN;

            model.CreatedBy = 1;
            model.CreatedOn = DateTime.Now;



            return Ok(await service.AddClient(model));
        }









        [HttpPost("savenewclientinfo")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> saveuserfeedback([FromBody] Client client)
        {
            return Ok(await service.savenewclient(client));
        }

        [HttpGet("getclientlist")]
        [ProducesResponseType(typeof(ServiceResponse<List<Client>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Client>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getclientlist()
        {
            return Ok(await service.GetClientList());
        }

        [HttpGet("getclientmeetings/{startdate}/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ClientMeetings>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ClientMeetings>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getclientmeetings(string startdate, string clientId)
        {
            return Ok(await service.GetClientMeetings(startdate, clientId));
        }

        [HttpPost("scheduleclientmeeting")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> scheduleclientmeeting([FromBody] MeetingDetails meeting)
        {
            return Ok(await service.scheduleclientmeeting(meeting));
        }

        [HttpPost("addStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveClientStatus([FromBody] ClientStatus model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveClientStatus(model));
        }

        [HttpGet("getClientStatusList/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpStatusList(int clientId)
        {
            return Ok(await service.GetClientStatusList(clientId));
        }
    }
}
