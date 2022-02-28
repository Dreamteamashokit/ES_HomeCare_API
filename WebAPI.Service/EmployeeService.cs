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
    }
}
