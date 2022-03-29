using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
using ES_HomeCare_API.WebAPI.Data.IData;
using ES_HomeCare_API.WebAPI.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service
{
    public class MeetingService: IMeetingService
    {
        private readonly IMeetingData data;
        public MeetingService(IMeetingData ldata)
        {
            data = ldata;
        }


        public async Task<ServiceResponse<string>> AddMeeting(MeetingModel model)
        {
            return await data.AddMeeting(model);
        }

        public async Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetEmpMeetingList(int empId)
        {
            return await data.GetEmpMeetingList(empId);
        }
        public async Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList()
        {
            return await data.GetClientMeetingList();
        }

       

    }
}
