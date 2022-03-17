using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ES_HomeCare_API.Model.Employee;
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


        [HttpPost("savenewemployeeinfo")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> savenewemployeeinfo([FromBody] Employee employee)
        {
            return Ok(await service.savenewemployee(employee));
        }

        [HttpGet("getemployeelist")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getemployeelist()
        {
            return Ok(await service.GetEmployeesList());
        }

        [HttpGet("deleteemployee/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> deleteemployee(int empId)
        {
            return Ok(await service.DeleteEmployee(empId));
        }

        [HttpGet("getemployeebyId/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<Employee>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<Employee>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getemployeebyId(string empId)
        {
            return Ok(await service.GetEmployeeById(empId));
        }

        #region Incident
        [HttpPost("addIncident")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddIncident([FromBody] IncidentMode model)
        {


            model.CreatedOn = DateTime.Now;
            return Ok(await service.AddIncident(model));
        }

        [HttpGet("getIncidentList/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetIncidentList(int empId)
        {
            return Ok(await service.GetIncidentList(empId));
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
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttendanceList(int empId)
        {
            return Ok(await service.GetAttendanceList(empId));
        }
        #endregion






        [HttpGet("getAvailabilityList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailabilityList()
        {
            return Ok(await service.GetAvailabilityList());
        }




        [HttpPost("addStatus")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveExitEmpStatus([FromBody] StatusModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveExitEmpStatus(model));
        }


        [HttpGet("getEmpStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpStatusList()
        {
            return Ok(await service.GetEmpStatusList());
        }

        [HttpGet("getOfficeUserList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOfficeUserLst()
        {
            return Ok(await _comService.GetOfficeUserLst());
        }

        [HttpGet("getTypeStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTypeStatusLst()
        {
            return Ok(await _comService.GetTypeStatusLst());
        }

        [HttpGet("getEmployeeStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStatusEmployeeLst()
        {
            return Ok(await _comService.GetEmployeeLst());
        }

        [HttpGet("getScheduleLst")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleLst()
        {
            return Ok(await _comService.GetScheduleLst());
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
            model.CreatedBy = 1;
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
        public async Task<IActionResult> SaveEmpRate([FromBody] SaveEmployeeRate model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveEmpPayRate(model));
        }


        [HttpGet("getEmpRate/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpRate(long empId)
        {

            return Ok(await service.GetEmpPayRate(empId, 0));
        }

        #endregion

        #region Declined Case

        [HttpPost("addEmpDeclinedCase")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveEmpDeclinedCase([FromBody] EmpDeclinedCase model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await service.SaveEmpDeclinedCase(model));
        }


        [HttpGet("getEmpDeclinedcase/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<EmpDeclinedCase>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<EmpDeclinedCase>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpDeclinedcase(int empId)
        {

            return Ok(await service.GetEmpDeclinedcase(empId));
        }

        #endregion





    }
}
