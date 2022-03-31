using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Meeting;
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
        Task<ServiceResponse<MeetingView>> GetMeetingDetail(long meetingId);
    }
}
