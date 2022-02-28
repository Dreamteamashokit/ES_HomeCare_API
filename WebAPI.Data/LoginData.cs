using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using WebAPI_SAMPLE.WebAPI.Data.IData;

namespace WebAPI_SAMPLE.WebAPI.Data
{
    public class LoginData : ILoginData
    {
        private IConfiguration configuration;
        public LoginData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<UserLogin>> ValidateUserLogin(string uname, string password)
        {
            ServiceResponse<UserLogin> sres = new ServiceResponse<UserLogin>();
            try
            {
                UserLogin _ud = new UserLogin();
                _ud.UName = uname;
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_VALIDATE_LOGIN", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UNAME", uname);
                    cmd.Parameters.AddWithValue("@PWD", password);

                    SqlParameter outputPara = new SqlParameter();
                    outputPara.ParameterName = "@LID";
                    outputPara.Direction = System.Data.ParameterDirection.Output;
                    outputPara.SqlDbType = System.Data.SqlDbType.VarChar;
                    outputPara.Size = 50;
                    cmd.Parameters.Add(outputPara);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    if (outputPara.Value != null & outputPara.Value.ToString().Length > 0)
                    {
                        _ud.LoginId = outputPara.Value.ToString();
                        sres.Result = true;
                        sres.Data = _ud;
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Data not found.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
            }
            finally
            {

            }

            return sres;
        }

    }
}
