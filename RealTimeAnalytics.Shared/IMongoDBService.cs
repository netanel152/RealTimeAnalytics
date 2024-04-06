namespace RealTimeAnalytics.Shared
{
  public interface IMongoDBService
  {
    Task SaveDataAsync(string data);
  }
}