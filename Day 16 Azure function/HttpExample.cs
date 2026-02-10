using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

public class HttpExample
{
    private readonly ILogger _logger;

    public HttpExample(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HttpExample>();
    }

    [Function("HttpExample")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("HTTP trigger function processed a request.");

        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var name = query["name"] ?? "Guest";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteString($"Hello {name}! Azure Function is running 🚀");
        return response;
    }
}
