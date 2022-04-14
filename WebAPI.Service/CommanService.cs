using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
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


        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterList(short typeId)
        {
            return await data.GetMasterList(typeId);
        }
        public async Task<ServiceResponse<string>> CreateMasterType(string _item)
        {
            return await data.CreateMasterType(_item);
        }
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterTypeList()
        {
            return await data.GetMasterTypeList();
        }
        public async Task<ServiceResponse<string>> CreateMaster(ItemObj _item)
        {
            return await data.CreateMaster(_item);
        }
        public async Task<ServiceResponse<IEnumerable<ItemObj>>> GetSystemMaster()
        {
            return await data.GetSystemMaster();
        }
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmpTypeList()
        {
            return await data.GetEmpTypeList();
        }
        public async Task<ServiceResponse<IEnumerable<SelectList>>> GetCountry()
        {
            return await data.GetCountry();
        }
        public async Task<ServiceResponse<IEnumerable<SelectList>>> GetState(string countryCode)
        {
            return await data.GetState(countryCode);
        }
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployees(string type)
        {
            return await data.GetEmployees(type);
        }
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployeesList()
        {
            return await data.GetEmployeesList();
        }
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetClientList()
        {
            return await data.GetClientList();

        }

        public async Task<ServiceResponse<string>> CreateTask(TaskModel _model)
        {
            return await data.CreateTask(_model);

        }

        public async Task<ServiceResponse<IEnumerable<TaskModel>>> GetTaskList()
        {
            return await data.GetTaskList();

        }




    }
}
