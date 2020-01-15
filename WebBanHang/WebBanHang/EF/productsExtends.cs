using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Add The following Namespaces to the project
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob; // Multipart

namespace WebBanHang.EF
{
    public partial class products
    {
        public string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=kellyfire611;AccountKey=1231321231231321313;EndpointSuffix=core.windows.net";

        public string GetImageUrlFromAzureStore()
        {
            string blobUrl = "";

            // Update file lên thư mục Storage Azure
            // Create Reference to Azure Storage Account
            String strorageconn = this.StorageConnectionString;
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("democontainer");

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(this.image); // hoahong.jpg
            blobUrl = blockBlob.Uri.AbsoluteUri; //https://azure.storage.com/kellyfire/democontainer/hoahong.jpg

            return blobUrl;
        }
    }
}