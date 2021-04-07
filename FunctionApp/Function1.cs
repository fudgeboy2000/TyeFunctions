using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Definitions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            log.LogInformation("Function1 - C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var demoData = JsonConvert.DeserializeObject<DataModel>(requestBody);
            demoData.DemoData += " - function added this";

            log.LogInformation("Function1 - C# HTTP trigger returning response");
            string responseString = JsonConvert.SerializeObject(demoData);
            return new OkObjectResult($"This HTTP triggered function executed successfully, response is {responseString}");
        }
    }
}

