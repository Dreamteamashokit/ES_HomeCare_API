using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public EmployeeController(IEmployeeService service)
        {
            this.service = service;
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
    }
}
