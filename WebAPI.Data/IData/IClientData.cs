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
        Task<ServiceResponse<string>> savenewclient(Client client);
        Task<ServiceResponse<List<Client>>> GetClientList();
        Task<ServiceResponse<IEnumerable<ClientMeetings>>> GetClientMeetings(string startdate, string cID);
        Task<ServiceResponse<string>> scheduleclientmeeting(MeetingDetails meeting);
        Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model);
        Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId);
    }
}
