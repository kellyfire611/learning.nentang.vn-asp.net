# learning.nentang.vn-asp.net
Học ASP.NET - Tạo Trang Web Thương mại điện tử Bán hàng | nentang.vn

# Azure
## Hướng dẫn upload file lên Storage Azure
https://social.technet.microsoft.com/wiki/contents/articles/51791.azure-storage-c-create-container-upload-and-download-blobs.aspx

## Hướng dẫn upload / download / delete file trên Storage Azure
https://www.c-sharpcorner.com/article/upload-download-and-delete-blob-files-in-azure-storage/

## Code lấy đường dẫn URL của BLOB trên Azure
```
static void BlobUrl()
{
    var account = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);
    var cloudBlobClient = account.CreateCloudBlobClient();
    var container = cloudBlobClient.GetContainerReference("container-name");
    var blob = container.GetBlockBlobReference("image.png");
    blob.UploadFromFile("File Path ....");//Upload file....
    var blobUrl = blob.Uri.AbsoluteUri;
}
```
