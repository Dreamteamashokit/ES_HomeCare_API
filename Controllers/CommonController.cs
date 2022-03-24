using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        


        [HttpPost("UploadFile"), DisableRequestSizeLimit]        
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public IActionResult Upload()
        {
            try
            {
                var files = Request.Form.Files;
              
               

                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }

                foreach (var file in files)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                   //you can add this path to a list and then return all dbPaths to the client if require

                    Stream fs = file.OpenReadStream();
                    AmazonUploader uploader = new AmazonUploader();
                    uploader.sendMyFileToS3(fs, "eshomecarewebapp", fileName);
                }

                return Ok("All the files are successfully uploaded.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
           
        }



    }
}
