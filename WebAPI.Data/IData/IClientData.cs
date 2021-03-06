using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.ViewModel.Compliance;
using ES_HomeCare_API.ViewModel.Employee;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Data.IData
{
    public interface IClientData
    {
        Task<ServiceResponse<int>> AddClient(ClientModel _model);
        Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId);
        Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model);
        Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId);
        Task<ServiceResponse<string>> DelClientStatus(int StatusId);
        Task<ServiceResponse<string>> UpdateClientStatus(ClientStatusModel _model);
        Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model, int Flag);
        Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list);
        Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId);
        Task<ServiceResponse<string>> UpdateService(ServiceTaskModel item);
        Task<ServiceResponse<string>> DeleteService(int SrvId);
        Task<ServiceResponse<IEnumerable<ClientEmrgencyInfo>>> ClienEmergencyInfo(ClientEmrgencyInfo Model);
        Task<ServiceResponse<string>> CreateEmpDeclined(EmployeeDecline _model);
        Task<ServiceResponse<IEnumerable<EmployeeDeclineView>>> GetEmpDeclined(int userId);
        Task<ServiceResponse<string>> UpdateEmpDeclined(EmployeeDecline item);
        Task<ServiceResponse<string>> DeleteEmpDeclined(int declinedId);
        Task<ServiceResponse<string>> SaveClientContactLog(ClientContactLog _model);
        Task<ServiceResponse<IEnumerable<ClientContactLog>>> GetClientContactLogs(int ClientId);
        Task<ServiceResponse<IEnumerable<ClientContactLog>>> getClientContactLogDetails(int contactLogId);
        Task<ServiceResponse<string>> UpdateClientContactLog(ClientContactLog item);
        Task<ServiceResponse<string>> DeleteClientContactLog(int contactLogId);
        Task<ServiceResponse<string>> AddOtherInfo(OtherInfoModel _model);
        Task<ServiceResponse<string>> UpdateOtherInfo(OtherInfoModel item);
        Task<ServiceResponse<OtherInfoModel>> GetOtherInfo(int UserId);
        Task<ServiceResponse<string>> AddDiagnosis(DiagnosisModel _model);
        Task<ServiceResponse<string>> UpdateDiagnosis(DiagnosisModel item);
        Task<ServiceResponse<IEnumerable<DiagnosisView>>> GetDiagnosisModel(int UserId);
        Task<ServiceResponse<string>> DeleteDiagnosis(int DiagnosisId);
        Task<ServiceResponse<List<ClientNote>>> ClientNoteOperation(ClientNote Model, int Flag);
        Task<ServiceResponse<List<ClientCommunityMaster>>> ClientCommunityOperation(ClientCommunityMaster Model, int Flag);
        Task<ServiceResponse<IEnumerable<ProvisionInfo>>> ClienProvisionInfo(DataTable dt, int UserId = 0);
        Task<ServiceResponse<List<ClientCompliance>>> ClientComplianceOperation(ClientCompliance Model, int Flag);
        Task<ServiceResponse<IEnumerable<ClientResult>>> SearchClient(string search);

        Task<ServiceResponse<string>> AddEmergContact(ContactModel model);
        Task<ServiceResponse<string>> AddEmergProvider(ProviderModel model);
        Task<ServiceResponse<string>> DelEmergProvider(int ProviderId);
        Task<ServiceResponse<ContactModel>> GetEmergContact(int userId, short typeId);
        Task<ServiceResponse<IEnumerable<ProviderModel>>> GetEmergProvider(int userId);
        Task<ServiceResponse<ComplianceCountsViewModel>> GetComplianceCountByUserid(int userId);
        Task<ServiceResponse<ClientEmployeeAttendanceViewModel>> GetClientANDEmployeeAttendanceDetails(int meetingId);
        Task<ServiceResponse<ClockinoutDetailsModel>> GetClockinOutDetailsByClientAndMeetingid(long clientId, long meetingId);
    }
}
