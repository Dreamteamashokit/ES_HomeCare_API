﻿using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Document;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface IDocumentData
    {

        Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int empId);

<<<<<<< HEAD

=======
>>>>>>> dad4c1170142d23b5de4648a36caa52f5948f010
        Task<ServiceResponse<string>> SaveFolder(FolderData model);

        Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int EmpID);
        Task<ServiceResponse<string>> Savefile(UploadFileFolder model);
        Task<ServiceResponse<string>> DeleteFile(DeleteItem item);
    }
}

