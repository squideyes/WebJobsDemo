using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System;
using System.Configuration;
using WebJobsDemo.Shared;

namespace WebJobsDemo.TelemetrySource
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var account = CloudStorageAccount.Parse(
                    ConfigurationManager.AppSettings["ConnString"]);

                var client = account.CreateCloudQueueClient();

                var queue = client.GetQueueReference("addtelemetry");

                queue.CreateIfNotExists();

                var someClass = new SomeClass()
                {
                    SomeClassId = Guid.NewGuid(),
                    Name = "Something Important To Know"
                };

                var telemetry = new TelemetryMessage()
                {
                    TrackingId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ContextId = Guid.NewGuid(),
                    Payload = JsonConvert.SerializeObject(someClass)
                };

                queue.AddMessage(new CloudQueueMessage(
                    JsonConvert.SerializeObject(telemetry)));

                Console.WriteLine(
                    $"Enqueued Telemetry (TrackingId: {telemetry.TrackingId})...");
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: "+ error.Message);
            }

            Console.WriteLine();
            Console.Write("Press any key to terminate the program...");

            Console.ReadKey(true);
        }
    }
}
