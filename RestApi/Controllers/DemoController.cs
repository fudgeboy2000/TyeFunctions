using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public DemoController(ILogger<DemoController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DataModel demoData)
        {
            _logger.LogInformation($"DemoController - starting POST");

            var client = _clientFactory.CreateClient("FunctionApp");

            var httpResponse = await client.PostAsync("/api/Function1", new StringContent(JsonSerializer.Serialize(demoData), Encoding.UTF8, "application/json"));

            httpResponse.EnsureSuccessStatusCode();

            var responseString = await httpResponse.Content.ReadAsStringAsync();

            _logger.LogInformation($"DemoController -- response string {responseString}");

            return new OkObjectResult(responseString);
        }
    }
}
