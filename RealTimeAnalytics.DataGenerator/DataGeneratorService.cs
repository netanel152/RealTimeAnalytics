using Confluent.Kafka;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.DataGenerator;
public class DataGeneratorService : IDataGeneratorService
{
    private readonly IProducer<Null, string> _producer;
    public DataGeneratorService(IProducer<Null, string> producer)
    {
        _producer = producer ?? throw new ArgumentNullException(nameof(producer));
    }

    public async Task GenerateDataAsync()
    {
        while (true)
        {
            var value = "Simulated IoT data";
            await _producer.ProduceAsync("iot-data-topic", new Message<Null, string> { Value = value });
            await Task.Delay(1000); // Simulate data every second
        }
    }
}
