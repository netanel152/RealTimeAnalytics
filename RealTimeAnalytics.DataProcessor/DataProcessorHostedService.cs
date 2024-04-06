using Microsoft.Extensions.Hosting;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.DataProcessor
{
    public class DataProcessorHostedService : BackgroundService
    {
        private readonly IDataProcessorService _dataProcessorService;

        public DataProcessorHostedService(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService ?? throw new ArgumentNullException(nameof(dataProcessorService));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _dataProcessorService.StartAsync();
        }
    }
}
