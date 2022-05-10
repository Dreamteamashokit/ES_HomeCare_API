using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [ProducesResponseType(typeof(ServiceResponse<List<ClientStatus>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientStatus>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpStatusList(int clientId)
        {
            return Ok(await service.GetClientStatusList(clientId));
        }


        [HttpPost("ClientMedicationcs")]
        [ProducesResponseType(typeof(ServiceResponse<List<Medicationcs>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Medicationcs>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClientMedicationcs(Medicationcs model)
        {


            model.createdOn = DateTime.Now.Date;
            int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Create"));
            return Ok(await service.ClientMedicationcs(model, Flag));

        }

        [HttpGet("GetClientMedicationcs/{CilentId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Medicationcs>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Medicationcs>>), StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
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
                    UserId = model.UserId,
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


        [HttpPost("addClientContactLog")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveClientContactLog([FromBody] ClientContactLog model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveClientContactLog(model));
        }

        [HttpGet("GetClientContactLogs/{clientId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientContactLog>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientContactLog>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientContactLogs(int clientId)
        {
            return Ok(await service.GetClientContactLogs(clientId));
        }


        [HttpGet("getClientContactLogDetails/{contactlogId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientContactLog>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientContactLog>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse<ClientContactLog>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<ClientContactLog>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getClientContactLogDetails(int contactlogId)
        {
            return Ok(await service.getClientContactLogDetails(contactlogId));
        }

        [HttpPost("updateClientContactLog")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> updateClientContactLog([FromBody] ClientContactLog model)
        {
            try
            {
                return Ok(await service.UpdateClientContactLog(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("deleteClientContactLog")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> deleteClientContactLog(int contactLogId)
        {
            try
            {

                return Ok(await service.DeleteClientContactLog(contactLogId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("ClientEmergencyInfo")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClientEmergencyInfo(ClientEmrgencyInfo model)
        {

            model.CreatedOn = DateTime.Now.Date;
            return Ok(await service.ClienEmergencyInfo(model));

        }


        [HttpGet("getClientEmergencyInfo/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getClientEmergencyInfo(int UserId)
        {

            ClientEmrgencyInfo model = new ClientEmrgencyInfo();
            model.UserId = UserId;
            model.CreatedOn = DateTime.Now.Date;

            return Ok(await service.ClienEmergencyInfo(model));



        }






        [HttpPost("addOtherInfo")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOtherInfo([FromBody] OtherInfoModel model)
        {
            try
            {

                model.CreatedOn = DateTime.Now.Date;
                return Ok(await service.AddOtherInfo(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("updateOtherInfo")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOtherInfo([FromBody] OtherInfoModel model)
        {
            try
            {
                return Ok(await service.UpdateOtherInfo(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpGet("getOtherInfo/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<OtherInfoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<OtherInfoModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOtherInfo(int UserId)
        {
            return Ok(await service.GetOtherInfo(UserId));

        }


        [HttpPost("addDiagnosis")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDiagnosis([FromBody] DiagnosisModel model)
        {
            try
            {

                model.CreatedOn = DateTime.Now;


                return Ok(await service.AddDiagnosis(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("updateDiagnosis")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDiagnosis([FromBody] DiagnosisModel model)
        {
            try
            {
                model.CreatedOn = DateTime.Now;
                return Ok(await service.UpdateDiagnosis(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete("deleteDiagnosis")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDiagnosis(int diagnosisId)
        {
            try
            {
                return Ok(await service.DeleteDiagnosis(diagnosisId));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("AddClientNote")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClientNotes(ClientNote model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Create"));
                return Ok(await service.ClientNoteOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("UpdateClientNote")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClientNotes(ClientNote model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Modify"));
                return Ok(await service.ClientNoteOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("DeleteClientNote")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClientNotes(ClientNote model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Delete"));
                return Ok(await service.ClientNoteOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GetClientNoteList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientNoteList(ClientNote model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "select"));
                return Ok(await service.ClientNoteOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GetClientNote")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientNote>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientNote(ClientNote model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = 5;
                return Ok(await service.ClientNoteOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        [HttpGet("getDiagnosisModel/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<DiagnosisView>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<DiagnosisView>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDiagnosisModel(int UserId)
        {
            return Ok(await service.GetDiagnosisModel(UserId));

        }


        [HttpPost("AddClientCommunity")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCommunityMaster>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCommunityMaster>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ClientCommunity(ClientCommunityMaster model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Create"));
                return Ok(await service.ClientCommunityOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost("GetClientCommunityList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCommunityMaster>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCommunityMaster>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientCommunityList(ClientCommunityMaster model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "select"));
                return Ok(await service.ClientCommunityOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("ProvisionInfo/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ProvisionInfo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ProvisionInfo>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ProvisionInfo(int UserId)
        {

            return Ok(await service.ClienProvisionInfo(null, UserId));
        }

        [HttpPost("SaveProvisionInfo")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientEmrgencyInfo>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveProvisionInfo(IEnumerable<ProvisionInfo> model)
        {

            return Ok(await service.ClienProvisionInfo(model, 0));
        }

        [HttpPost("AddClientCompliance")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClientCompliance(ClientCompliance model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Create"));
                return Ok(await service.ClientComplianceOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("UpdateClientCompliance")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClientCompliance(ClientCompliance model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Modify"));
                return Ok(await service.ClientComplianceOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("DeleteClientCompliance")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteClientCompliance(ClientCompliance model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "Delete"));
                return Ok(await service.ClientComplianceOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GetClientComplianceList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientComplianceList(ClientCompliance model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = (int)((SqlQueryType)Enum.Parse(typeof(SqlQueryType), "select"));
                return Ok(await service.ClientComplianceOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("GetClientCompliance")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientCompliance>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientCompliance(ClientCompliance model)
        {
            try
            {
                model.CreatedOn = DateTime.Now.Date;
                int Flag = 5;
                return Ok(await service.ClientComplianceOperation(model, Flag));
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}

