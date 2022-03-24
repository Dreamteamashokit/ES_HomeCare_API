using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Helper
{
    public class AmazonUploader
    {
        public readonly string AccessKey = string.Empty;
        public readonly string SceretKey = string.Empty;
        public readonly string BucketName = string.Empty;
        private IConfiguration configuration;
        public AmazonUploader(IConfiguration _configuration)
        {
            configuration = _configuration;
            AccessKey = configuration.GetConnectionString("AWSAccessKey").ToString();
            SceretKey = configuration.GetConnectionString("AWSsceretKey").ToString();
            BucketName = configuration.GetConnectionString("AWSBucketName").ToString();
        }

        //public  string AccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        //public  string SceretKey = ConfigurationManager.AppSettings["AWSsceretKey"];
        //public string BucketName = ConfigurationManager.AppSettings["AWSBucketName"];
        public void sendMyFileToS3(System.IO.Stream localFile, string fileNameInS3)
        {

            try
            {

                IAmazonS3 client = new AmazonS3Client(AccessKey, SceretKey, RegionEndpoint.USEast1);

                RunFolderCreationDemo(client, "Navneet");
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                request.BucketName = BucketName;

                request.Key = fileNameInS3;
                request.InputStream = localFile;
                request.CannedACL = S3CannedACL.PublicReadWrite;
                utility.Upload(request);

                //bool status = CheckFile(client, fileNameInS3, sourceFilePath);
                //return status;
            }
            catch (System.Exception ex)
            {

                // return false;
            }


        }



        public void RunFolderCreationDemo(IAmazonS3 s3Client, string FolderName)
        {
            try
            {

                PutObjectRequest folderRequest = new PutObjectRequest();
                String delimiter = "";
                folderRequest.BucketName = BucketName;
                String folderKey = string.Concat(FolderName, delimiter);
                folderRequest.Key = folderKey;
                folderRequest.InputStream = new MemoryStream(new byte[0]);
                Task<PutObjectResponse> folderResponse = s3Client.PutObjectAsync(folderRequest);
            }
            catch (AmazonS3Exception e)
            {

            }
        }
    }
}
