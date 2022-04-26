using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using System;
using System.Collections.Generic;
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
            return await data.ClientMedicationcs(Model, Flag);
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


        public async Task<ServiceResponse<string>> SaveClientContactLog(ClientContactLog _model)
        {
            return await data.SaveClientContactLog(_model);
        }

        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> GetClientContactLogs(int ClientId)
        {
            return await data.GetClientContactLogs(ClientId);
        }

        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> getClientContactLogDetails(int contactLogId)
        {
            return await data.getClientContactLogDetails(contactLogId);
        }

        public async Task<ServiceResponse<string>> UpdateClientContactLog(ClientContactLog item)
        {
            return await data.UpdateClientContactLog(item);
        }

        public async Task<ServiceResponse<string>> DeleteClientContactLog(int contactLogId)
        {
            return await data.DeleteClientContactLog(contactLogId);
        }

        public async Task<ServiceResponse<List<ClientNote>>> ClientNoteOperation(ClientNote Model, int Flag)
        {
            return await data.ClientNoteOperation(Model, Flag);
        }

        public async Task<ServiceResponse<List<ClientCommunityMaster>>> ClientCommunityOperation(ClientCommunityMaster Model, int Flag)
        {
            return await data.ClientCommunityOperation(Model, Flag);
        }

        public async Task<ServiceResponse<List<ClientCompliance>>> ClientComplianceOperation(ClientCompliance Model, int Flag)
        {
            return await data.ClientComplianceOperation(Model, Flag);
        }
    }
}
