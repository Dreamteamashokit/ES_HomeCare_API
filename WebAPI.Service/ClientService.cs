using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE.WebAPI.Service
{
    public class ClientService : IClientService
    {
        private readonly IClientData data;
        public ClientService(IClientData ldata)
        {
            data = ldata;
        }


        public async Task<ServiceResponse<string>> AddClient(ClientModel client)
        {
            return await data.AddClient(client);
        }

        public async Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId)
        {
            return await data.GetClientDetail(clientId);
        }
        public async Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model)
        {
            return await data.SaveClientStatus(_model);
        }

        public async Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId)
        {
            return await data.GetClientStatusList(ClientId);
        }

        public async Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model, int Flag)
        {
            return await data.ClientMedicationcs(Model,Flag);
        }



        public async Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list)
        {
            return await data.CreateServiceTask(_list);
        }

        public async Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId)
        {
            return await data.GetServiceTaskList(userId);
        }


        public async Task<ServiceResponse<string>> UpdateService(ServiceTaskModel item)
        {
            return await data.UpdateService(item);
        }


        public async Task<ServiceResponse<string>> DeleteService(int SrvId)
        {
            return await data.DeleteService(SrvId);
        }

        public async Task<ServiceResponse<IEnumerable<ClientEmrgencyInfo>>> ClienEmergencyInfo(ClientEmrgencyInfo Model)
        {
            return await data.ClienEmergencyInfo(Model);
        }

        public async Task<ServiceResponse<string>> CreateEmpDeclined(EmployeeDecline _model)
        {
            return await data.CreateEmpDeclined(_model);
        }
        public async Task<ServiceResponse<IEnumerable<EmployeeDeclineView>>> GetEmpDeclined(int userId)
        {
            return await data.GetEmpDeclined(userId);
        }
        public async Task<ServiceResponse<string>> UpdateEmpDeclined(EmployeeDecline item)
        {
            return await data.UpdateEmpDeclined(item);
        }
        public async Task<ServiceResponse<string>> DeleteEmpDeclined(int declinedId)
        {
            return await data.DeleteEmpDeclined(declinedId);
        }

        public async Task<ServiceResponse<IEnumerable<ProvisionInfo>>> ClienProvisionInfo(IEnumerable<ProvisionInfo> Model, int UserId = 0)
        {



            DataTable dt = CreateTable();
            if (Model!=null)
            {
                foreach (ProvisionInfo item in Model)
                {
                    dt.Rows.Add(item.ProvisionId, item.Value, UserId);
                }
            }
            
            return await data.ClienProvisionInfo(dt,UserId);
        }


        DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProvisionId", typeof(Int32));
            dt.Columns.Add("ProvisionValue", typeof(string));
            dt.Columns.Add("UserId", typeof(Int32));
            return dt;
        }










    }
}
