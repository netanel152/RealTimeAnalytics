using Confluent.Kafka;
using RealTimeAnalytics.DataGenerator;
using RealTimeAnalytics.DataProcessor;
using RealTimeAnalytics.MongoDBService;
using RealTimeAnalytics.Shared;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var kafkaConfig = new ProducerConfig
{
    BootstrapServers = configuration["Kafka:BootstrapServers"],
    ClientId = configuration["Kafka:ClientId"]
};

var mongoConnectionString = configuration["MongoDB:ConnectionString"];
var mongoDatabaseName = configuration["MongoDB:DatabaseName"];
var mongoCollectionName = configuration["MongoDB:CollectionName"];

// Add services to the container.
builder.Services.AddScoped<IProducer<Null, string>>(provider => new ProducerBuilder<Null, string>(kafkaConfig).Build());
builder.Services.AddScoped<IConsumer<Ignore, string>>(provider => new ConsumerBuilder<Ignore, string>(kafkaConfig).Build());
builder.Services.AddScoped<IMongoDBService>(provider => new MongoDBService(mongoConnectionString, mongoDatabaseName, mongoCollectionName));
builder.Services.AddScoped<IDataGeneratorService, DataGeneratorService>();
builder.Services.AddScoped<IDataProcessorService, DataProcessorService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
