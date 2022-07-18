using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
using ES_HomeCare_API.ViewModel.Meeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Service.IService
{
    public interface IMeetingService
    {
        Task<ServiceResponse<string>> AddMeeting(MeetingModel _model);
        Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetEmpMeetingList(int empId);
        Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList();
        Task<ServiceResponse<IEnumerable<ClientMeeting>>> GetClientMeetingList(ClientFilter model);
        Task<ServiceResponse<MeetingView>> GetMeetingDetail(long meetingId);
        Task<ServiceResponse<string>> UpdateMeeting(MeetingModel _model);
        Task<ServiceResponse<string>> AddUpdateMeetingRate(MeetingRateModel model);
        Task<ServiceResponse<string>> PostNote(NotesModel _model);
        Task<ServiceResponse<string>> ChangeStatus(MeetingStatus _model);
        Task<ServiceResponse<IEnumerable<EmpMeeting>>> GetUserMeetingList(int _userId, short _userTypeId);
        Task<ServiceResponse<IEnumerable<MeetingView>>> UpCommingAppointments(int ClientId);
        Task<ServiceResponse<string>> AddRecurringMeeting(MeetingModel model);
        Task<ServiceResponse<IEnumerable<MeetingLog>>> GetMeetingLog(long MeetingId);
        Task<ServiceResponse<MeetingRateViewModel>> GetMeetingRateByMeetingId(long MeetingId);

    }
}
