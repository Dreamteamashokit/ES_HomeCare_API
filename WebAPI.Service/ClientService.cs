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

        public async Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model)
        {
            return await data.ClientMedicationcs(Model);
        }



        public async Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list)
        {
            return await data.CreateServiceTask(_list);
        }

        public async Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId)
        {
            return await data.GetServiceTaskList(userId);
        }







    }
}
