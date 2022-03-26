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
using ES_HomeCare_API.Helper;
using System;

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

        public async Task<ServiceResponse<string>> SaveFolder(FolderData model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            AmazonUploader uploader = new AmazonUploader(configuration);
            bool CheckFolderExits = uploader.RunFolderCreationDemo(model.FolderName);
            if (CheckFolderExits==true)
            {
               
                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblFolderMaster (EmpId,FolderName,CreatedOn,CreateBy) Values (@EmpId,@FolderName,@CreatedOn,@CreatedBy);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        EmpId = model.EmpId,
                        FolderName = model.FolderName,
                        CreatedOn = DateTime.Now,
                        CreatedBy = model.CreatedBy
                    });

                    if (rowsAffected > 0)
                    {
                        sres.Data = model.FolderName;
                        sres.Message = " Data Save Sucessfully";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Data not save";
                    }
                }
               
            }
            else if (CheckFolderExits==false)
            {

                sres.Data = null;
                sres.Message = "Folder not Created.";
            }
            return sres;

        }



        public async Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int EmpId)
        {
            ServiceResponse<IEnumerable<UploadFileRecord>> obj = new ServiceResponse<IEnumerable<UploadFileRecord>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select FolderId,FolderName,'' as FileName from tblFoldermaster where EmpId= @EmpId; ";

                IEnumerable<UploadFileRecord> resData = (await connection.QueryAsync<UploadFileRecord>(sql,
             new { @EmpId = EmpId }));

                obj.Data = resData;
                obj.Result = resData.Any() ? true : false;
                obj.Message = resData.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<string>> Savefile(UploadFileFolder model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
           
          

                using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sqlQuery = "Insert Into tblEmpDocument (FolderId,FileName,FilePath,Title,SeachTag,Description,CreateOn,CreatedOn) Values (@FolderId,@FileName,@FilePath,@Title,@SeachTag,@Description,@CreateOn,@CreatedOn);";
                    int rowsAffected = db.Execute(sqlQuery, new
                    {
                        FolderId = model.folderid,
                        FileName = model.filename,
                        FilePath =string.Empty,
                        Title = model.Title,
                        SeachTag=model.Search,
                        Description=model.Description,
                        CreateOn=model.CreatedBy,
                        CreatedOn=DateTime.Now
                    });

                    if (rowsAffected > 0)
                    {
                      
                        sres.Message = " Data Save Sucessfully";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Data not save";
                    }
                }

            
          
            return sres;

        }



       



    }
}
