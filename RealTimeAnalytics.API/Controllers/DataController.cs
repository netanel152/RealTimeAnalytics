using Microsoft.AspNetCore.Mvc;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IMongoDBService _mongoDBService;

        public DataController(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService ?? throw new ArgumentNullException(nameof(mongoDBService));
        }

        [HttpPost]
        public async Task<IActionResult> SaveData([FromBody] string data)
        {
            await _mongoDBService.SaveDataAsync(data);
            return Ok("Data saved to MongoDB");
        }
    }
}
