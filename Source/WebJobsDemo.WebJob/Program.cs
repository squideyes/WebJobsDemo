using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SendGrid;
using System.Diagnostics;
using System.Net.Mail;

namespace WebJobsDemo.WebJob
{
    class Program
    {
        static void Main()
        {
            var watcher = new WebJobsShutdownWatcher();

            if (watcher.Token.IsCancellationRequested)
                return;

            var config = new JobHostConfiguration();

            config.Tracing.ConsoleLevel = TraceLevel.Verbose;

            config.UseTimers();

            config.UseCore();

            config.UseSendGrid(new SendGridConfiguration()
            {
                FromAddress = new MailAddress(
                    "support@bentley.com", "Bentley Support")
            });

            if (!watcher.Token.IsCancellationRequested)
            {
                var host = new JobHost(config);

                host.RunAndBlock();
            }
        }
    }
}
