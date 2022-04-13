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
                model.createdOn = DateTime.Now.Date;
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
            model.createdOn = DateTime.Now.Date;
            model.ClientID = CilentId;
            int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "select"));
            return Ok(await service.ClientMedicationcs(model, Flag));
        }



        [HttpDelete("deleteMedicationData")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMedicationData(int MedicationId, int UserId)
        {
            Medicationcs model = new Medicationcs();
            model.MedicationID = MedicationId;
            model.ClientID = UserId;
            model.createdOn = DateTime.Now.Date;
            int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Delete"));
            return Ok(await service.ClientMedicationcs(model, Flag));
        }


        [HttpPost("createServiceTask")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateServiceTask([FromBody] IList<ServiceTaskModel> model)
        {
            try
            {

                foreach (var item in model)
                {



                    item.IsActive = 1;
                    item.CreatedBy = 1;
                    item.CreatedOn = DateTime.Now;
                }



                return Ok(await service.CreateServiceTask(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getServiceTaskList/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ServiceTaskView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ServiceTaskView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetServiceTaskList(int UserId)
        {
            return Ok(await service.GetServiceTaskList(UserId));
        }

        [HttpPost("updateService")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateService([FromBody] ServiceTaskModel model)
        {
            try
            {

                return Ok(await service.UpdateService(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete("deleteService")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteService(int TaskSrvId)
        {
            try
            {

                return Ok(await service.DeleteService(TaskSrvId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        [HttpPost("createEmpDeclined")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmpDeclined([FromBody] EmployeeDeclineJson model)
        {
            try
            {
                EmployeeDecline obj = new EmployeeDecline()
                {
                    ReportedDate = model.ReportedDate.ParseDateTime(),
                    EmpId = model.EmpId,
                    CaseType = model.CaseType,
                    Reason = model.Reason,
                    StartDate = model.StartDate.ParseDate(),
                    Notes = model.Notes,
                    UserId= model.UserId,
                    CreatedBy = model.CreatedBy,

                };

                obj.IsActive = 1;
                obj.CreatedOn = DateTime.Now;

                return Ok(await service.CreateEmpDeclined(obj));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("getEmpDeclined/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeDeclineView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeDeclineView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpDeclined(int UserId)
        {
            return Ok(await service.GetEmpDeclined(UserId));
        }



        [HttpPost("updateEmpDeclined")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateEmpDeclined([FromBody] EmployeeDecline model)
        {
            try
            {

                return Ok(await service.UpdateEmpDeclined(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("deleteEmpDeclined")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteEmpDeclined(int declinedId)
        {
            try
            {

                return Ok(await service.DeleteEmpDeclined(declinedId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






















    }
}
