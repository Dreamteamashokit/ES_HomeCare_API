using ES_HomeCare_API.Model;
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
    public class MeetingService : IMeetingService
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

        public async Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList(ClientFilter model)
        {
            return await data.GetClientMeetingList(model);
        }

        public async Task<ServiceResponse<MeetingView>> GetMeetingDetail(long meetingId)

        {
            return await data.GetMeetingDetail(meetingId);
        }



        public async Task<ServiceResponse<string>> UpdateMeeting(MeetingModel _model)
        {
            return await data.UpdateMeeting(_model);
        }

        public async Task<ServiceResponse<string>> PostNote(NotesModel _model)
        {
            return await data.PostNote(_model);
        }

        public async Task<ServiceResponse<string>> ChangeStatus(MeetingStatus _model)
        {
            return await data.ChangeStatus(_model);
        }

        public async Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetUserMeetingList(int _userId, short _userTypeId)


        {
            return await data.GetUserMeetingList(_userId, _userTypeId);
        }


        public async Task<ServiceResponse<IEnumerable<MeetingView>>> UpCommingAppointments(int ClientId)
        {
            return await data.UpCommingAppointments(ClientId);
        }


        public async Task<ServiceResponse<string>> AddRecurringMeeting(MeetingModel model)
        {
            return await data.AddRecurringMeeting(model);
        }

    }
}
