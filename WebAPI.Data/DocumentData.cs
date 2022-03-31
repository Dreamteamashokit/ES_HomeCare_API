
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
            using (var connection = new SqlConnection(configuration.GetConnectionString("DBConnectionString").ToString()))
            {
                string sql = "select x.FolderId,x.FolderName,y.Documentid,y.FileName,y.FilePath,y.Title,y.Description,y.SeachTag from " +
                    "tblFoldermaster x left join tblEmpDocument y on x.FolderId = y.FolderId where EmpId = @EmpId ;";

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
                                        DocumentId = x.Documentid,
                                        Title = x.Title,
                                        SearchTag = x.SearchTag,
                                        Description = x.Description,
                                        FileName = x.FileName,
                                        FilePath = x.FilePath,
                                    }).ToList()
                                };

                obj.Data = GroupByQS;
                obj.Result = GroupByQS.Any() ? true : false;
                obj.Message = GroupByQS.Any() ? "Data Found." : "No Data found.";
            }
            return obj;

        }


    }
}
