using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Service.IService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<string>> savenewemployee(Employee employee);
        Task<ServiceResponse<List<Employee>>> GetEmployeesList();
        Task<ServiceResponse<string>> DeleteEmployee(int EmpId);
        Task<ServiceResponse<Employee>> GetEmployeeById(string EmpId);
        Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model);
        Task<ServiceResponse<AddressModel>> GetEmpAddress(int empId);
        Task<ServiceResponse<string>> AddIncident(IncidentMode _model);
        Task<ServiceResponse<IEnumerable<IncidentMode>>> GetIncidentList(int empId);
        Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model);
        Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId);               
        Task<ServiceResponse<string>> SaveExitEmpStatus(StatusModel EmpId);
        Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList();
        Task<ServiceResponse<IEnumerable<AvailabilityStatus>>> GetEmpStatusList();
        Task<ServiceResponse<string>> AddCompliance(ComplianceModel _model);
        Task<ServiceResponse<IEnumerable<ComplianceModel>>> GetComplianceList(int empId);
        Task<ServiceResponse<string>> SaveEmpPayRate(SaveEmployeeRate Emprate);
        Task<ServiceResponse<List<EmpRate>>> GetEmpPayRate(long EmpId, long ClientId);


        Task<ServiceResponse<string>> SaveEmpDeclinedCase(EmpDeclinedCase client);
        Task<ServiceResponse<List<EmpDeclinedCase>>> GetEmpDeclinedcase(int EmpId);


    }
}
