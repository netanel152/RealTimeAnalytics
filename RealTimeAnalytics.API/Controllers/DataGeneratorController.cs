using Microsoft.AspNetCore.Mvc;
using RealTimeAnalytics.Shared;

namespace RealTimeAnalytics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataGeneratorController : ControllerBase
    {
        private readonly IDataGeneratorService _dataGeneratorService;

        public DataGeneratorController(IDataGeneratorService dataGeneratorService)
        {
            _dataGeneratorService = dataGeneratorService ?? throw new ArgumentNullException(nameof(dataGeneratorService));
        }

        [HttpPost]
        public async Task<IActionResult> StartDataGeneration()
        {
            await _dataGeneratorService.GenerateDataAsync();
            return Ok("Data generation started");
        }
    }
}
