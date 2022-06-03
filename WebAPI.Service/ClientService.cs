using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ServiceResponse<int>> AddClient(ClientModel client)
        {
            return await data.AddClient(client);
        }
        public async Task<ServiceResponse<ClientModel>> GetClientDetail(int clientId)
        {
            return await data.GetClientDetail(clientId);
        }
        public async Task<ServiceResponse<string>> SaveClientStatus(ClientStatus _model)
        {
            return await data.SaveClientStatus(_model);
        }
        public async Task<ServiceResponse<IEnumerable<ClientStatusLst>>> GetClientStatusList(int ClientId)
        {
            return await data.GetClientStatusList(ClientId);
        }


        public async Task<ServiceResponse<string>> DelClientStatus(int StatusId)
        {
            return await data.DelClientStatus(StatusId);
        }
        public async Task<ServiceResponse<string>> UpdateClientStatus(ClientStatusModel _model)
        {
            return await data.UpdateClientStatus(_model);
        }


        public async Task<ServiceResponse<List<Medicationcs>>> ClientMedicationcs(Medicationcs Model, int Flag)
        {
            return await data.ClientMedicationcs(Model, Flag);
        }
        public async Task<ServiceResponse<string>> CreateServiceTask(IList<ServiceTaskModel> _list)
        {
            return await data.CreateServiceTask(_list);
        }
        public async Task<ServiceResponse<IEnumerable<ServiceTaskView>>> GetServiceTaskList(int userId)
        {
            return await data.GetServiceTaskList(userId);
        }
        public async Task<ServiceResponse<string>> UpdateService(ServiceTaskModel item)
        {
            return await data.UpdateService(item);
        }
        public async Task<ServiceResponse<string>> DeleteService(int SrvId)
        {
            return await data.DeleteService(SrvId);
        }
        public async Task<ServiceResponse<IEnumerable<ClientEmrgencyInfo>>> ClienEmergencyInfo(ClientEmrgencyInfo Model)
        {
            return await data.ClienEmergencyInfo(Model);
        }
        public async Task<ServiceResponse<string>> CreateEmpDeclined(EmployeeDecline _model)
        {
            return await data.CreateEmpDeclined(_model);
        }
        public async Task<ServiceResponse<IEnumerable<EmployeeDeclineView>>> GetEmpDeclined(int userId)
        {
            return await data.GetEmpDeclined(userId);
        }
        public async Task<ServiceResponse<string>> UpdateEmpDeclined(EmployeeDecline item)
        {
            return await data.UpdateEmpDeclined(item);
        }
        public async Task<ServiceResponse<string>> DeleteEmpDeclined(int declinedId)
        {
            return await data.DeleteEmpDeclined(declinedId);
        }
        public async Task<ServiceResponse<string>> SaveClientContactLog(ClientContactLog _model)
        {
            return await data.SaveClientContactLog(_model);
        }
        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> GetClientContactLogs(int ClientId)
        {
            return await data.GetClientContactLogs(ClientId);
        }
        public async Task<ServiceResponse<IEnumerable<ClientContactLog>>> getClientContactLogDetails(int contactLogId)
        {
            return await data.getClientContactLogDetails(contactLogId);
        }
        public async Task<ServiceResponse<string>> UpdateClientContactLog(ClientContactLog item)
        {
            return await data.UpdateClientContactLog(item);
        }
        public async Task<ServiceResponse<string>> DeleteClientContactLog(int contactLogId)
        {
            return await data.DeleteClientContactLog(contactLogId);
        }
        public async Task<ServiceResponse<string>> AddOtherInfo(OtherInfoModel _model)
        {
            return await data.AddOtherInfo(_model);
        }
        public async Task<ServiceResponse<string>> UpdateOtherInfo(OtherInfoModel item)
        {
            return await data.UpdateOtherInfo(item);
        }
        public async Task<ServiceResponse<OtherInfoModel>> GetOtherInfo(int UserId)
        {
            return await data.GetOtherInfo(UserId);
        }
        public async Task<ServiceResponse<string>> AddDiagnosis(DiagnosisModel _model)
        {
            return await data.AddDiagnosis(_model);
        }
        public async Task<ServiceResponse<string>> UpdateDiagnosis(DiagnosisModel item)
        {
            return await data.UpdateDiagnosis(item);
        }
        public async Task<ServiceResponse<IEnumerable<DiagnosisView>>> GetDiagnosisModel(int UserId)
        {
            return await data.GetDiagnosisModel(UserId);
        }
        public async Task<ServiceResponse<string>> DeleteDiagnosis(int DiagnosisId)
        {
            return await data.DeleteDiagnosis(DiagnosisId);
        }
        public async Task<ServiceResponse<List<ClientNote>>> ClientNoteOperation(ClientNote Model, int Flag)
        {
            return await data.ClientNoteOperation(Model, Flag);
        }
        public async Task<ServiceResponse<List<ClientCommunityMaster>>> ClientCommunityOperation(ClientCommunityMaster Model, int Flag)
        {
            return await data.ClientCommunityOperation(Model, Flag);
        }
        public async Task<ServiceResponse<IEnumerable<ProvisionInfo>>> ClienProvisionInfo(IEnumerable<ProvisionInfo> Model, int UserId = 0)
        {
            DataTable dt = CreateTable();
            if (Model != null)
            {
                foreach (ProvisionInfo item in Model)
                {
                    UserId = item.Userid;
                    if (item.ProvisionType == 1)
                    {
                        dt.Rows.Add(item.ProvisionId, item.IsChecked, item.Userid);
                    }
                    else if (item.ProvisionType == 2)
                    {
                        dt.Rows.Add(item.ProvisionId, item.Value, item.Userid);
                    }

                }
            }

            return await data.ClienProvisionInfo(dt, UserId);
        }
        public async Task<ServiceResponse<List<ClientCompliance>>> ClientComplianceOperation(ClientCompliance Model, int Flag)
        {
            return await data.ClientComplianceOperation(Model, Flag);
        }
        DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProvisionId", typeof(Int32));
            dt.Columns.Add("ProvisionValue", typeof(string));
            dt.Columns.Add("UserId", typeof(Int32));
            return dt;
        }
        public async Task<ServiceResponse<IEnumerable<ClientResult>>> SearchClient(string search)
        {
            return await data.SearchClient(search);
        }

        public async Task<ServiceResponse<string>> AddEmergContact(ContactModel model) {
            return await data.AddEmergContact(model);
        }
        public async Task<ServiceResponse<string>> AddEmergProvider(ProviderModel model) {
            return await data.AddEmergProvider(model);
        }
        public async Task<ServiceResponse<string>> DelEmergProvider(int ProviderId) {

            return await data.DelEmergProvider(ProviderId);
        }
        public async Task<ServiceResponse<ContactModel>> GetEmergContact(int userId, short typeId) {
            return await data.GetEmergContact(userId, typeId);
        }
        public async Task<ServiceResponse<IEnumerable<ProviderModel>>> GetEmergProvider(int userId) {
            return await data.GetEmergProvider(userId);
        }


    }
}
