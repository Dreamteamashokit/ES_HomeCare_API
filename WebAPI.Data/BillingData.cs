using Dapper;
using ES_HomeCare_API.Model;
using ES_HomeCare_API.Model.Billing;
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
    public class BillingData : IBillingData
    {

        private IConfiguration configuration;
        public BillingData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> AddPayer(PayerModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();



                    string sqlQuery = @"INSERT INTO tblPayer
(PayerName,BillToName,Email,Phone,Fax,NPI,FedId,ETIN,Taxonomy,MedicaidId,IsActive,CreatedOn,CreatedBy)
VALUES
(@PayerName,@BillToName,@Email,@Phone,@Fax,@NPI,@FedId,@ETIN,@Taxonomy,@MedicaidId,@IsActive,@CreatedOn,@CreatedBy)";


                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected.ToString();
                    }
                    else
                    {
                        sres.Data = rowsAffected.ToString();
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }
            return sres;
        }

        public async Task<ServiceResponse<string>> UpdatePayer(PayerModel _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();



                    string sqlQuery = @"UPDATE tblPayer SET PayerName=@PayerName,BillToName=@BillToName,Email=@Email,Phone=@Phone,Fax=@Fax,NPI=@NPI,FedId=@FedId,ETIN=@ETIN,
Taxonomy=@Taxonomy,MedicaidId=@MedicaidId,IsActive=@IsActive,CreatedOn=@CreatedOn,CreatedBy=@CreatedBy
WHERE PayerId=@PayerId";


                    int rowsAffected = await cnn.ExecuteAsync(sqlQuery, _model, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        sres.Result = true;
                        sres.Data = rowsAffected.ToString();
                    }
                    else
                    {
                        sres.Data = rowsAffected.ToString();
                        sres.Message = "Failed new creation.";
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }
            return sres;
        }

        public async Task<ServiceResponse<IEnumerable<PayerModel>>> GetPayerList()
        {
            ServiceResponse<IEnumerable<PayerModel>> obj = new ServiceResponse<IEnumerable<PayerModel>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select * from tblPayer Where IsActive = @IsActive";
                IEnumerable<PayerModel> dataResult = (await connection.QueryAsync<PayerModel>(sql,
                         new { @IsActive = (int)Status.Active }));
                obj.Data = dataResult;
                obj.Result = dataResult.Any() ? true : false;
                obj.Message = dataResult.Any() ? "Data Found." : "No Data found.";

            }
            return obj;

        }

        public async Task<ServiceResponse<string>> DelPayer(int PayerId)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlQuery = "Update tblPayer Set IsActive=@IsActive where PayerId=@PayerId;";
                    var modeMapping = new
                    {
                        @PayerId = PayerId,
                        @IsActive = (int)Status.InActive,
                    };
                    int rowsAffected = await connection.ExecuteAsync(sqlQuery, modeMapping);
                    if (rowsAffected > 0)
                    {
                        result.Result = true;
                        result.Data = "Sucessfully  Updated.";
                    }
                    else
                    {
                        result.Data = null;
                        result.Message = "Failed to Update.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Data = null;
                result.Message = ex.Message;
            }
            return result;
        }




        public async Task<ServiceResponse<BillingSummaryInfoModel>> GetBillingSummaryInfo(int userId)
        {
            ServiceResponse<BillingSummaryInfoModel> obj = new ServiceResponse<BillingSummaryInfoModel>();
            using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                SqlCommand cmd = new SqlCommand("GetBillingSummaryInfo", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(table);
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        obj.Data = new BillingSummaryInfoModel
                        {
                            BillingId = Convert.ToInt32(table.Rows[i]["BillingId"]),
                            PayerId = Convert.ToInt32(table.Rows[i]["PayerId"]),
                            Type = table.Rows[i]["Type"] == null ? "" : table.Rows[i]["Type"].ToString(),
                            DueDate = string.IsNullOrWhiteSpace(table.Rows[i]["DueDate"].ToString()) ? "" : table.Rows[i]["DueDate"].ToString(),
                            BillToName = string.IsNullOrWhiteSpace(table.Rows[i]["BillToName"].ToString()) ? "" : table.Rows[i]["BillToName"].ToString()
                        };
                        obj.Result = true;
                        obj.Message = "Billing details retrive successfull.";
                    }
                }
                else
                {
                    obj.Result = true;
                    obj.Message = "Billing details does not exists.";
                }
                return obj;
            }
        }
    }

}
