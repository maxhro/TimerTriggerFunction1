using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Azure.Functions.Worker.Http;
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
        public OutputType Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

            try
            {
                // Connect to Azure SQL Database via Binding
                return new OutputType()
                {
                    testItem = new TestItem
                    {
                        FrontName = "Werner",
                        LastName = "Brösel",
                        Birthday = new DateTime(1981, 12, 12),
                        CountShoes = Convert.ToInt16((new Random().Next() * 100))
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                return new OutputType();
            }
        }
    }

    public class OutputType
    {
        [SqlOutput("dbo.TestData", connectionStringSetting: "sqlconnect")]
        public TestItem? testItem { get; set; }
    }
}
