﻿using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Document;
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
    public class DocumentController : ControllerBase
    {

        private readonly IDocumentService docSrv;
        private IConfiguration configuration;

        public DocumentController(IDocumentService _docSrv, IConfiguration _configuration)
        {
            configuration = _configuration;
            this.docSrv = _docSrv;

        }



        [HttpGet("getDocumentlist/{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDocumentlist(int empId)
        {
            return Ok(await docSrv.GetDocumentlist(empId));
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

                return Ok(await docSrv.Savefile(model));

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
            return Ok(await docSrv.SaveFolder(model));
        }


        [HttpGet("getFolderlist/{EmpId}")]
        [ProducesResponseType(typeof(ServiceResponse<List<ItemList>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<List<UploadFileRecord>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFolderlist(int EmpId)
        {
            return Ok(await docSrv.GetFolderlist(EmpId));
        }

        [HttpGet("download/{documentName}")]

        public IActionResult GetDocumentFromS3(string documentName, string foldername)
        {
            try
            {
                documentName = foldername + "/" + documentName;

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
            sres.Message = ex.Message;
            return sres;
        }

        [HttpDelete("DeletetDocumentFromS3")]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletetDocumentFromS3(int DocumentId, string FileName, int FolderId, string FolderName, int empid)
        {
            //string DeleteFilename = string.IsNullOrEmpty(FileName) ? FolderName + "/" : FolderName + "/" + FileName;
            //AmazonUploader Download = new AmazonUploader(configuration);
            //Download.DeleteFile(DeleteFilename);
            DeleteItem obj = new DeleteItem();
            obj.DocumentId = DocumentId;
            obj.FileName = FileName;
            obj.FolderName = FolderName;
            obj.FolderId = FolderId;
            obj.EmpId = empid;
            return Ok(await docSrv.DeleteFile(obj));
        }



    }
}