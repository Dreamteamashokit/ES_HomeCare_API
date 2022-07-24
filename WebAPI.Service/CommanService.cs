using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using Microsoft.Extensions.Configuration;
namespace ES_HomeCare_API.WebAPI.Service
{
    public class CommanService : ICommanService
    {
        private readonly ICommanData data;
        private IConfiguration configuration;
        public CommanService(ICommanData ldata, IConfiguration _configuration)
        {
            data = ldata;
            configuration = _configuration;
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
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployees(int type)
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

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetNoteTypeList()
        {
            return await data.GetNoteTypeList();
        }

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetDiagnosisList()
        {
            return await data.GetDiagnosisList();

        }



        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetProvisionList(int ProvisionType)
        {
            return await data.GetProvisionList(ProvisionType);
        }


        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetPayers()
        {
            return await data.GetPayers();
        }




        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetUsers(int type)
        {
            return await data.GetUsers(type);
        }


        public async Task<ServiceResponse<ClientGeoProvisions>> GetUsersGeoProvision(int UserId)
        {
            return await data.GetUsersGeoProvision(UserId);
        }


        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetCMPLCategoryList(int CategoryId, short UserTypeId)
        {
            return await data.GetCMPLCategoryList(CategoryId, UserTypeId);
        }


        public async Task<bool> SendEmail(Email model)
        {
            bool isSend = false;
            EmailSmtp EmailSmtpObj = new EmailSmtp(configuration);
            string emailBody = EmailSmtpObj.SupportEmail();
            emailBody = emailBody.Replace("{user}", "Admin");
            emailBody = emailBody.Replace("{message}", model.Message);
            emailBody = emailBody.Replace("{support}", "Admin");
            EmailSmtpObj.SendMail(mailTo: "", mailSubject: "Clock in out issue", mailBody: emailBody);
            return isSend;
        }
    }
}
