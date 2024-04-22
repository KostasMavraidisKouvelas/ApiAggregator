using System.Runtime.CompilerServices;
using ApiAggregator.Application;
using ApiAggregator.Application.Filters;
using Microsoft.AspNetCore.Mvc;
using static ApiAggregator.Application.Filters.AggregatorDataFilter;

namespace ApiAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IOperations _operations;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IOperations operations)
        {
            _logger = logger;
            _operations = operations;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] AggregatorDataFilter aggregatorDataFilter)
        {
            return Ok(await _operations.GetAggregatedDataAsync(aggregatorDataFilter));
        }
    }
}
