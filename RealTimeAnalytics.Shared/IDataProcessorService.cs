namespace RealTimeAnalytics.Shared
{
  public interface IDataProcessorService
  {
        Task StartAsync();
        Task ProcessDataAsync(string data);
    }
}
