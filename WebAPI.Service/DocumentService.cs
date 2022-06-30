using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Document;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class DocumentService : IDocumentService
    {

        private readonly IDocumentData data;
        public DocumentService(IDocumentData ldata)
        {
            data = ldata;
        }


        public async Task<ServiceResponse<string>> AddFolder(FolderModel model)
        {
            ServiceResponse<string> rObj=null;
            if (model.ParentFolderId > 0)
            {
                rObj = await data.AddSubFolder(model);
            }
            else
            {
                rObj = await data.AddFolder(model);
            }
            
            return rObj;

        }
        public async Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int UserId)
        {
            return await data.GetFolderlist(UserId);
        }

        public async Task<ServiceResponse<string>> DeleteFolder(long FolderId, int UserId)
        {

            return await data.DeleteFolder(FolderId, UserId);

        }
        public async Task<ServiceResponse<string>> AddDocument(UploadFileFolder model)
        {

            return await data.AddDocument(model);
        }
        public async Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int UserId)
        {

            return await data.GetDocumentlist(UserId);
        }

        public async Task<ServiceResponse<IEnumerable<NewFolderView>>> GetTreeDocumentlist(int UserId)
        {

            return await data.GetTreeDocumentlist(UserId);
        }

        public async Task<ServiceResponse<string>> DeleteDocument(long DocumentId)
        {

            return await data.DeleteDocument(DocumentId);
        }














    }
}
