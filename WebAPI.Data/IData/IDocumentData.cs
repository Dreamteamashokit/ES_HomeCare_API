using ES_HomeCare_API.Model.Document;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IDocumentData
    {

        Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int empId);
    }
}
