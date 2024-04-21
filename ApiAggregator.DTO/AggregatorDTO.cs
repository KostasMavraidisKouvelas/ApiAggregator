using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregator.DTO
{
    public class AggregatorDto
    {
        public List<ArticlesDto> Articles { get; set; }
        public List<WeatherDto>  WeatheForecasts { get; set; }
        public List<CountryDto> CountryDto { get; set; }
    }
}
