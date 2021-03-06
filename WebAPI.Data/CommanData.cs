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
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.ViewModel.Employee;

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
                sres.Result = false;
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
                sres.Result = false;
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
                string sql = "select x.MasterTypeId, x.MasterName from tblMasterType x; ";

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
                string sql = "select x.MasterId as ItemId, x.ItemName from tblMaster x where x.MasterType= @TypeId and x.IsActive= 1; ";

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
                IEnumerable<SelectList> cmeetings = (await connection.QueryAsync(sql, new { @CountryCode = CountryCode })).Select(x => new SelectList { ItemCode = x.StateCode, ItemName = x.StateName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetEmployees(int type)
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = @"Select x.UserId,x.LastName +', '+ x.FirstName +', '+z.TypeName +' - ' +convert(varchar,x.UserId)   as EmpName from 
tblUser x inner join tblEmployee y on x.UserId=y.UserId 
inner join tblEmpType z on y.EmpType=z.TypeId
where y.EmpType=@EmpType;";
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


                string sqlqry = @"Select x.UserId,x.LastName +', '+ x.FirstName +', '+ z.TypeName +' - ' + CONVERT(varchar(10), x.UserId) as EmpName from  
			tblUser x inner join tblEmployee y Inner JOIN tblEmpType z on y.EmpType=z.TypeId
			on x.UserId=y.UserId where x.IsActive=@IsActive;";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry, new { @IsActive = (int)Status.Active })).Select(x => new ItemList { ItemId = x.UserId, ItemName = x.EmpName });
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
                string sqlqry = "select x.UserId,x.LastName +' '+ x.FirstName+'(' +  CONVERT(varchar(10), x.UserId) +')' as ClientName from  tblUser x inner join tblClient y on x.UserId=y.UserId where x.IsActive=1;";

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
                        @IsActive = (int)Status.Active,
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
                sres.Result = false;
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

                string sqlqry = "select * from tblTaskMaster Where IsActive=@IsActive";
                IEnumerable<TaskModel> objResult = (await connection.QueryAsync<TaskModel>(sqlqry, new { @IsActive = (int)Status.Active }));

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

        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetDiagnosisList()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {


                string sqlqry = "select * from tblDiagnosisMaster;";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sqlqry)).Select(x => new ItemList { ItemId = x.DxId, ItemName = x.Description });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }



        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetProvisionList(int ProvisionType)
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "Select * from tblProvisionsMaster Where ProvisionType=@ProvisionType; ";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sql, new { @ProvisionType = ProvisionType })).Select(x => new ItemList { ItemId = x.ProvisionId, ItemName = x.Title });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }






        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetPayers()
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select *  from tblPayer; ";
                IEnumerable<ItemList> cmeetings = (await connection.QueryAsync(sql)).Select(x => new ItemList { ItemId = x.PayerId, ItemName = x.PayerName });
                obj.Data = cmeetings;
                obj.Result = cmeetings.Any() ? true : false;
                obj.Message = cmeetings.Any() ? "Data Found." : "No Data found.";
            }
            return obj;
        }



        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetUsers(int type)
        {
            ServiceResponse<IEnumerable<ItemList>> obj = new ServiceResponse<IEnumerable<ItemList>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = "Select UserId,LastName +','+ FirstName  as UserName from tblUser where UserType=@UserType;";
                IEnumerable<ItemList> users = (await connection.QueryAsync(sqlqry, new { @UserType = type })).Select(x => new ItemList { ItemId = x.UserId, ItemName = x.UserName });
                obj.Data = users;
                obj.Result = users.Any() ? true : false;
                obj.Message = users.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }



        public async Task<ServiceResponse<ClientGeoProvisions>> GetUsersGeoProvision(int UserId)
        {
            ServiceResponse<ClientGeoProvisions> obj = new ServiceResponse<ClientGeoProvisions>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlqry = @"select x.UserId,y.Latitude,y.Longitude ,z.ProvisionId
                                 ,(isnull(FirstName,'')+' '+isnull(MiddleName,'')+' '+isnull(LastName,'')) as UserName
                               ,y.Address,y.ZipCode,y.FlatNo, y.City, y.Country ,y.State
                                from tblUser x 
                                inner join tblAddress y on x.UserId=y.UserId
                                Left join tblProvisions z on x.UserId=z.UserId 
                                where IsNULL(z.ProvisionValue,'True')='True'
                                and x.UserId=@UserId";
                var results = (await connection.QueryAsync(sqlqry, new { @UserId = UserId })).ToList();

                //Using Query Syntax
                var GroupByQS = (from p in results
                                 group p by new
                                 {
                                     p.UserId,
                                     p.Latitude
                                 ,
                                     p.Longitude,
                                     p.UserName
                                 ,
                                     p.Address,
                                     p.ZipCode,
                                     p.FlatNo,
                                     p.City,
                                     p.Country,
                                     p.State
                                 } into g
                                 orderby g.Key.UserId descending
                                 select new ClientGeoProvisions
                                 {
                                     UserName = g.Key.UserName,
                                     ClientId = g.Key.UserId,
                                     Latitude = g.Key.Latitude == null ? 0.0m : Convert.ToDecimal(g.Key.Latitude),
                                     Longitude = g.Key.Longitude == null ? 0.0m : Convert.ToDecimal(g.Key.Longitude),
                                     Provisions = g.Where(x => x.ProvisionId != null).Select(f => (int)f.ProvisionId).ToArray(),
                                     Address = g.Key.Address == null ? "" : g.Key.Address,
                                     City = g.Key.City == null ? "" : g.Key.City,
                                     State = g.Key.State == null ? "" : g.Key.State,
                                     Country = g.Key.Country == null ? "" : g.Key.Country,
                                     ZipCode = g.Key.ZipCode == null ? "" : g.Key.ZipCode,
                                     FlatNo = g.Key.FlatNo == null ? "" : g.Key.FlatNo,

                                 }).FirstOrDefault();

                obj.Data = GroupByQS;
                obj.Result = results.Any() ? true : false;
                obj.Message = results.Any() ? "Data Found." : "No Data found.";


            }
            return obj;

        }





        public async Task<ServiceResponse<IEnumerable<ItemList>>> GetCMPLCategoryList(int CategoryId, short UserTypeId)
        {
            ServiceResponse<IEnumerable<ItemList>> objItemList = new ServiceResponse<IEnumerable<ItemList>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = @"Select * from tblCategoryMaster Where IsNULL(ParentId,0)=@ParentId and IsActive=@IsActive and UserTypeId= @UserTypeId";
                    IEnumerable<ItemList> resObj = (await connection.QueryAsync(sqlQuery,
                         new { @ParentId = CategoryId, @IsActive = (int)Status.Active, @UserTypeId = UserTypeId })).Select(x => new ItemList { ItemId = x.CategoryId, ItemName = x.CategoryName });
                    objItemList.Data = resObj.ToList();
                    objItemList.Result = resObj.Any() ? true : false;
                    objItemList.Message = resObj.Any() ? "Data Found." : "No Data found.";
                }

                return objItemList;
            }
            catch (Exception ex)
            {
                objItemList.Message = ex.Message;
                return objItemList;
            }
        }


        
    }
}

