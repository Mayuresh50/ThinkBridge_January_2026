using Microsoft.Graph;
using Azure.Identity;
using dotenv.net;

// Load environment variables from .env file (if present)
DotEnv.Load();
var envVars = DotEnv.Read();

// Read Azure AD app registration values
string clientId = envVars["CLIENT_ID"];
string tenantId = envVars["TENANT_ID"];

// Validate environment variables
if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(tenantId))
{
    Console.WriteLine("Please set CLIENT_ID and TENANT_ID environment variables.");
    return;
}

var scopes = new[] { "User.Read" };

var options = new InteractiveBrowserCredentialOptions
{
    ClientId = clientId,
    TenantId = tenantId,
    RedirectUri = new Uri("http://localhost")
};

var credential = new InteractiveBrowserCredential(options);

var graphClient = new GraphServiceClient(credential);

Console.WriteLine("Retrieving user profile...");
await GetUserProfile(graphClient);

async Task GetUserProfile(GraphServiceClient graphClient)
{
    try
    {
        var me = await graphClient.Me.GetAsync();

        Console.WriteLine($"Display Name: {me?.DisplayName}");
        Console.WriteLine($"Principal Name: {me?.UserPrincipalName}");
        Console.WriteLine($"User Id: {me?.Id}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error retrieving profile: {ex.Message}");
    }
}
