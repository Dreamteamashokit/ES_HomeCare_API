using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Data.IData
{
    public interface IClientData
    {
        Task<ServiceResponse<string>> AddClient(ClientModel _model);
        Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId);
        Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model);
        Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId);
        Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model);

        Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list);
        Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId);









    }
}
