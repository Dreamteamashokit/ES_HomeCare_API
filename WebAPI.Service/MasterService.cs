using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Master;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class MasterService : IMasterService
    {
        private readonly IMasterData data;
        public MasterService(IMasterData ldata)
        {
            data = ldata;
        }

        public async Task<ServiceResponse<string>> CreateDiagnosis(DiagnosisItem _model)
        {
            return await data.CreateDiagnosis(_model);

        }

        public async Task<ServiceResponse<IEnumerable<DiagnosisItem>>> GetDiagnosis()

        {
            return await data.GetDiagnosis();

        }

        public async Task<ServiceResponse<string>> UpdateTask(TaskModel item)
        {
            return await data.UpdateTask(item);
        }

        public async Task<ServiceResponse<string>> ActiveTask(int TaskId, int Status)
        {
            return await data.ActiveTask(TaskId, Status);
        }

        public async Task<ServiceResponse<string>> AddCategory(CategoryModel _model)
        {
            return await data.AddCategory(_model);
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetParentCategoryList()
        {
            return await data.GetParentCategoryList();
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetSubCategoryList(int categoryId)
        {
            return await data.GetSubCategoryList(categoryId);
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetMasterCategoryList()
        {
            return await data.GetMasterCategoryList();
        }
    }
}
