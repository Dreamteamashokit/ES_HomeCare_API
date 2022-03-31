using ES_HomeCare_API.Model.Document;
using ES_HomeCare_API.WebAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {

        private readonly IDocumentService docSrv;
      

        public DocumentController(IDocumentService _docSrv)
        {
            this.docSrv = _docSrv;
      
        }


        [HttpGet("getDocumentlist{empId}")]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse<FolderView>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDocumentlist(int empId)
        {
            return Ok(await docSrv.GetDocumentlist(empId));
        }

    }
}
