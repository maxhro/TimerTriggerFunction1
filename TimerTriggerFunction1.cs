using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MaxHRO.Function1
{
    public class TimerTriggerFunction1
    {
        private readonly ILogger _logger;

        public TimerTriggerFunction1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerTriggerFunction1>();
        }

        [Function("TimerTriggerFunction1")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            // Connect to Azure SQL Database via Binding

        }
    }
}
