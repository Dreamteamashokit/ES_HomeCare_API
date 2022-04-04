using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommanService comSrv;
        private IConfiguration configuration;

        public CommonController(ICommanService _comSrv, IConfiguration _configuration)
        {
            configuration = _configuration;
            this.comSrv = _comSrv;
        }







        [HttpGet("getMaster/{typeId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpMeetingList(short typeId)
        {
            return Ok(await comSrv.GetMasterList(typeId));
        }

        [HttpPost("addMasterType")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMasterType(string  Name)
        {
            try
            {
                return Ok(await comSrv.CreateMasterType(Name));
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("addMaster")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMaster([FromBody] ItemObj model)
        {
            try
            {
                return Ok(await comSrv.CreateMaster(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("getMasterType")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMasterTypeList()
        {
            return Ok(await comSrv.GetMasterTypeList());
        }


        [HttpGet("getSystemMaster")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemObj>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemObj>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSystemMaster()
        {
            return Ok(await comSrv.GetSystemMaster());
        }


        [HttpGet("getEmpTypeList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpTypeList()
        {
            return Ok(await comSrv.GetEmpTypeList());
        }   
      

        [HttpGet("getCountry")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getCountryList()       
        {
            return Ok(await comSrv.GetCountry());
        }        
        
        
        [HttpGet("getStateList/{country}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getStateList(string country)
        {
            return Ok(await comSrv.GetState(country));
         
        }

        [HttpGet("getEmployees/{type}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmployees(string type)
        {
            return Ok(await comSrv.GetEmployees(type));
        }


        [HttpGet("getEmpList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getEmpSelectList()
        {
            return Ok(await comSrv.GetEmployeesList());
        }

        [HttpGet("getClientList")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getClientSelectList()
        {
            return Ok(await comSrv.GetClientList());
        }
      











   


      


    }
}
