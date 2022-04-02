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

        public async Task<ServiceResponse<string>> savenewclient(Client client)
        {
            return await data.savenewclient(client);
        }

        public async Task<ServiceResponse<List<Client>>> GetClientList()
        {
            return await data.GetClientList();
        }

        public async Task<ServiceResponse<IEnumerable<ClientMeetings>>> GetClientMeetings(string startdate, string cID)
        {
            return await data.GetClientMeetings(startdate, cID);
        }

        public async Task<ServiceResponse<string>> scheduleclientmeeting(MeetingDetails meeting)
        {
            return await data.scheduleclientmeeting(meeting);
        }
    }
}
