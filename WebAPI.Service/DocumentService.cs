using ES_HomeCare_API.Model.Document;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class DocumentService: IDocumentService
    {

        private readonly IDocumentData data;
        public DocumentService(IDocumentData ldata)
        {
            data = ldata;
        }

       
        public async Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int empId)
        {
            return await data.GetDocumentlist(empId);
        }
    }
}
