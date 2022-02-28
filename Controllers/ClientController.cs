using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Model;
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
    }
}
