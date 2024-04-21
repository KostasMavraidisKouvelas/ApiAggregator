using ApiAggregator.DTO;
using Newtonsoft.Json;

namespace ApiAggregator.Application
{
    public class Operations : IOperations
    {
        private HttpClient _httpClient;

        public Operations(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponseDto>  GetWeather()
        {
            var response = await _httpClient.GetAsync("http://api.openweathermap.org/data/2.5/forecast?lat=44.34&lon=10.99&appid=e5dcaa673b9fa708fdbb48a6cbe10e28");
            var json = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<WeatherResponseDto>(json);
            return weatherForecast;
        }
    }
}
