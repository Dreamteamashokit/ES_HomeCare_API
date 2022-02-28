using ES_HomeCare_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Service.IService
{
    public interface IClientService
    {
        Task<ServiceResponse<string>> savenewclient(Client client);
        Task<ServiceResponse<List<Client>>> GetClientList();
        Task<ServiceResponse<IEnumerable<ClientMeetings>>> GetClientMeetings(string startdate, string cID);
        Task<ServiceResponse<string>> scheduleclientmeeting(MeetingDetails meeting);
    }
}
