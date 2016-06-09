using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using SendGrid;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebJobsDemo.Shared;
using WebJobsDemo.WebJob.Models;

namespace WebJobsDemo.WebJob
{
    public class Functions
    {
        public static async Task AddTelemetry(int dequeueCount, TextWriter log,
            [QueueTrigger("addtelemetry")] Telemetry telemetry,
            [Blob("telemetryerrors/{TrackingId}")] CloudBlockBlob blob)
        {
            try
            {
                var connString = CloudConfigurationManager.GetSetting("Entities");

                using (var db = new Entities())
                {
                    var data = new TelemetryTable()
                    {
                        TelemetryId = Guid.NewGuid(),
                        UserId = telemetry.UserId,
                        ContextId = telemetry.ContextId,
                        PostedOn = DateTime.UtcNow,
                        Payload = telemetry.Payload
                    };

                    db.Telemetries.Add(data);

                    throw new Exception("Ooops!");

                    await db.SaveChangesAsync();
                }

                await log.WriteLineAsync($"[AddTelemetry] Added: {telemetry}");
            }
            catch (Exception error)
            {
                if (dequeueCount == 5)
                {
                    var errorInfo = new ErrorInfo<Telemetry>()
                    {
                        Error = error,
                        Message = telemetry
                    };

                    await blob.UploadTextAsync(
                        JsonConvert.SerializeObject(errorInfo));
                }

                await log.WriteLineAsync(
                    $"[AddTelemetry] Error: {error.Message.ToSingleLine()} (Telementry: {telemetry}, DequeueCount: {dequeueCount})");
            }
        }

        public static async Task HandleTelemetryPoison(
            [QueueTrigger("addtelemetry-poison")] Telemetry telemetry,
            TextWriter log, [SendGrid] SendGridMessage message)
        {
            message.AddTo("louis@squideyes.com");
            message.Subject = $"[AddTelemetry] Error for {telemetry}";

            var sb = new StringBuilder();

            sb.AppendLine("TELEMETRY");
            sb.AppendLine("=========");
            sb.AppendLine(JsonConvert.SerializeObject(telemetry, Formatting.Indented));
            sb.AppendLine();
            sb.AppendLine("ERROR INFO");
            sb.AppendLine("==========");
            sb.AppendLine($"See telemetryerrors/{telemetry.TrackingId} for error details");

            await log.WriteLineAsync(
                $"[AddTelemetry] Dispatched Error Email (TrackingId: {telemetry.TrackingId})");
        }
    }
}
