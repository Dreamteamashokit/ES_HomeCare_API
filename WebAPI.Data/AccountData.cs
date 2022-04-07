using Dapper;
using ES_HomeCare_API.Model.Account;
using ES_HomeCare_API.WebAPI.Data.IData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;

namespace ES_HomeCare_API.WebAPI.Data
{
    public class AccountData : IAccountData
    {
        private IConfiguration configuration;
        public AccountData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<UserModel>> LogInUser(LoginModel model)
        {
            ServiceResponse<UserModel> obj = new ServiceResponse<UserModel>();
            try
            {
                using (IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "select * from tblUser where UserName=@UserName and UserPassword=@UserPassword and IsActive=@IsActive";

                    var result = (await connection.QueryAsync(sqlQuery,
                        new
                        {
                            @UserName = model.UserName,
                            @UserPassword = model.Password,
                            @IsActive = model.IsActive
                        })).FirstOrDefault();

                    if (result.UserId > 0)
                    {

                        var resObj = new UserModel
                        {
                            UserId = result.UserId,

                            UserName = result.UserName,
                        };

                        string sqlstr = "insert Into tblLogin(UserId,LogIn) Values(@UserId,@LogIn) select SCOPE_IDENTITY();";
                        resObj.LoginInId = (long)(connection.Query<long>(sqlstr, new { @UserId = result.UserId, @LogIn = DateTime.Now }).First());

                        obj.Data = resObj;
                        obj.Result = obj.Data.LoginInId > 0 ? true : false;
                        obj.Message = obj.Data.LoginInId > 0 ? "Data Found." : "No Data found.";

                    }
                };

                return obj;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
        }


        public async Task<ServiceResponse<string>> LogOutUser(int userId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Update tblLogin Set LogOut=@LogOut Where UserId=@UserId; ";

                    int rowsAffected = cnn.Execute(sqlQuery, new { @LogOut = DateTime.Now, @UserId = userId });
                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Sucessfully  Created.";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Failed new creation.";
                    }

                }

            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }
            finally
            {

            }
            return obj;
        }



    }
}
