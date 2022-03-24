using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Helper
{
    public class AmazonUploader
    {
        public void sendMyFileToS3(System.IO.Stream localFilePath, string bucketName, string fileNameInS3)
        {

            try
            {

                IAmazonS3 client = new AmazonS3Client("AKIAVSTZJGNSR2UPYM6F", "d6jG9K7ZOIyycQzNX31V08EzXn8P4AvFIyx5AsWa", RegionEndpoint.USEast1);

                RunFolderCreationDemo(client, "Navneet");
                TransferUtility utility = new TransferUtility(client);
                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
                request.BucketName = bucketName;

                request.Key = fileNameInS3;
                request.InputStream = localFilePath;
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
                String delimiter = "/";
                folderRequest.BucketName = "eshomecarewebapp";
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
