using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;

namespace Dow.SSD.Framework.Common
{
    public class AzureBlobServiceOpViaSASHelper
    {
        public string signature = ConfigurationManager.AppSettings["SharedAccessSignature"].ToString();
        public string storageAccountName = ConfigurationManager.AppSettings["StorageAccountName"].ToString();
        public string blobContainerName = ConfigurationManager.AppSettings["StorageBlobContainerName"].ToString();
        public string sasBlobUri = string.Empty;
        public CloudBlobContainer blobContainer;

        public AzureBlobServiceOpViaSASHelper()
        {
            sasBlobUri = string.Format("https://{0}.blob.core.windows.net/{1}{2}", storageAccountName, blobContainerName, signature);
            blobContainer = new CloudBlobContainer(new Uri(sasBlobUri));
            blobContainer.CreateIfNotExists();
        }

        public void UploadBlobToContainer(string blobName, string blobFullPath)
        {
            // If blobName is NULL, get the file name from full path as blob name
            if (string.IsNullOrEmpty(blobName))
            {
                blobName = blobFullPath.Substring(blobFullPath.LastIndexOf("\\") + 1);
            }

            // Retrieve reference to the blobName.
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            // Create or overwrite the blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(blobFullPath))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

        public void DeleteBlobFromContainer(string blobName)
        {
            // Retrieve reference to the blobName.
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            blockBlob.Delete();
        }

        public void DownloadBlobFromContainer(string blobName, string downloadPath)
        {
            // Retrieve reference to the blobName.
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            using (var fileStream = System.IO.File.OpenWrite(downloadPath))
            {
                blockBlob.DownloadToStream(fileStream);
            }
        }

        public IEnumerable<IListBlobItem> ListAllBlobInContainer()
        {
            return blobContainer.ListBlobs(null, true);
        }
    }
}
