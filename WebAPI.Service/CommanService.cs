using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class CommanService : ICommanService
    {
        private readonly ICommanData data;
        public CommanService(ICommanData ldata)
        {
            data = ldata;
        }
        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetOfficeUserLst()
        {
            return await data.GetOfficeUserLst();
        }

        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetTypeStatusLst()
        {
            return await data.GetTypeStatusLst();
        }


        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetEmployeeLst()
        {
            return await data.GetEmployeeLst();
        }
        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetScheduleLst()
        {
            return await data.GetScheduleLst();
        }


        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterList(short typeId)
        {
            return await data.GetMasterList(typeId);
        }
    }
}
