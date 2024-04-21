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


        public async Task<WeatherResponseDto>  GetWeatherAsync()
        {
            var response = await _httpClient.GetAsync("http://api.openweathermap.org/data/2.5/forecast?lat=44.34&lon=10.99&appid=e5dcaa673b9fa708fdbb48a6cbe10e28");
            var json = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<WeatherResponseDto>(json);
            return weatherForecast;
        }

        public async Task<NewsResponseDto> GetNewsAsync()
        {
            var response = await _httpClient.GetAsync("https://newsapi.org/v2/everything?q=tesla&from=2024-03-20&sortBy=publishedAt&apiKey=89ab964822464fba93a0025f8cfd7948");
            var json = await response.Content.ReadAsStringAsync();
            var news = JsonConvert.DeserializeObject<NewsResponseDto>(json);
            return news;
        }

        public async Task<AggregatorDto> GetAggregatedDataAsync()
        {
            var weatherRersponse =  this.GetWeatherAsync();
            var  newsResponse =  this.GetNewsAsync();

            await Task.WhenAll(weatherRersponse, newsResponse);
            return new AggregatorDto
            {
                NewsResponseDto = newsResponse.Result,
                WeatherResponseDto = weatherRersponse.Result
            };
        }
    }
}
