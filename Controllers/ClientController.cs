using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Helper;
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


        [HttpPost("ClientMedicationcs")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClientMedicationcs(Medicationcs model)
        {
            try
            {
                model.createdOn = DateTime.Now.ToString("dd-mm-yyyy");
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Create"));
                return Ok(await service.ClientMedicationcs(model, Flag));
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        [HttpGet("GetClientMedicationcs/{CilentId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientMedicationcs(int CilentId)
        {
            Medicationcs model = new Medicationcs();
            model.ClientID = CilentId;      
            int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "select"));
            return Ok(await service.ClientMedicationcs(model,Flag));
        }


        [HttpPost("createServiceTask")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateServiceTask([FromBody] IList<ServiceTaskModel> model)
        {
            try
            {
                return Ok(await service.CreateServiceTask(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getServiceTaskList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ServiceTaskView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ServiceTaskView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetServiceTaskList(int UserId)
        {
            return Ok(await service.GetServiceTaskList(UserId));
        }

        [HttpDelete("deleteMedicationData")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMedicationData(int MedicationId,int UserId)
        {
            Medicationcs model = new Medicationcs();
            model.MedicationID = MedicationId;
            model.ClientID = UserId;
            int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Delete"));
            return Ok(await service.ClientMedicationcs(model, Flag));
        }











    }
}
