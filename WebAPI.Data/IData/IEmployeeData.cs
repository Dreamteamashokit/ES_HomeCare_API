using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace WebAPI_SAMPLE.WebAPI.Data.IData
{
    public interface IEmployeeData
    {
        Task<ServiceResponse<string>> savenewemployee(Employee client);
        Task<ServiceResponse<List<Employee>>> GetEmployeesList();
        Task<ServiceResponse<string>> DeleteEmployee(int EmpId);
        Task<ServiceResponse<Employee>> GetEmployeeById(string EmpId);
    }
}
