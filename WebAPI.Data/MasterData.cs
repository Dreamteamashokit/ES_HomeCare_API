using Dapper;
using ES_HomeCare_API.Model;
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

        public async Task<ServiceResponse<string>> ActiveTask(int TaskId, int Status)
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
                        @IsActive = Status,
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

        public async Task<ServiceResponse<string>> AddCMPLCategory(CategoryModel _model)
        {
            ServiceResponse<string> addCategoryResponse = new ServiceResponse<string>();
            try
            {
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery;
                    if (_model.CategoryId == 0)
                    {

                        sqlQuery = sqlQuery = @"INSERT INTO tblCategoryMaster(CategoryName,ParentId,IsActive,CreatedOn,CreatedBy)
VALUES(@CategoryName,@ParentId,@IsActive,@CreatedOn,@CreatedBy)";

                    }
                    else
                    {
                        sqlQuery = @"Update tblCategoryMaster set CategoryName=@CategoryName,ParentId=@ParentId where CategoryId=@CategoryId;";
                    }
               
                    int rowsAffected = await db.ExecuteAsync(sqlQuery, new
                    {
                        @CategoryId = _model.CategoryId,
                        @CategoryName = _model.CategoryName,
                        @ParentId = _model.ParentId,
                        @IsActive = (int)Status.Active,
                        @CreatedOn = _model.CreatedOn,
                        @CreatedBy = _model.CreatedBy
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

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetCMPLCategoryList()
        {
            ServiceResponse<IEnumerable<CategoryModel>> objCategoryListResponse = new ServiceResponse<IEnumerable<CategoryModel>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = @"SELECT category.CategoryId, category.CategoryName, ISNULL(category.ParentId,0) as ParentId , ISNULL(parent.CategoryName,'RootCategory') as ParentName 
from tblCategoryMaster category LEFT JOIN tblCategoryMaster as parent ON category.ParentId = parent.CategoryId Where IsActive=@IsActive;";
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

        public async Task<ServiceResponse<IEnumerable<CategoryModel>>> GetCMPLCategoryList(int CategoryId)
        {
            ServiceResponse<IEnumerable<CategoryModel>> objCategoryListResponse = new ServiceResponse<IEnumerable<CategoryModel>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = @"Select * from tblCategoryMaster Where IsNULL(ParentId,0)=@ParentId and IsActive=@IsActive";
                    IEnumerable<CategoryModel> resObj = await connection.QueryAsync<CategoryModel>(sqlQuery,
                         new { @ParentId = CategoryId, @IsActive = (int)Status.Active, });
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

        public async Task<ServiceResponse<string>> DelCMPLCategory(int CategoryId)
        {
            ServiceResponse<string> obj = new ServiceResponse<string>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    var sqlQuery = "Update tblCategoryMaster Set IsActive=@IsActive where ProviderId=@ContactId;";
                    int rowsAffected = await connection.ExecuteAsync(sqlQuery, new
                    {
                        @CategoryId = CategoryId,
                        @IsActive = (int)Status.InActive,

                    });

                    if (rowsAffected > 0)
                    {
                        obj.Result = true;
                        obj.Data = "Delete Successfully";
                    }
                    else
                    {
                        obj.Result = false;
                        obj.Data = null;
                        obj.Message = "Delete Failed.";
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
            
    
    }
}
