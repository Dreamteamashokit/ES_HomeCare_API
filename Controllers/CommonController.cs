using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
   
        private readonly ICommanService comSrv;

        public CommonController( ICommanService _comSrv)
        {
          
            this.comSrv = _comSrv;
        }



        [HttpGet("getMaster/{typeId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEmpMeetingList(short typeId)
        {
            return Ok(await comSrv.GetMasterList(typeId));
        }





    }
}
