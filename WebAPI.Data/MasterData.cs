using Dapper;
using ES_HomeCare_API.Model.Client;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model.Master;
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
    public class MasterData : IMasterData
    {
        private IConfiguration configuration;
        public MasterData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<ServiceResponse<string>> CreateDiagnosis(DiagnosisItem _model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string Query = "INSERT INTO tblDiagnosisMaster(DxCodes, Description, IsActive, CreatedBy, CreatedOn) VALUES(@DxCodes, @Description, @IsActive, @CreatedBy, @CreatedOn);";


                    int rowsAffected = cnn.Execute(Query, _model);

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

        public async Task<ServiceResponse<IEnumerable<DiagnosisItem>>> GetDiagnosis()
        {
            ServiceResponse<IEnumerable<DiagnosisItem>> obj = new ServiceResponse<IEnumerable<DiagnosisItem>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {

                string sqlqry = "select * from tblDiagnosisMaster; ";
                IEnumerable<DiagnosisItem> objResult = (await connection.QueryAsync<DiagnosisItem>(sqlqry));

                obj.Data = objResult;
                obj.Result = objResult != null ? true : false;
                obj.Message = objResult != null ? "Data Found." : "No Data found.";
            }
            return obj;
        }

        public async Task<ServiceResponse<string>> UpdateTask(TaskModel item)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblTaskMaster SET TaskCode = @TaskCode, TaskName = @TaskName Where TaskId = @TaskId";
                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new
                    {
                        @TaskCode = item.TaskCode,
                        @TaskName = item.TaskName,
                        @TaskId = item.TaskId,
                    });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
                    }

                }

            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }


            return obj;
        }

        public async Task<ServiceResponse<string>> ActiveTask(int TaskId,int Status)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlqry = "Update tblTaskMaster SET IsActive = @IsActive Where TaskId = @TaskId";

                    int rowsAffected = await connection.ExecuteAsync(sqlqry, new
                    {
                        @TaskId = TaskId,
                        @IsActive=Status,
                    });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Updated Successfully";
                    }
                    else
                    {
                        obj.Data = null;
                        obj.Message = "Updation Failed.";
                    }

                }

            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
                return obj;
            }


            return obj;
        }

        public async Task<ServiceResponse<string>> AddCategory(CategoryModel _model)
        {
            ServiceResponse<string> addCategoryResponse = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblCategoryMaster (CategoryName, ParentCategoryId) Values(@CategoryName, @ParentCategoryId)";

                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        CategoryName = _model.CategoryName,
                        ParentCategoryId = _model.ParentCategoryId
                    });

                    if (rowsAffected > 0)
                    {
                        addCategoryResponse.Result = true;
                        addCategoryResponse.Data = "Sucessfully Created.";
                    }
                    else
                    {
                        addCategoryResponse.Data = null;
                        addCategoryResponse.Message = "Failed new creation.";
                    }
                }
            }
            catch (Exception ex)
            {
                addCategoryResponse.Message = ex.Message;
            }

            return addCategoryResponse;
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetParentCategoryList()
        {
            ServiceResponse<IEnumerable<CategoryModel>> objCategoryListResponse = new ServiceResponse<IEnumerable<CategoryModel>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "SELECT CategoryId, CategoryName from tblCategoryMaster " +
                        " where ParentCategoryId = null or ParentCategoryId = 0;";
                    IEnumerable<CategoryModel> resObj = await connection.QueryAsync<CategoryModel>(sqlQuery);

                    objCategoryListResponse.Data = resObj.ToList();
                    objCategoryListResponse.Result = resObj.Any() ? true : false;
                    objCategoryListResponse.Message = resObj.Any() ? "Data Found." : "No Data found.";

                }
                return objCategoryListResponse;
            }
            catch (Exception ex)
            {
                objCategoryListResponse.Message = ex.Message;
                return objCategoryListResponse;
            }
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetMasterCategoryList()
        {
            ServiceResponse<IEnumerable<CategoryModel>> objCategoryListResponse = new ServiceResponse<IEnumerable<CategoryModel>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "SELECT category.CategoryId, category.CategoryName, category.ParentCategoryId, parent.CategoryName as ParentCategoryName " +
                        "from tblCategoryMaster category LEFT JOIN tblCategoryMaster as parent ON category.ParentCategoryId = parent.CategoryId;";
                    IEnumerable<CategoryModel> resObj = await connection.QueryAsync<CategoryModel>(sqlQuery);

                    objCategoryListResponse.Data = resObj.ToList();
                    objCategoryListResponse.Result = resObj.Any() ? true : false;
                    objCategoryListResponse.Message = resObj.Any() ? "Data Found." : "No Data found.";
                }
                return objCategoryListResponse;
            }
            catch (Exception ex)
            {
                objCategoryListResponse.Message = ex.Message;
                return objCategoryListResponse;
            }
        }

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetSubCategoryList(int categoryId)
        {
            ServiceResponse<IEnumerable<CategoryModel>> objCategoryListResponse = new ServiceResponse<IEnumerable<CategoryModel>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "SELECT category.CategoryId, category.CategoryName, category.ParentCategoryId, parent.CategoryName as ParentCategoryName from tblCategoryMaster category " +
                        " LEFT JOIN tblCategoryMaster as parent ON category.ParentCategoryId = parent.CategoryId and category.ParentCategoryId = @ParentCategoryId;";
                    IEnumerable<CategoryModel> resObj = await connection.QueryAsync<CategoryModel>(sqlQuery,
                         new { @ParentCategoryId = categoryId });

                    objCategoryListResponse.Data = resObj.ToList();
                    objCategoryListResponse.Result = resObj.Any() ? true : false;
                    objCategoryListResponse.Message = resObj.Any() ? "Data Found." : "No Data found.";
                }
                return objCategoryListResponse;
            }
            catch (Exception ex)
            {
                objCategoryListResponse.Message = ex.Message;
                return objCategoryListResponse;
            }
        }
    }
}
