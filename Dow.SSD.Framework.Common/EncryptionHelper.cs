using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Dow.SSD.Framework.Common
{
    public static class EncryptionHelper
    {
        //the method that will be provided to the KeyVaultClient
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(WebConfigurationManager.AppSettings["ClientId"], WebConfigurationManager.AppSettings["ClientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        public static string GetSAKey()
        {
            var vaultAddress = System.Web.Configuration.WebConfigurationManager.AppSettings["VaultUri"];
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(EncryptionHelper.GetToken));
            var secret = keyVaultClient.GetSecretAsync(vaultAddress, "SAPrimaryKeyInKeyVault").GetAwaiter().GetResult();
            var storagePrimaryAccessKey = secret.Value;

            return storagePrimaryAccessKey;
        }

    }
}
