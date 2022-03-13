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

        private readonly ICommanService CommanService;
        
        public EmployeeController(IEmployeeService service, ICommanService Sev)
        {
            this.service = service;
            this.CommanService = Sev;
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
            return Ok(await CommanService.GetOfficeUserLst());
        }

        [HttpGet("getTypeStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTypeStatusLst()
        {
            return Ok(await CommanService.GetTypeStatusLst());
        }

        [HttpGet("getEmployeeStatusList")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStatusEmployeeLst()
        {
            return Ok(await CommanService.GetEmployeeLst());
        }

        [HttpGet("getScheduleLst")]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<Employee>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetScheduleLst()
        {
            return Ok(await CommanService.GetScheduleLst());
        }


    }
}
