using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Master;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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




        #region CMPLCategory

        [HttpPost("addCMPLCategory")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCMPLCategory([FromBody] CategoryModel model)
        {
            model.CreatedOn = DateTime.Now;
            return Ok(await mstrSrv.AddCMPLCategory(model));
        }

        [HttpGet("getCMPLCategoryList")]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCMPLCategoryList()
        {
            return Ok(await mstrSrv.GetCMPLCategoryList());
        }

        [HttpGet("getCMPLCategoryList/{CategoryId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<CategoryModel>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCMPLCategoryList(int CategoryId)
        {
            return Ok(await mstrSrv.GetCMPLCategoryList(CategoryId));
        }


        [HttpDelete("delCMPLCategory")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DelCMPLCategory(int CategoryId)
        {
            return Ok(await mstrSrv.DelCMPLCategory(CategoryId));
        }

        #endregion

    }
}
