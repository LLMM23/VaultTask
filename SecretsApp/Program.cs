using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.Commons;

var EndPoint = "https://localhost:8201/";
var httpClientHandler = new HttpClientHandler();
httpClientHandler.ServerCertificateCustomValidationCallback =
(message, cert, chain, sslPolicyErrors) => { return true; };

// Initialize one of the several auth methods. //Path finder du fra YML filen 
IAuthMethodInfo authMethod =
new TokenAuthMethodInfo("00000000-0000-0000-0000-000000000000");
// Initialize settings. You can also set proxies, custom delegates etc. here.
var vaultClientSettings = new VaultClientSettings(EndPoint, authMethod)
{
    Namespace = "",
    MyHttpClientProviderFunc = handler
    => new HttpClient(httpClientHandler)
    {
        BaseAddress = new Uri(EndPoint)
    }
};
IVaultClient vaultClient = new VaultClient(vaultClientSettings);

// Use client to read a key-value secret.
//Ekstra tilføjelser, så har jeg tilføjet en ekstra secret for path: hemmeligheder 
Secret<SecretData> kv2Secret = await vaultClient.V1.Secrets.KeyValue.V2
.ReadSecretAsync(path: "hemmeligheder", mountPoint: "secret");
var minkode = kv2Secret.Data.Data["MinKode"];
//var minkode1 = kv2Secret.Data.Data["MinKode1"];
Console.WriteLine($"path_hemmeligheder-MinKode: {minkode}");
//Console.WriteLine($"path_hemmeligheder-MinKode1: {minkode1}");


/*
// Nyt path - hemmeligheder2 - Bemærk at kv2Secret er blevet ændret til kv3Secret - For at kunne køre begge path 
Secret<SecretData> kv3Secret = await vaultClient.V1.Secrets.KeyValue.V2
.ReadSecretAsync(path: "hemmeligheder2", mountPoint: "secret");
var minkode2 = kv3Secret.Data.Data["MinKode2"];
Console.WriteLine($"path_hemmeligheder2-MinKode2: {minkode2}");

*/