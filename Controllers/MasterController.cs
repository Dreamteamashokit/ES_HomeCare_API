using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterService mstrSrv;
        private IConfiguration configuration;

        public MasterController(IMasterService _mstrSrv, IConfiguration _configuration)
        {
            configuration = _configuration;
            this.mstrSrv = _mstrSrv;
        }


        [HttpPost("createDiagnosis")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDiagnosis([FromBody] DiagnosisItem model)
        {
            try
            {
                model.IsActive = (int)Status.Active;
                model.CreatedOn = DateTime.Now;
                return Ok(await mstrSrv.CreateDiagnosis(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        [HttpGet("getDiagnosis")]
        [ProducesResponseType(typeof(ServiceResponse<List<DiagnosisItem>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<DiagnosisItem>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDiagnosis()
        {
            return Ok(await mstrSrv.GetDiagnosis());
        }


        [HttpPost("updateTask")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTask([FromBody] TaskModel model)
        {
            try
            {
                return Ok(await mstrSrv.UpdateTask(model));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("activeTask")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ActiveTask(int TaskId)
        {
            try
            {
                return Ok(await mstrSrv.ActiveTask(TaskId, (int)Status.InActive));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    
    }
}
