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
        
        [HttpPost("UploadFile"), DisableRequestSizeLimit]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile()
        {
            try
            {
                var files = Request.Form.Files;

                UploadFileFolder model = new UploadFileFolder();
                model.folderid = Convert.ToInt32(Request.Form["folderid"]);
                model.Title = Request.Form["Title"].ToString();
                model.CreatedBy = Convert.ToInt32(Request.Form["CreatedBy"]);
                model.Search = Request.Form["Search"].ToString();
                model.Description = Request.Form["Description"].ToString();
                string Foldername = Request.Form["Foldername"].ToString();


                if (files.Any(f => f.Length == 0))
                {
                    return BadRequest();
                }

                foreach (var file in files)
                {

                    var fileName = Foldername + "/" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //you can add this path to a list and then return all dbPaths to the client if require
                    model.filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    Stream fs = file.OpenReadStream();
                    AmazonUploader uploader = new AmazonUploader(configuration);
                    uploader.sendMyFileToS3(fs, fileName);

                }

                return Ok(await comSrv.Savefile(model));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }


        [HttpPost("SaveFolder")]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveFolder([FromBody] FolderData model)
        {
            return Ok(await comSrv.SaveFolder(model));
        }


        [HttpGet("getFolderlist/{EmpId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<UploadFileRecord>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFolderlist(int EmpId)
        {
            return Ok(await comSrv.GetFolderlist(EmpId));
        }

        [HttpGet("download/{documentName}")]
        
        public IActionResult GetDocumentFromS3(string documentName,string foldername)
        {
            try
            {
                documentName = foldername+"/"+ documentName;

                AmazonUploader Download = new AmazonUploader(configuration);
                var document = Download.DownloadFileAsync(documentName).Result;

                return File(document, "application/octet-stream", documentName);
            }
            catch (Exception ex)
            {
                return Ok(null);
            }
          
        }

        private ServiceResponse<string> ValidateException(Exception ex)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            sres.Data = ex.Data.ToString();
            sres.Message =ex.Message;
            return sres;
        }












   


      


    }
}
