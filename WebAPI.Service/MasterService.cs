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

        public async Task<ServiceResponse<string>> AddCMPLCategory(CategoryModel _model)
        {
            return await data.AddCMPLCategory(_model);
        }  

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetCMPLCategoryList(int CategoryId)
        {
            return await data.GetCMPLCategoryList(CategoryId);
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetCMPLUserCategoryList(short CatType)
        {
            return await data.GetCMPLUserCategoryList(CatType);
        }

        public async Task<ServiceResponse<string>> DelCMPLCategory(int CategoryId)
        {
            return await data.DelCMPLCategory(CategoryId);
        }

    }
}
