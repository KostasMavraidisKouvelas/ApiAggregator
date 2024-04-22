using System.Runtime.CompilerServices;
using ApiAggregator.Application;
using ApiAggregator.Application.Filters;
using Microsoft.AspNetCore.Mvc;
using static ApiAggregator.Application.Filters.AggregatorDataFilter;

namespace ApiAggregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregatorController : ControllerBase
    {
        private readonly IOperations _operations;

        private readonly ILogger<AggregatorController> _logger;

        public AggregatorController(ILogger<AggregatorController> logger,IOperations operations)
        {
            _logger = logger;
            _operations = operations;
        }

        [HttpGet(Name = "GetAggregatorData")]
        public async Task<IActionResult> Get([FromQuery] AggregatorDataFilter aggregatorDataFilter)
        {
            return Ok(await _operations.GetAggregatedDataAsync(aggregatorDataFilter));
        }
    }
}
