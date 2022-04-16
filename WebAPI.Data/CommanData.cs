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
using System.Data;
using System;
using ES_HomeCare_API.Helper;
using ES_HomeCare_API.Model.Client;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class CommanData : ICommanData
    {
        private IConfiguration configuration;
        public CommanData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> CreateMasterType(string _item)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblMasterType (MasterName) values(@MasterName);";


                    int rowsAffected = cnn.Execute(sqlQuery, new { @MasterName = _item });



                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }

            return sres;
        }

        public async Task<ServiceResponse<string>> CreateMaster(ItemObj _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblMaster (MasterType,ItemId,ItemName,IsActive) values(@MasterType,@ItemId,@ItemName,@IsActive);";


                    int rowsAffected = cnn.Execute(sqlQuery, new
                    {
                        @MasterType = _model.MasterType,
                        @ItemId = _model.ItemId,
                        @ItemName = _model.ItemName,
                        @IsActive = true
                    });



                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }

            return sres;
        }
       
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetMasterTypeList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.MasterITyped, x.MasterName from tblMasterType x; ";

                IEnumerable<ItemList> resData = (await connection.QueryAsync(sql)).Select(x => new ItemList { ItemId = x.MasterITyped, ItemName = x.MasterName });

                obj.Data = resData;
                obj.Result = resData.Any() ? true : false;
                obj.Message = resData.Any() ? "Data Found." : "No Data found.";
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

        public async Task<ServiceResponse<IEnumerable<ItemObj>>> GetSystemMaster()
        {
            ServiceResponse<IEnumerable<ItemObj>> obj = new ServiceResponse<IEnumerable<ItemObj>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.MasterType, x.ItemId, x.ItemName from tblMaster x  ";

                IEnumerable<ItemObj> resData = (await connection.QueryAsync<ItemObj>(sql));

                obj.Data = resData;
                obj.Result = resData.Any() ? true : false;
                obj.Message = resData.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }
                
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmpTypeList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select * from tblEmpType ;";

                IEnumerable<ItemList> resData = (await connection.QueryAsync(sql)).Select(x => new ItemList { ItemId = x.TypeId, ItemName = x.TypeName });

                obj.Data = resData;
                obj.Result = resData.Any() ? true : false;
                obj.Message = resData.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<SelectList>>> GetCountry()
        {
            ServiceResponse<IEnumerable<SelectList>> obj = new ServiceResponse<IEnumerable<SelectList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select *  from tblCountry; ";
                IEnumerable<SelectList> cmeetings = (await connection.QueryAsync(sql)).Select(x => new SelectList { ItemCode = x.Code, ItemName = x.Name });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<SelectList>>> GetState(string CountryCode)
        {
            ServiceResponse<IEnumerable<SelectList>> obj = new ServiceResponse<IEnumerable<SelectList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select *  from tblStates where CountryCode=@CountryCode; ";
                IEnumerable<SelectList> cmeetings = (await connection.QueryAsync(sql, new { @CountryCode = CountryCode })).Select(x => new SelectList { ItemCode = x.InternetCountryCode, ItemName = x.StateName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployees(string type)
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = "Select x.UserId,x.LastName +' '+ x.FirstName+'(' + ISNULL(x.UserKey,'0-0-0')+')' as EmpName from tblUser x inner join tblEmployee y on x.UserId=y.UserId where y.EmpType=@EmpType;";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry, new { @EmpType = type })).Select(x => new ItemList { ItemId = x.UserId, ItemName = x.EmpName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }
        
        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployeesList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
          

                string sqlqry = "select x.UserId,x.LastName +' '+ x.FirstName+'(' +ISNULL(x.UserKey,'0-0-0')+')' as EmpName from  tblUser x inner join tblEmployee y on x.UserId=y.UserId where x.IsActive=1;";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry)).Select(x => new ItemList { ItemId = x.UserId, ItemName = x.EmpName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetClientList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = "select x.UserId,x.LastName +' '+ x.FirstName+'(' +ISNULL(x.UserKey,'1-1-1')+')' as ClientName from  tblUser x inner join tblClient y on x.UserId=y.UserId where x.IsActive=1;";
                
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry)).Select(x => new ItemList { ItemId = x.UserId, ItemName = x.ClientName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<string>> CreateTask(TaskModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string Query = "INSERT INTO tblTaskMaster (TaskCode,TaskName,TaskDetail,IsActive,CreatedOn,CreatedBy) VALUES (@TaskCode,@TaskName,@TaskDetail,@IsActive,@CreatedOn,@CreatedBy);";


                    int rowsAffected = cnn.Execute(Query, new
                    {
                        @TaskCode = _model.TaskCode,
                        @TaskName = _model.TaskName,
                        @TaskDetail = _model.TaskDetail,
                        @IsActive = _model.IsActive,
                        @CreatedOn = _model.CreatedOn,
                        @CreatedBy = _model.CreatedBy,
                    });

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }

            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<TaskModel>>> GetTaskList()
        {
            ServiceResponse<IEnumerable<TaskModel>> obj = new ServiceResponse<IEnumerable<TaskModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sqlqry = "select * from tblTaskMaster; ";
                IEnumerable<TaskModel> objResult = (await connection.QueryAsync<TaskModel>(sqlqry));

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }


        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetNoteTypeList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = "select  NotesTypeId , Description from tblNotesType(nolock) where IsActive=1;";

                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry)).Select(x => new ItemList { ItemId = x.NotesTypeId, ItemName = x.Description });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }
    }
}
