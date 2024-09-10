using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Azure.CosmosDB.Operations
{
    public static class CosmosOperations
    {
        [FunctionName("SaveStories")]
        public static async Task<IActionResult> SaveStories(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

          

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            await CosmosDbHelper.UpsertStories(requestBody);

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }


        [FunctionName("SaveAssets")]
        public static async Task<IActionResult> SaveAssets(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("SaveAssets HTTP trigger function processing a request.");



            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            await CosmosDbHelper.UpsertAssets(requestBody);

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("DeleteStory")]
        public static async Task<IActionResult> DeleteStory(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("DeleteStory HTTP trigger function processed a request.");

            int storyId = Int32.Parse(req.Query["storyId"]);

            await CosmosDbHelper.DeleteStory(storyId);

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }


        [FunctionName("DeleteAsset")]
        public static async Task<IActionResult> DeleteAsset(
           [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("DeleteAsset HTTP trigger function processing a request.");
            int assetId = Int32.Parse(req.Query["assetId"]);

            await CosmosDbHelper.DeleteAsset(assetId);

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
