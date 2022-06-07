using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Master;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;


namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IMasterService
    {
        Task<ServiceResponse<string>> CreateDiagnosis(DiagnosisItem _model);
        Task<ServiceResponse<IEnumerable<DiagnosisItem>>> GetDiagnosis();
        Task<ServiceResponse<string>> UpdateTask(TaskModel item);
        Task<ServiceResponse<string>> ActiveTask(int TaskId, int Status);
        Task<ServiceResponse<string>> AddCategory(CategoryModel model);
        Task<ServiceResponse<IEnumerable<CategoryModel>>> GetParentCategoryList();
        Task<ServiceResponse<IEnumerable<CategoryModel>>> GetSubCategoryList(int categoryId);
        Task<ServiceResponse<IEnumerable<CategoryModel>>> GetMasterCategoryList();
    }
}
