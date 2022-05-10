using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data.IData
{
    public interface ICommanData
    {

        Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterList(short typeId);
        Task<ServiceResponse<string>> CreateMasterType(string _item);
        Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterTypeList();
        Task<ServiceResponse<string>> CreateMaster(ItemObj _model);
        Task<ServiceResponse<IEnumerable<ItemObj>>> GetSystemMaster();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetEmpTypeList();
        Task<ServiceResponse<IEnumerable<SelectList>>> GetCountry();
        Task<ServiceResponse<IEnumerable<SelectList>>> GetState(string countryCode);
        Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployees(string type);
        Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployeesList();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetClientList();

        Task<ServiceResponse<string>> CreateTask(TaskModel _model);
        Task<ServiceResponse<IEnumerable<TaskModel>>> GetTaskList();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetNoteTypeList();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetCategoryList();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetSubCategoryList();
        Task<ServiceResponse<IEnumerable<ItemList>>> GetDiagnosisList();

        Task<ServiceResponse<IEnumerable<ItemList>>> GetProvisionList(int ProvisionType);

    }





}
