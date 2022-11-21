using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace BootcampExampleFunction
{
    public static class BitcoinValueCalculator
    {
        [FunctionName("BitcoinValueCalculator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string numberOfBitcoin = req.Query["numberOfBitcoin"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            numberOfBitcoin = numberOfBitcoin ?? data?.numberOfBitcoin;

            var rand = new Random();

            string responseMessage = string.IsNullOrEmpty(numberOfBitcoin)
                ? "Please pass in the number of bitcoin you have."
                : $"Your {numberOfBitcoin} bitcoin are worth R { int.Parse(numberOfBitcoin) * rand.Next(100, 1000000)}";

            return new OkObjectResult(responseMessage);
        }
    }
}
