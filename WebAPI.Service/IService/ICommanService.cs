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

<<<<<<< HEAD

      
=======
>>>>>>> dad4c1170142d23b5de4648a36caa52f5948f010
  

    }
}
