using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ServiceResponse<string>> savenewemployee(Employee employee)
        {
            return await data.savenewemployee(employee);
        }

        public async Task<ServiceResponse<List<Employee>>> GetEmployeesList()
        {
            return await data.GetEmployeesList();
        }

        public async Task<ServiceResponse<string>> DeleteEmployee(int EmpId)
        {
            return await data.DeleteEmployee(EmpId);
        }

        public async Task<ServiceResponse<Employee>> GetEmployeeById(string EmpId)
        {
            return await data.GetEmployeeById(EmpId);
        }
        
        public async Task<ServiceResponse<string>> AddEmpAddress(AddressModel _model)
        {
            return await data.AddEmpAddress(_model);
        }
        

        public async Task<ServiceResponse<string>> AddIncident(IncidentMode _model)
        {
            return await data.AddIncident(_model);
        }


        public async Task<ServiceResponse<IEnumerable<IncidentMode>>> GetIncidentList(int empId)
        {
            return await data.GetIncidentList(empId);
        }

        public async Task<ServiceResponse<string>> AddAttendance(AttendanceModel _model)
        {
            return await data.AddAttendance(_model);
        }

        public async Task<ServiceResponse<IEnumerable<AttendanceModel>>> GetAttendanceList(int empId)
        {
            return await data.GetAttendanceList(empId);
        }


        



        public async Task<ServiceResponse<string>> SaveExitEmpStatus(StatusModel _obj)
        {
            return await data.SaveExitEmpStatus(_obj);
        }




        public async Task<ServiceResponse<IEnumerable<AvailabilityMaster>>> GetAvailabilityList()
        {
            return await data.GetAvailabilityList();
        }


        public async Task<ServiceResponse<IEnumerable<AvailabilityStatus>>> GetEmpStatusList()
        {
            return await data.GetEmpStatusList();
        }






    }
}
