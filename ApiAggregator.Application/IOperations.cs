using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAggregator.DTO;

namespace ApiAggregator.Application
{
    public interface IOperations
    {
        public Task<WeatherResponseDto> GetWeatherAsync();

        public Task<NewsResponseDto> GetNewsAsync();

        public Task<List<CountryDto>> GetCountriesAsync();

        public Task<AggregatorDto> GetAggregatedDataAsync();
    }
}
