using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Service.IService
{
    public interface IClientService
    {

        Task<ServiceResponse<string>> AddClient(ClientModel _model);
        Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId);
        Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model);
        Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId);
    }
}
