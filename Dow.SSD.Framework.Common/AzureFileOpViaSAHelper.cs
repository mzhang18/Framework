using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

/// <summary>
/// Azure Storage File Help Class. 
/// 
/// This class is used for Azure File service operation and connect to it via storage account/key.
/// 
/// The Azure storage account/key has been registered in Keyvault. 
/// </summary>

namespace Dow.SSD.Framework.Common
{
    public static class AzureFileOpViaSAHelper
    {
        static string storageFileShareName = ConfigurationManager.AppSettings["StorageFileShareName"].ToString();
        static int storageFileShareSize = int.Parse(ConfigurationManager.AppSettings["StorageFileShareSize"].ToString());
        static string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"].ToString();

        public static CloudFileDirectory BasicAzureFileOperations()
        {
            // Get Storage Account Key from Key Vault
            string saKey = EncryptionHelper.GetSAKey();

            // Retrieve storage account information from connection string
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(string.Format(storageConnectionString, saKey));

            // Create a file client for interacting with the file service.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();

            // Get the storage file share reference based on storage account
            CloudFileShare fileShare = fileClient.GetShareReference(storageFileShareName);

            // Check whether the File Share exist or not. By default, is should be created by TM team. And now, the focalpoint is: Spoelhof, Nathan (ND) <NDSpoelhof@dow.com> 
            // who create the storage manually (blob, file, table & queue)
            // Default size of file share is 1GB and configued in web.config
            if (!fileShare.Exists())
            {
                fileShare.Properties.Quota = storageFileShareSize;
                fileShare.CreateIfNotExists();
            }

            // Get a reference to the root directory of the share.        
            CloudFileDirectory root = fileShare.GetRootDirectoryReference();

            return root;
        }

        // Create & Check Directory/Folder exist or not
        public static void CreateFolderUnderFileShare(string folderName)
        {
            CloudFileDirectory rootDirectory = BasicAzureFileOperations();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            subDirectory.CreateIfNotExists();
        }

        // Delete Directory/Folder
        public static void DeleteFolderUnderFileShare(string folderName)
        {
            CloudFileDirectory rootDirectory = BasicAzureFileOperations();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            subDirectory.DeleteIfExists();
        }

        // Upload file into Directory/Folder
        public static void UploadFileToFolder(string folderName, string fileName, string filePath)
        {
            CloudFileDirectory rootDirectory = BasicAzureFileOperations();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            CloudFile file = subDirectory.GetFileReference(fileName);
            file.UploadFromFile(filePath);
        }

        // Delete file from Directory/Folder
        public static void DeleteFileFromFolder(string folderName, string fileName)
        {
            CloudFileDirectory rootDirectory = BasicAzureFileOperations();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            CloudFile file = subDirectory.GetFileReference(fileName);
            file.DeleteIfExists();
        }

        // File Download
        public static void DownloadFileFromFolder(string folderName, string fileName)
        {
            CloudFileDirectory rootDirectory = BasicAzureFileOperations();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            CloudFile downloadFile = subDirectory.GetFileReference(fileName);
            downloadFile.DownloadToFile(fileName, FileMode.Create);
        }
        
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            return storageAccount;
        }


    }
}
