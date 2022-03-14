using System.Threading.Tasks;
using ES_HomeCare_API.Model.Employee;
using System.Collections.Generic;
using WebAPI_SAMPLE.Model;
using System.Data.SqlClient;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Dapper;
using ES_HomeCare_API.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class CommanData : ICommanData
    {
        private IConfiguration configuration;
        public CommanData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetOfficeUserLst()
        {
            ServiceResponse<IEnumerable<EmpStatusSelectlst>> obj = new ServiceResponse<IEnumerable<EmpStatusSelectlst>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select *  from dbo.tblOfficeUserMaster; ";
                IEnumerable<EmpStatusSelectlst> cmeetings = (await connection.QueryAsync<EmpStatusSelectlst>(sql));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetTypeStatusLst()
        {
            ServiceResponse<IEnumerable<EmpStatusSelectlst>> obj = new ServiceResponse<IEnumerable<EmpStatusSelectlst>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select *  from dbo.tblEmpStatusMaster; ";
                IEnumerable<EmpStatusSelectlst> cmeetings = (await connection.QueryAsync<EmpStatusSelectlst>(sql));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetEmployeeLst()
        {
            ServiceResponse<IEnumerable<EmpStatusSelectlst>> obj = new ServiceResponse<IEnumerable<EmpStatusSelectlst>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "SELECT EmpId,(FirstName + ' ' + MiddleName + ' ' + LastName) as Text FROM tblEmployee; ";
                IEnumerable<EmpStatusSelectlst> cmeetings = (await connection.QueryAsync<EmpStatusSelectlst>(sql));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<EmpStatusSelectlst>>> GetScheduleLst()
        {
            ServiceResponse<IEnumerable<EmpStatusSelectlst>> obj = new ServiceResponse<IEnumerable<EmpStatusSelectlst>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select * from tblEmpStatusScheduling; ";
                IEnumerable<EmpStatusSelectlst> cmeetings = (await connection.QueryAsync<EmpStatusSelectlst>(sql));
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }




        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterList(short typeId)
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.ItemId, x.ItemName from tblMaster x where x.MasterType= @TypeId and x.IsActive= 1; ";

                IEnumerable<ItemList> resData = (await connection.QueryAsync<ItemList>(sql,
       new { @TypeId = typeId }));

                obj.Data = resData;
                obj.Result = resData.Any() ? true : false;
                obj.Message = resData.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }









    }
}
