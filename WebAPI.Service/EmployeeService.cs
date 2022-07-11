using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;
using WebAPI_SAMPLE.WebAPI.Service.IService;

namespace WebAPI_SAMPLE.WebAPI.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeData data;
        public EmployeeService(IEmployeeData ldata)
        {
            data = ldata;
        }
        #region Employee
        public async Task<ServiceResponse<int>> AddEmployee(EmployeeModel _model)
        {
            return await data.AddEmployee(_model);
        }

        public async Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(int userId)
        {
            return await data.GetEmployeeListObj(userId);
        }
        public async Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(ClientFilter model)
        {
            return await data.GetEmployeeListObj(model);
        }
        public async Task<ServiceResponse<string>> DeleteEmployee(int UserId)
        {
            return await data.DeleteEmployee(UserId);
        }

        public async Task<ServiceResponse<EmployeeJson>> GetEmployeeById(int UserId)
        {
            return await data.GetEmployeeById(UserId);
        }

        #endregion






        #region Address
        public async Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model)
        {
            return await data.AddEmpAddress(_model);
        }
        public async Task<ServiceResponse<AddressModel>> GetEmpAddress(int empId)
        {
            return await data.GetEmpAddress(empId);
        }
        #endregion
        public async Task<ServiceResponse<string>> AddIncident(IncidentModel _model)
        {
            return await data.AddIncident(_model);
        }
        public async Task<ServiceResponse<IEnumerable<IncidentModel>>> GetIncidentList(int empId)
        {
            return await data.GetIncidentList(empId);
        }
        public async Task<ServiceResponse<string>> DelIncident(int IncidentId)
        {
            return await data.DelIncident(IncidentId);
        }
        

        public async Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model)
        {
            return await data.AddAttendance(_model);
        }
        public async Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId)
        {
            return await data.GetAttendanceList(empId);
        }

        public async Task<ServiceResponse<string>> DelAttendance(int AttendanceId)
        {
            return await data.DelAttendance(AttendanceId);
        }    


        public async Task<ServiceResponse<string>> AddEmpStatus(StatusModel _obj)
        {
            return await data.AddEmpStatus(_obj);
        }
        public async Task<ServiceResponse<IEnumerable<StatusModel>>> GetEmpStatusList(int empId)
        {
            return await data.GetEmpStatusList(empId);
        
        }
        public async Task<ServiceResponse<string>> DelEmpStatus(int StatusId)
        {
            return await data.DelEmpStatus(StatusId);
        }

        public async Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList()
        {
            return await data.GetAvailabilityList();
        }    

        public async Task<ServiceResponse<string>> AddEmpRate(EmployeeRateModel _model)
        {
            return await data.AddEmpRate(_model);
        }

        public async Task<ServiceResponse<IEnumerable<EmployeeRateModel>>> GetEmpPayRate(int empId)
        {
            return await data.GetEmpPayRate(empId);
        }
        public async Task<ServiceResponse<string>> DelEmpPayRate(int RateId)
        {
            return await data.DelEmpPayRate(RateId);
        }
        public async Task<ServiceResponse<string>> AddDeclinedCase(EmpDeclinedCase _model)
        {
            return await data.AddDeclinedCase(_model);
        }
        public async Task<ServiceResponse<IEnumerable<EmpDeclinedCase>>> GetDeclinedCaseList(int empId)
        {
            return await data.GetDeclinedCaseList(empId);
        }
        public async Task<ServiceResponse<string>> DelDeclinedCase(int DeclinedCaseId)
        {
            return await data.DelDeclinedCase(DeclinedCaseId);
        }

        public async Task<ServiceResponse<string>> AddCompliance(ComplianceModel _model)
        {
            return await data.AddCompliance(_model);
        }
        public async Task<ServiceResponse<IEnumerable<ComplianceModel>>> GetComplianceList(int empId)
        {
            return await data.GetComplianceList(empId);
        }

        public async Task<ServiceResponse<ComplianceList>> GetLatestThreeOverdueComplianceList(int userId)
        {
            return await data.GetLatestThreeOverdueComplianceList(userId);
        }
        public async Task<ServiceResponse<string>> DeleteCompliance(int complianceId)
        {
            return await data.DeleteCompliance(complianceId);
        }


        public async Task<ServiceResponse<CaregiverViewModel>> GetCareGiverDetails(int UserId)
        {
            return await data.GetCareGiverDetails(UserId);
        }

        public async Task<ServiceResponse<ExternalLoginViewModel>> ExternalLogin(ExternalLoginModel externalLoginModel)
        {
            return await data.ExternalLogin(externalLoginModel);
        }

        public async Task<ServiceResponse<List<ClientListViewModel>>> GetClientListByempId(int empId)
        {
            return await data.GetClientListByempId(empId);
        }

        public async Task<ServiceResponse<HHAClockInDetailsModel>> GetClockinDetails(int userId)
        {
            return await data.GetClockinDetails(userId);
        }

        public async Task<ServiceResponse<string>> HHAClockin(HHAClockInModel hhaClockin)
        {
            return await data.HHAClockin(hhaClockin);
        }

        public async Task<ServiceResponse<string>> AddUpdateRatings(RatingModel _model)
        {
            return await data.AddUpdateRatings(_model);
        }
        public async Task<ServiceResponse<RatingViewModel>> GetRatingsDetails(long userId)
        {
            return await data.GetRatingsDetails(userId);
        }
    }
}
