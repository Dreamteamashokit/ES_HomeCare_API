﻿using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.ViewModel.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Service.IService
{
    public interface IEmployeeService
    {
        #region Employee
        Task<ServiceResponse<string>> AddEmployee(EmployeeModel _model);
        Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(ClientFilter model);
        Task<ServiceResponse<IEnumerable<EmployeeList>>> GetEmployeeListObj(int userId);
        Task<ServiceResponse<string>> DeleteEmployee(int UserId);
        Task<ServiceResponse<EmployeeJson>> GetEmployeeById(int UserId);

        #endregion


        Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model);
        Task<ServiceResponse<AddressModel>> GetEmpAddress(int empId);
        Task<ServiceResponse<string>> AddIncident(IncidentModel _model);
        Task<ServiceResponse<IEnumerable<IncidentModel>>> GetIncidentList(int empId);
        Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model);
        Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId);               
        Task<ServiceResponse<string>> SaveExitEmpStatus(StatusModel EmpId);
        Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList();
        Task<ServiceResponse<IEnumerable<AvailabilityStatus>>> GetEmpStatusList(int empId);
        Task<ServiceResponse<string>> AddCompliance(ComplianceModel _model);
        Task<ServiceResponse<IEnumerable<ComplianceModel>>> GetComplianceList(int empId);
        Task<ServiceResponse<string>> SaveEmpPayRate(EmployeeRateModel Emprate);
        Task<ServiceResponse<List<EmpRate>>> GetEmpPayRate(long EmpId, long ClientId);


        Task<ServiceResponse<string>> SaveEmpDeclinedCase(EmpDeclinedCase client);
        Task<ServiceResponse<List<EmpDeclinedCase>>> GetEmpDeclinedcase(int EmpId);

        Task<ServiceResponse<CaregiverViewModel>> GetCareGiverDetails(int UserId);
        Task<ServiceResponse<ExternalLoginViewModel>> ExternalLogin(ExternalLoginModel externalLoginModel);
        Task<ServiceResponse<List<ClientListViewModel>>> GetClientListByempId(int empId);
    }
}
