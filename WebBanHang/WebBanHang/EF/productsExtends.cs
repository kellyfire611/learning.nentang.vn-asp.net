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
        /// <summary>
        /// Biến chứa chuỗi Kết nối đến Storage Azure
        /// </summary>
        public string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=azurestudentsdiag959;AccountKey=yYLvvu+cHBGK/sBLIvbYkVzE/VBS+z5Tw4K5komw2ybyvFYIV7ilKtYZnfa7rYQwUvTSrmFniX9QNrLyq9y3sQ==;EndpointSuffix=core.windows.net";

        /// <summary>
        /// Hàm lấy đường dẫn URL từ Storage Azure
        /// </summary>
        /// <returns>Image URL</returns>
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

        /// <summary>
        /// Hàm lấy đường dẫn từ Folder nằm trong dự án (ví dụ: folder /UploadedFiles/ProductImages/)
        /// </summary>
        /// <returns>Image URL</returns>
        public string GetImageUrlFromFolder()
        {
            return "/UploadedFiles/ProductImages/" + this.image;
        }
    }
}