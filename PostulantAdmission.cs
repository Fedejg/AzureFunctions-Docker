using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionExample
{
    public static class PostulantAdmission
    {
        [FunctionName("PostulantAdmission")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var PostulantAdmitted = data.age >= 24 && data.age <= 32;

            var AdmittedResult = new { data, PostulantAdmitted };

            return PostulantAdmitted != null
                ? (ActionResult)new OkObjectResult($"Candidate past the Admission: {data.age} years")
                : new BadRequestObjectResult("Please pass an age on the query string or in the request body");
        }
    }
}


 