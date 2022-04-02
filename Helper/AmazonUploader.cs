using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Net;
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

       
        public void sendMyFileToS3(System.IO.Stream localFile, string fileNameInS3)
        {

            try
            {

                IAmazonS3 client = new AmazonS3Client(AccessKey, SceretKey, RegionEndpoint.USEast1);

               
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



        public bool RunFolderCreationDemo(string FolderName)
        {
            try
            {
                IAmazonS3 client = new AmazonS3Client(AccessKey, SceretKey, RegionEndpoint.USEast1);

                PutObjectRequest folderRequest = new PutObjectRequest();
                String delimiter = "/";
                folderRequest.BucketName = BucketName;
                String folderKey = string.Concat(FolderName, delimiter);
                folderRequest.Key = folderKey;
                folderRequest.InputStream = new MemoryStream(new byte[0]);
                Task<PutObjectResponse> folderResponse = client.PutObjectAsync(folderRequest);
                return true;
            }
            catch (AmazonS3Exception ec)
            {
                return false;
            }
        }


        //bool CheckFile(IAmazonS3 client, string FileName)
        //{


        //    GetObjectRequest request = new GetObjectRequest();
        //    request.BucketName = BucketName;
        //    request.Key = FileName;
        //    GetObjectResponse response = client.GetObject(request);

        //    if (response.ContentLength == 0 && string.IsNullOrEmpty(response.ETag))
        //    {
        //        return false;
        //    }
        //    return true;


        //}


        public async Task<byte[]> DownloadFileAsync(string file)
        {
            MemoryStream ms = null;

            try
            {
                IAmazonS3 client = new AmazonS3Client(AccessKey, SceretKey, RegionEndpoint.USEast1);
                GetObjectRequest getObjectRequest = new GetObjectRequest
                {
                    BucketName = BucketName,
                    Key = file
                };

                using (var response = await client.GetObjectAsync(getObjectRequest))
                {
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        using (ms = new MemoryStream())
                        {
                            await response.ResponseStream.CopyToAsync(ms);
                        }
                    }
                }

                if (ms is null || ms.ToArray().Length < 1)
                    throw new FileNotFoundException(string.Format("The document '{0}' is not found", file));

                return ms.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
