using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregator.DTO
{
    public class AggregatorDto
    {
        public NewsResponseDto NewsResponseDto { get; set; }
        public WeatherResponseDto WeatherResponseDto { get; set; }
    }
}
