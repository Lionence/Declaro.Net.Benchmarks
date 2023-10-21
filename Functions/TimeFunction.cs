using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiIsolated
{
    public class HttpTrigger
    {
        private readonly ILogger _logger;
        private static Time _time = new Time();

        public HttpTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("Time")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "patch", "delete")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            HttpResponseData response;

            switch (req.Method)
            {
                case "GET":
                    response = req.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(_time);
                    break;
                case "POST":
                case "PATCH":
                case "PUT":
                    _time = await req.ReadFromJsonAsync<Time>();
                    response = req.CreateResponse(HttpStatusCode.OK);
                    await response.WriteAsJsonAsync(_time);
                    break;
                case "DELETE":
                    _time = null;
                    response = req.CreateResponse(HttpStatusCode.OK);
                    break;
                default:
                    response = req.CreateResponse(HttpStatusCode.BadRequest);
                    break;
            }
            return response;
        }
        
        [Function("Times")]
        public async Task<HttpResponseData> RunList([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new List<Time>() { _time });

            return response;
        }
    }
}