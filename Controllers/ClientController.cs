using System;
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
         
            model.TimeSlip = true;
            model.IsHourly = true;
            model.IsActive = 1;
            model.UserName = model.Email;
            model.UserPassword = model.SSN;
            model.CreatedBy = 1;
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddClient(model));
        }


        [HttpGet("getClientDetail/{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<ClientModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<ClientModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientDetail(int userId)
        {
            return Ok(await service.GetClientDetail(userId));
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
