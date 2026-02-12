using Microsoft.Identity.Client;
using dotenv.net;
using System.Linq;

// Load environment variables from .env file
DotEnv.Load();
var envVars = DotEnv.Read();

// Retrieve Azure AD Application ID and tenant ID
string _clientId = envVars["CLIENT_ID"];
string _tenantId = envVars["TENANT_ID"];

string[] _scopes = { "User.Read" };

var app = PublicClientApplicationBuilder.Create(_clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithDefaultRedirectUri()
    .Build();


AuthenticationResult result;

try
{
    // Try silent login first
    var accounts = await app.GetAccountsAsync();
    result = await app.AcquireTokenSilent(_scopes, accounts.FirstOrDefault())
                      .ExecuteAsync();
}
catch (MsalUiRequiredException)
{
    // Fallback → Interactive login
    result = await app.AcquireTokenInteractive(_scopes)
                      .ExecuteAsync();
}

Console.WriteLine($"Access Token:\n{result.AccessToken}");
