using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Data.IData
{
    public interface IEmployeeData
    {
        #region Employee
        Task<ServiceResponse<int>> AddEmployee(EmployeeModel _model);
        Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(int userId);
        Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(ClientFilter model);
        Task<ServiceResponse<string>> DeleteEmployee(int UserId);
        Task<ServiceResponse<EmployeeJson>> GetEmployeeById(int UserId);
        #endregion

        Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model);
        Task<ServiceResponse<AddressModel>> GetEmpAddress(int empId);

        Task<ServiceResponse<string>> AddIncident(IncidentModel _model);
        Task<ServiceResponse<IEnumerable<IncidentModel>>> GetIncidentList(int empId);
        Task<ServiceResponse<string>> DelIncident(int IncidentId);

        Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model);
        Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId);
        Task<ServiceResponse<string>> DelAttendance(int AttendanceId);

        Task<ServiceResponse<string>> AddEmpStatus(StatusModel _model);
        Task<ServiceResponse<IEnumerable<StatusModel>>> GetEmpStatusList(int empId);
        Task<ServiceResponse<string>> DelEmpStatus(int StatusId);

        Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList();

        Task<ServiceResponse<string>> AddEmpRate(EmployeeRateModel _model);
        Task<ServiceResponse<IEnumerable<EmployeeRateModel>>> GetEmpPayRate(int empId);
        Task<ServiceResponse<string>> DelEmpPayRate(int RateId);

        Task<ServiceResponse<string>> AddDeclinedCase(EmpDeclinedCase _model);
        Task<ServiceResponse<IEnumerable<EmpDeclinedCase>>> GetDeclinedCaseList(int empId);
        Task<ServiceResponse<string>> DelDeclinedCase(int DeclinedCaseId);

        Task<ServiceResponse<string>> AddCompliance(ComplianceModel _model);
        Task<ServiceResponse<IEnumerable<ComplianceModel>>> GetComplianceList(int UserId);
        Task<ServiceResponse<string>> DeleteCompliance(int complianceId);



        Task<ServiceResponse<CaregiverViewModel>> GetCareGiverDetails(int UserId);
        Task<ServiceResponse<ExternalLoginViewModel>> ExternalLogin(ExternalLoginModel externalLoginModel);
        Task<ServiceResponse<List<ClientListViewModel>>> GetClientListByempId(int empId);
        Task<ServiceResponse<HHAClockInDetailsModel>> GetClockinDetails(int userId);
        Task<ServiceResponse<string>> HHAClockin(HHAClockInModel hhaClockin);
        Task<ServiceResponse<string>> AddUpdateRatings(RatingModel _model);
        Task<ServiceResponse<RatingViewModel>> GetRatingsDetails(long userId);
    }
}
