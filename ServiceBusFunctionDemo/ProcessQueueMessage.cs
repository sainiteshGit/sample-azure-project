using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging

namespace ServiceBusFunctionDemo
{
    public class ProcessQueueMessage
    {
        [FunctionName("ProcessQueueMessage")]
        public void Run([ServiceBusTrigger("testqueue", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
