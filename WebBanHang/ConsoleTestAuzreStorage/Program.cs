using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Add The following Namespaces to the project
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob; // Multipart

namespace ConsoleTestAuzreStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Reference to Azure Storage Account
            String strorageconn = System.Configuration.ConfigurationSettings.AppSettings.Get("StorageConnectionString");
            CloudStorageAccount storageacc = CloudStorageAccount.Parse(strorageconn);

            //Create Reference to Azure Blob
            CloudBlobClient blobClient = storageacc.CreateCloudBlobClient();

            //The next 2 lines create if not exists a container named "democontainer"
            CloudBlobContainer container = blobClient.GetContainerReference("webbanhang_shopA");
            container.CreateIfNotExists();

            //The next 7 lines upload the file test.txt with the name DemoBlob on the container "democontainer"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("20200103211130_testAzure.jpg");
            using (var filestream = System.IO.File.OpenRead(@"D:\dnpcuong\testAzure.jpg"))
            {
                blockBlob.UploadFromStream(filestream);
            }

            Console.Read();
        }
    }
}
