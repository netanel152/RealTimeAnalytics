using Confluent.Kafka;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.DataProcessor
{
    public class DataProcessorService : IDataProcessorService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IMongoDBService _mongoDBService;

        public DataProcessorService(IConsumer<Ignore, string> consumer, IMongoDBService mongoDBService)
        {
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _mongoDBService = mongoDBService ?? throw new ArgumentNullException(nameof(mongoDBService));
        }

        public void ProcessData(string data)
        {
            // Process data here
            _mongoDBService.SaveDataAsync(data).Wait(); // This might not be the best approach, consider using async/await all the way
        }

        public async Task ProcessDataAsync(string data)
        {
            await _mongoDBService.SaveDataAsync(data);
        }

        public async Task StartAsync()
        {
            _consumer.Subscribe("iot-data-topic");

            while (true)
            {
                try
                {
                    var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                    if (consumeResult != null)
                    {
                        await ProcessDataAsync(consumeResult.Message.Value);
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }
    }
}
