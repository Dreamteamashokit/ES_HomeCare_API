using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using ES_HomeCare_API.WebAPI.Service;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;

        private readonly ICommanService _comService;

        public EmployeeController(IEmployeeService service, ICommanService Sev)
        {
            this.service = service;
            this._comService = Sev;
        }
        #region Employee

        [HttpPost("addEmployee")]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<int>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel model)
        {



            if (model.SupervisorId == 0)
            {
                model.SupervisorId = null;
            }

            model.DateOfHireS = model.DateOfHire.ParseDate();
            model.DOBS = model.DOB.ParseDate();
            if (!string.IsNullOrEmpty(model.DateOfFirstCase))
            {
                model.DateOfFirstCaseS = model.DateOfFirstCase.ParseDate();

            }
            else
            {
                model.DateOfFirstCaseS = null;
            }

            model.UserKey = model.EmpKey;
            model.IsActive = (int)Status.Active;
            model.CreatedBy = 1;
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddEmployee(model));
        }


        [HttpGet("getEmployeeListObj/{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeListObj(int userId)
        {
            return Ok(await service.GetEmployeeListObj(userId));
        }

        [HttpPost("getEmployeeListObj")]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<EmployeeList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeListObj([FromBody] ClientFilter model)
        {
            return Ok(await service.GetEmployeeListObj(model));
        }

        


        [HttpGet("deleteEmployee/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> deleteemployee(int empId)
        {
            return Ok(await service.DeleteEmployee(empId));
        }

        [HttpGet("getEmployeebyId/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<EmployeeJson>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<EmployeeJson>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployeeById(int empId)
        {
            return Ok(await service.GetEmployeeById(empId));
        }

        #endregion

        #region Incident
        [HttpPost("addIncident")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddIncident([FromBody] IncidentModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddIncident(model));
        }

        [HttpGet("getIncidentList/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<IncidentModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<IncidentModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIncidentList(int empId)
        {
            return Ok(await service.GetIncidentList(empId));
        }

        [HttpDelete("delIncident")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelIncident(int IncidentId)
        {
            return Ok(await service.DelIncident(IncidentId));
        }
        #endregion

        #region Attendance
        [HttpPost("addAttendance")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAttendance([FromBody] AttendanceModel model)
        {


            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddAttendance(model));
        }

        [HttpGet("getAttendanceList/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<AttendanceModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<AttendanceModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceList(int empId)
        {
            return Ok(await service.GetAttendanceList(empId));
        }


        [HttpDelete("delAttendance")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelAttendance(int AttendanceId)
        {
            return Ok(await service.DelAttendance(AttendanceId));
        }



        #endregion

        [HttpGet("getAvailabilityList")]
        [ProducesResponseType(typeof(ServiceResponse<List<AvailabilityMaster>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<AvailabilityMaster>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailabilityList()
        {
            return Ok(await service.GetAvailabilityList());
        }

        [HttpPost("addStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddEmpStatus([FromBody] StatusModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddEmpStatus(model));
        }


        [HttpGet("getEmpStatusList/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<StatusModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<StatusModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpStatusList(int empId)
        {
            return Ok(await service.GetEmpStatusList(empId));
        }


        [HttpDelete("delEmpStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelEmpStatus(int StatusId)
        {
            return Ok(await service.DelEmpStatus(StatusId));
        }


        #region Compliance
        [HttpPost("addCompliance")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCompliance([FromBody] ComplianceModel model)
        {


            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddCompliance(model));
        }

        [HttpGet("getComplianceList/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ComplianceModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ComplianceModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComplianceList(int empId)
        {
            return Ok(await service.GetComplianceList(empId));
        }
        #endregion






        #region Address
        [HttpPost("addAddress")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAddress([FromBody] AddressModel model)
        {

            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddEmpAddress(model));
        }

        [HttpGet("getAddress/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<AddressModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<AddressModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAddress(int empId)
        {
            return Ok(await service.GetEmpAddress(empId));
        }

        #endregion





        #region Rate
        [HttpPost("addRate")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveEmpRate([FromBody] EmployeeRateModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveEmpPayRate(model));
        }


        [HttpGet("getEmpRate/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<EmpRate>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<EmpRate>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpRate(long empId)
        {

            return Ok(await service.GetEmpPayRate(empId, 0));
        }

        #endregion

        #region Declined Case




        [HttpPost("addEmpDeclinedCase")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDeclinedCase([FromBody] EmpDeclinedCase model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddDeclinedCase(model));
        } 


        [HttpDelete("delDeclinedCase")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelDeclinedCase(int DeclinedCaseId)
        {
            return Ok(await service.DelDeclinedCase(DeclinedCaseId));
        }


        [HttpGet("getEmpDeclinedcase/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<EmpDeclinedCase>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<EmpDeclinedCase>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDeclinedCaseList(int empId)
        {

            return Ok(await service.GetDeclinedCaseList(empId));
        }

        #endregion




        #region Caregiver 
        [HttpGet("GetCareGiverDetails/{UserId}")]
        [ProducesResponseType(typeof(ServiceResponse<CaregiverViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<CaregiverViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCareGiverDetails(int UserId)
        {
            return Ok(await service.GetCareGiverDetails(UserId));
        }
        #endregion

        #region External login
        [HttpPost("ExternalLogin")]
        [ProducesResponseType(typeof(ServiceResponse<ExternalLoginViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<ExternalLoginViewModel>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExternalLogin(ExternalLoginModel externalLoginModel)
        {
            return Ok(await service.ExternalLogin(externalLoginModel));
        }
        #endregion

        #region Get HHA Customer/Patients details
        [HttpGet("GetClientListByempId/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientListViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientListViewModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClientListByempId(int empId)
        {
            return Ok(await service.GetClientListByempId(empId));
        }
        #endregion

        #region Category
        [HttpPost("addCategory")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] CategoryModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddCategory(model));
        }

        [HttpGet("getCategoryList")]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryList()
        {
            return Ok(await service.GetCategoryList());
        }

        [HttpGet("getSubCategoryList/{categoryId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSubCategoryList(int categoryId)
        {
            return Ok(await service.GetSubCategoryList(categoryId));
        }
        #endregion


        [HttpGet("GetClockinDetails/{userId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientListViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ClientListViewModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetClockinDetails(int userId)
        {
            return Ok(await service.GetClockinDetails(userId));
        }

        [HttpPost("HHAClockin")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HHAClockin([FromBody] HHAClockInModel hhaClockinModel)
        {
            return Ok(await service.HHAClockin(hhaClockinModel));
        }
    }
}
