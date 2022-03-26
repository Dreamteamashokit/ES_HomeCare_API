using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface ICommanService
    {
        Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetOfficeUserLst();
        Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetTypeStatusLst();
        Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetEmployeeLst();
        Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetScheduleLst();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterList(short typeId);
        Task<ServiceResponse<string>> SaveFolder(FolderData model);
        Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int EmpId);

        Task<ServiceResponse<string>> Savefile(UploadFileFolder model);
    }
}
