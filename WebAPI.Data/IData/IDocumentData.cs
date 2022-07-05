using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Document;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IDocumentData
    {
        Task<ServiceResponse<string>> AddFolder(FolderModel model);
        Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int UserId);
        Task<ServiceResponse<string>> DeleteFolder(long FolderId, int UserId);
        Task<ServiceResponse<string>> AddDocument(UploadFileFolder model);
        Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int UserId);
        Task<ServiceResponse<string>> DeleteDocument(long DocumentId);
        Task<ServiceResponse<string>> AddSubFolder(FolderModel model);
        Task<ServiceResponse<IEnumerable<NewFolderView>>> GetTreeDocumentlist(int UserId);


    }
}

