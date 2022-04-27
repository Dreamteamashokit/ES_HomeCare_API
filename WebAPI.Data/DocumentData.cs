
using Dapper;
using ES_HomeCare_API.Model.Employee;
using ES_HomeCare_API.Model.Document;
using ES_HomeCare_API.WebAPI.Data.IData;

using ES_HomeCare_API.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_SAMPLE.Model;
using ES_HomeCare_API.Model.Common;
using ES_HomeCare_API.Model;

namespace ES_HomeCare_API.WebAPI.Data
{

    public class DocumentData: IDocumentData
    {
        private IConfiguration configuration;
        public DocumentData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public async Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int empId)
        {
            ServiceResponse<IEnumerable<FolderView>> obj = new ServiceResponse<IEnumerable<FolderView>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sql = "Select x.FolderId,x.FolderName,y.Documentid,y.FileName,y.FilePath,y.Title,y.Description,y.SeachTag, y.createdon as CreatedOn, e.FirstName + ' ' + e.MiddleName + ' ' + e.LastName as CreatedByName from  tblFoldermaster x left join tblEmpDocument y on x.FolderId = y.FolderId left join tblUser e on y.CreateOn=e.UserId where x.EmpId = @EmpId and x.FolderName <> '';";

                    var result = (await connection.QueryAsync(sql, new { @EmpId = empId }));

                    //Using Query Syntax
                    var GroupByQS = from doc in result
                                    group doc by new { doc.FolderId, doc.FolderName, } into docGrp
                                    orderby docGrp.Key.FolderId descending
                                    select new FolderView
                                    {
                                        FolderId = docGrp.Key.FolderId,
                                        FolderName = docGrp.Key.FolderName,

                                        DocumentList = docGrp.Select(x => new DocumentView
                                        {
                                            DocumentId = x.Documentid==null?0:x.Documentid,
                                            Title = string.IsNullOrEmpty(x.Title) ? string.Empty : x.Title,
                                            SearchTag = string.IsNullOrEmpty(x.SearchTag) ? string.Empty : x.SearchTag,
                                            Description = string.IsNullOrEmpty(x.Description) ? string.Empty : x.Description,
                                            FileName = string.IsNullOrEmpty(x.FileName) ? string.Empty : x.FileName,
                                            FilePath = string.IsNullOrEmpty(x.FilePath) ? string.Empty : x.FilePath,
                                            CreatedByName = string.IsNullOrEmpty(x.CreatedByName) ? "admin" : "",
                                            CreatedOn = string.IsNullOrEmpty(x.CreatedOn) ?string.Empty: x.CreatedOn
                                        }).ToList()
                                    };

                    obj.Data = GroupByQS;
                    obj.Result = GroupByQS.Any() ? true : false;
                    obj.Message = GroupByQS.Any() ? "Data Found." : "No Data found.";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return obj;

        }


        public async Task<ServiceResponse<string>> SaveFolder(FolderData model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            AmazonUploader uploader = new AmazonUploader(configuration);
            bool CheckFolderExits = uploader.RunFolderCreationDemo(model.FolderName);
            if (CheckFolderExits == true)
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
            else if (CheckFolderExits == false)
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
                string sql = "select tbl.FolderId,FolderName,FileName from tblFoldermaster tbl left join tblEmpDocument ted on tbl.FolderId = ted.FolderId where EmpId = @EmpId and FolderName<>'';";

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
                    FolderId = model.FolderId,
                    FileName = model.FileName,
                    FilePath = string.Empty,
                    Title = model.Title,
                    SeachTag = model.Search,
                    Description = model.Description,
                    CreateOn = model.CreatedBy,
                    CreatedOn = DateTime.Now
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




        public async Task<ServiceResponse<string>> DeleteFile(DeleteItem item)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    SqlCommand cmd = new SqlCommand("SP_DocumentProc", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@EmpId", item.EmpId);                   

                    cmd.Parameters.AddWithValue("@EmpId", item.EmpId);

                    cmd.Parameters.AddWithValue("@DocumentId", item.DocumentId);
                    cmd.Parameters.AddWithValue("@FolderId", item.FolderId);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        sres.Result = true;
                        sres.Data = "Record Deleted";
                    }
                    else
                    {
                        sres.Data = null;
                        sres.Message = "Deletion Failed.";
                    }
                }

            }
            catch (Exception ex)
            {
                sres.Message = ex.Message;
                return sres;
            }
            finally
            {

            }

            return sres;
        }


    }
}
