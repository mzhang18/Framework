using System;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.File;
using System.Collections.Generic;
using System.IO;

namespace Dow.SSD.Framework.Common
{
    public class AzureFileServiceOpViaSASHelper
    {
        public string signature = ConfigurationManager.AppSettings["SharedAccessSignature"].ToString();
        public string storageAccountName = ConfigurationManager.AppSettings["StorageAccountName"].ToString();
        public string fileShareName = ConfigurationManager.AppSettings["StorageFileShareName"].ToString();
        public string sasFileUri = string.Empty;
        public CloudFileShare fileShare;

        public AzureFileServiceOpViaSASHelper()
        {
            sasFileUri = string.Format("https://{0}.file.core.windows.net/{1}{2}", storageAccountName, fileShareName, signature);
            fileShare = new CloudFileShare(new Uri(sasFileUri));
            fileShare.CreateIfNotExists();
        }

        // Create & Check Directory/Folder exist or not
        public void CreateFolderUnderFileShare(string folderName)
        {
            CloudFileDirectory rootDirectory = fileShare.GetRootDirectoryReference();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            subDirectory.CreateIfNotExists();
        }

        // Delete Directory/Folder
        public void DeleteFolderUnderFileShare(string folderName)
        {
            CloudFileDirectory rootDirectory = fileShare.GetRootDirectoryReference();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            subDirectory.DeleteIfExists();
        }

        // Upload file into Directory/Folder
        public void UploadFileToFolder(string folderName, string fileName, string filePath)
        {
            CloudFileDirectory rootDirectory = fileShare.GetRootDirectoryReference();

            if (string.IsNullOrEmpty(folderName))
            {
                CloudFile file = rootDirectory.GetFileReference(fileName);
                file.UploadFromFile(filePath);
            }
            else
            {
                CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
                subDirectory.CreateIfNotExists();
                CloudFile file = rootDirectory.GetFileReference(fileName);
                file.UploadFromFile(filePath);
            }
        }

        // Delete file from Directory/Folder
        public void DeleteFileFromFolder(string folderName, string fileName)
        {
            CloudFileDirectory rootDirectory = fileShare.GetRootDirectoryReference();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            CloudFile file = subDirectory.GetFileReference(fileName);
            file.DeleteIfExists();
        }


        // File Download
        public void DownloadFileFromFolder(string folderName, string fileName)
        {
            CloudFileDirectory rootDirectory = fileShare.GetRootDirectoryReference();
            CloudFileDirectory subDirectory = rootDirectory.GetDirectoryReference(folderName);
            CloudFile downloadFile = subDirectory.GetFileReference(fileName);
            downloadFile.DownloadToFile(fileName, FileMode.Create);
        }


    }
}
