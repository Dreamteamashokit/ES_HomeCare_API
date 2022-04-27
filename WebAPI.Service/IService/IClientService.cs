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
        Task<ServiceResponse<IEnumerable<ProvisionInfo>>> ClienProvisionInfo(IEnumerable<ProvisionInfo> model, int UserId = 0);
        Task<ServiceResponse<List<ClientCompliance>>> ClientComplianceOperation(ClientCompliance Model, int Flag);
    }
}
