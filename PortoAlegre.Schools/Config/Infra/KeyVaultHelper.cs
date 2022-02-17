using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using PortoAlegre.Schools.Config.Models;

namespace PortoAlegre.Schools.Config.infra
{
    public class KeyVaultHelper
    {
        public static BingMapsClientConfig GetSecret(string vaultName="localsearch-api")
        {
            AzureServiceTokenProvider azureServiceTokenProvider = new();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var distanceMatrixKey = keyVaultClient.GetSecretAsync("https://" + vaultName + ".vault.azure.net/secrets/LocalSearchApi--DistanceMatrixEndpoint").Result.Value;
            var RouteKey = keyVaultClient.GetSecretAsync("https://" + vaultName + ".vault.azure.net/secrets/LocalSearchApi--RouteEndpoint").Result.Value;
            var bingMapsKey = keyVaultClient.GetSecretAsync("https://" + vaultName + ".vault.azure.net/secrets/LocalSearchApi--Key").Result.Value;
            var connectionApi = keyVaultClient.GetSecretAsync("https://" + vaultName + ".vault.azure.net/secrets/LocalSearchApi--Key").Result.Value;

            BingMapsClientConfig config = new BingMapsClientConfig(distanceMatrixKey, RouteKey, bingMapsKey);

            return config;
        }
    }
}
