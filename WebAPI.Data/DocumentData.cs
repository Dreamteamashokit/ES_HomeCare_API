
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

    public class DocumentData : IDocumentData
    {
        private IConfiguration configuration;
        public DocumentData(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        public async Task<ServiceResponse<string>> AddFolder(FolderModel model)
        {
            ServiceResponse<string> rObj = new ServiceResponse<string>();
            try
            {

                AmazonUploader uploader = new AmazonUploader(configuration);
                bool CheckFolderExits = uploader.RunFolderCreationDemo(model.FolderName);
                if (CheckFolderExits)
                {
                    using (var con = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                    {
                        string sqlQuery = @"BEGIN TRY
BEGIN TRANSACTION SCHEDUL
IF NOT EXISTS (SELECT * FROM tblFolderMaster where FolderName = @FolderName)
BEGIN
Insert Into tblFolderMaster (FolderName,CreatedOn,CreatedBy) 
Values (@FolderName,@CreatedOn,@CreatedBy);
Select @FolderId=SCOPE_IDENTITY();
Insert Into tblFolderUser (FolderId,UserId,CreatedOn,CreatedBy) 
Values (@FolderId,@UserId,@CreatedOn,@CreatedBy);
END  
            

ELSE 
BEGIN 
SELECT @FolderId=FolderId FROM tblFolderMaster where FolderName = @FolderName
Insert Into tblFolderUser (FolderId,UserId,CreatedOn,CreatedBy) 
Values (@FolderId,@UserId,@CreatedOn,@CreatedBy);

END

COMMIT TRANSACTION SCHEDUL

END TRY

BEGIN CATCH 
IF (@@TRANCOUNT > 0)
BEGIN
ROLLBACK TRANSACTION SCHEDUL
PRINT ''
END 
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage
END CATCH
";
                        int rowsAffected = await con.ExecuteAsync(sqlQuery, new
                        {
                            flag = 1,
                            FolderId = model.FolderId,
                            FolderName = model.FolderName,
                            UserId = model.UserId,
                            IsActive = model.IsActive,
                            CreatedOn = DateTime.Now,
                            CreatedBy = model.CreatedBy
                        });

                        if (rowsAffected > 0)
                        {

                            rObj.Result = true;
                            rObj.Data = model.FolderId.ToString();
                            rObj.Message = " Data Save Sucessfully";
                        }
                        else
                        {
                            rObj.Data = null;
                            rObj.Result = false;
                            rObj.Message = "Data not save";
                        }
                    }
                }
                else
                {
                    rObj.Data = null;
                    rObj.Result = false;
                    rObj.Message = "Data not save";

                }

            }
            catch (Exception ex)
            {

                rObj.Data = null;
                rObj.Result = false;
                rObj.Message = ex.Message;

            }


            return rObj;

        }

        public async Task<ServiceResponse<IEnumerable<UploadFileRecord>>> GetFolderlist(int UserId)
        {
            ServiceResponse<IEnumerable<UploadFileRecord>> obj = new ServiceResponse<IEnumerable<UploadFileRecord>>();
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = @"Select x.*,y.UserId From tblFoldermaster x Inner Join tblFolderUser y on x.FolderId=y.FolderId Where y.UserId=@UserId;";
                IEnumerable<UploadFileRecord> result = (await connection.QueryAsync<UploadFileRecord>(sql, new { @UserId = UserId }));
                obj.Data = result;
                obj.Result = result.Any() ? true : false;
                obj.Message = result.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }

        public async Task<ServiceResponse<string>> DeleteFolder(long FolderId, int UserId)
        {
            ServiceResponse<string> rObj = new ServiceResponse<string>();
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();
                    string _query = @"delete from tblFolderUser  Where FolderId=@FolderId and UserId=@UserId;";

                    int rowsAffected = await cnn.ExecuteAsync(_query, new { @FolderId = FolderId, @UserId = UserId }, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        rObj.Result = true;
                        rObj.Data = "Record Deleted";
                    }
                    else
                    {
                        rObj.Result = false;
                        rObj.Data = null;
                        rObj.Message = "Failed Deletion";
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                rObj.Result = false;
                rObj.Data = null;
                rObj.Message = ex.Message;
                return rObj;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }
            return rObj;
        }



        public async Task<ServiceResponse<string>> AddDocument(UploadFileFolder model)
        {
            ServiceResponse<string> sres = new ServiceResponse<string>();

            using (IDbConnection db = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sqlQuery = @"BEGIN
BEGIN TRANSACTION UploadDoc
declare @UserTypeId int=NULL,@DocumentId bigInt =null
Insert Into tblEmpDocument (FolderId,FileName,FilePath,Title,SeachTag,Description,UserId,CreatedOn,CreatedBy) 
Values (@FolderId,@FileName,@FilePath,@Title,@SeachTag,@Description,@UserId,@CreatedOn,@CreatedBy)


Select @DocumentId=SCOPE_IDENTITY();
select @UserTypeId=UserType from tblUser where UserId=@UserId
UPDATE  xy SET xy.CompletedOn = GetDate(), xy.IsStatus=@IsStatus,xy.DocumentId=@DocumentId,xy.IsCompleted=1
from tblCompliance xy 
inner join  tblCategoryMaster x on xy.CodeId=x.CategoryId
inner join tblFolderMaster y on x.CategoryName=y.FolderName 
inner join tblFolderUser z on y.FolderId= z.FolderId and z.UserId =xy.UserId
Where  z.UserId=@UserId and x.UserTypeId=@UserTypeId and xy.IsStatus!=@IsStatus

COMMIT TRANSACTION UploadDoc
END ";
                
                int rowsAffected = await db.ExecuteAsync(sqlQuery, new
                {
                    FolderId = model.FolderId,
                    FileName = model.FileName,
                    FilePath = string.Empty,
                    Title = model.Title,
                    SeachTag = model.Search,
                    Description = model.Description,
                    UserId = model.UserId,
                    CreatedBy = model.CreatedBy,
                    IsStatus=(int)ComplianceStatusEnum.Completed,          
                    CreatedOn = DateTime.Now
                });

                if (rowsAffected > 0)
                {

                    sres.Message = "Data Save Sucessfully";
                }
                else
                {
                    sres.Data = null;
                    sres.Message = "Data not save";
                }
            }

            return sres;

        }

        public async Task<ServiceResponse<IEnumerable<FolderView>>> GetDocumentlist(int UserId)
        {
            ServiceResponse<IEnumerable<FolderView>> obj = new ServiceResponse<IEnumerable<FolderView>>();
            try
            {
                using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    string sql = @"Select x.FolderId,x.FolderName,p.UserId,y.DocumentId,y.FileName,y.FilePath,y.Title,y.Description,y.SeachTag, 
y.CreatedOn,e.FirstName + ' ' + ISNULL(e.MiddleName,'') + ' ' + e.LastName as CreatedByName from  tblFoldermaster x 
Inner join tblFolderUser p On x.FolderId=p.FolderId 
left join tblEmpDocument y on p.FolderId = y.FolderId left join tblUser e on y.CreatedBy=e.UserId
where p.UserId = @UserId;";

                    var result = (await connection.QueryAsync(sql, new { @UserId = UserId }));
                    var GroupByQS = from doc in result
                                    group doc by new { doc.FolderId, doc.FolderName, } into docGrp
                                    orderby docGrp.Key.FolderId descending
                                    select new FolderView
                                    {
                                        FolderId = docGrp.Key.FolderId,
                                        FolderName = docGrp.Key.FolderName,
                                        DocumentList = docGrp.Where(x => x.DocumentId != null).Select(x => new DocumentView
                                        {
                                            DocumentId = x.DocumentId == null ? 0 : x.DocumentId,
                                            Title = x.Title == null ? string.Empty : x.Title,
                                            SearchTag = x.SearchTag == null ? string.Empty : x.SearchTag,
                                            Description = x.Description == null ? string.Empty : x.Description,
                                            FileName = x.FileName == null ? string.Empty : x.FileName,
                                            FilePath = x.FilePath == null ? string.Empty : x.FilePath,
                                            CreatedByName = x.CreatedByName == null ? "admin" : x.CreatedByName,
                                            CreatedOn = x.CreatedOn,
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

        public async Task<ServiceResponse<string>> DeleteDocument(long DocumentId)
        {
            ServiceResponse<string> rObj = new ServiceResponse<string>();
            IDbTransaction transaction = null;
            try
            {
                using (IDbConnection cnn = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
                {
                    if (cnn.State != ConnectionState.Open)
                        cnn.Open();
                    transaction = cnn.BeginTransaction();
                    string _query = @"delete from tblEmpDocument where DocumentId=@DocumentId";

                    int rowsAffected = await cnn.ExecuteAsync(_query, new { @DocumentId = DocumentId }, transaction);
                    transaction.Commit();

                    if (rowsAffected > 0)
                    {
                        rObj.Result = true;
                        rObj.Data = "Record Deleted";
                    }
                    else
                    {
                        rObj.Data = null;
                        rObj.Message = "Failed Deletion";
                    }
                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                rObj.Message = ex.Message;
                return rObj;
            }
            finally
            {
                if (transaction != null)
                    transaction.Dispose();
            }
            return rObj;
        }


    }
}
