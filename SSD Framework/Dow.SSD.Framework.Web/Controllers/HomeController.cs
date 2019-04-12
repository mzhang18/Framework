using Dow.SSD.Framework.Common;
using System.Web.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.KeyVault;
using System.Web.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace Dow.SSD.Framework.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.CurrentLoginUserID = UserInfoHelper.GetCurrentLoginUser();

            //Following codes are used to demo how to log error into SQL server database
            //try
            //{
            //    decimal salary = 1;
            //    int weeks = 0;
            //    decimal weekCheck = salary / weeks;
            //}
            //catch (Exception ex)
            //{
            //    LogUntity.LogError(ex, string.Empty);
            //}

            //Following codes are used to demo how to upload file into Azure file share
            //AttachmentHelper.CreateFolderUnderFileShare("FredTest");
            //AttachmentHelper.UploadFileToFolder("FredTest", "Jellyfish.jpg", @"C:\Users\Public\Pictures\Sample Pictures\Jellyfish.jpg");
            //AttachmentHelper.DeleteFileFromFolder("FredTest", "Jellyfish.jpg");
            //AttachmentHelper.DeleteFolderUnderFileShare("FredTest");
            //AttachmentHelper.DownloadFileFromFolder("FredTest", "Chrysanthemum.jpg");


            //Following codes are used to demo how to retrive account key if that account is registered in key vault
            //var vaultAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["VaultUri"];
            //var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(EncryptionHelper.GetToken));
            //var secret = keyVaultClient.GetSecretAsync(vaultAddress, "SAPrimaryKeyInKeyVault").GetAwaiter().GetResult();
            //var storagePrimaryAccessKey = secret.Value;

            //ViewBag.CurrentLoginUserID = UserInfoHelper.GetCurrentLoginUser();

            //AzureBlobServiceOpViaSASHelper blobHelper = new AzureBlobServiceOpViaSASHelper();
            //blobHelper.UploadBlobToContainer("", @"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");

            AzureFileServiceOpViaSASHelper fileHelper = new AzureFileServiceOpViaSASHelper();
            fileHelper.UploadFileToFolder("", "MyPic", @"C:\Users\Public\Pictures\Sample Pictures\Koala.jpg");


            return View();
        }
    }
}
