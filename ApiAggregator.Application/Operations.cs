using System.Runtime.InteropServices.JavaScript;
using ApiAggregator.Application.Filters;
using ApiAggregator.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ApiAggregator.Application
{
    public class Operations : IOperations
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const int maxRetryCount = 3;

        public Operations(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public Operations()
        {
        }

        protected virtual async Task<WeatherResponseDto> GetWeatherAsync()
        {
            HttpResponseMessage response;
            string json;
            WeatherResponseDto weatherForecast;
            var apiKey = _configuration["Weather:ApiKey"];
            var weatherUrl = _configuration["Weather:WeatherUrl"];

            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                response = await _httpClient.GetAsync($"{weatherUrl}&appid={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    weatherForecast = JsonConvert.DeserializeObject<WeatherResponseDto>(json);
                    return weatherForecast;
                }
                else
                {
                    await Task.Delay(1000); // Wait for 1 second before retrying
                    retryCount++;
                }
            }

            return null;
        }

        protected virtual async Task<NewsResponseDto> GetNewsAsync()
        {
            HttpResponseMessage response;
            string json;
            NewsResponseDto news;
            //var newUrl = _configuration["NewsUrl"];
            //var apiKey = _configuration["ApiKey"];
            var apiKey = _configuration["News:ApiKey"];
            var newsUrl = _configuration["News:NewsUrl"];
            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                //response = await _httpClient.GetAsync($"{newsUrl}from=2024-03-22&sortBy=publishedAt&apiKey={apiKey}");
                response = await _httpClient.GetAsync(
                    $"{newsUrl}?q=tesla&from={DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd")}&apiKey={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    news = JsonConvert.DeserializeObject<NewsResponseDto>(json);
                    return news;
                }
                else
                {
                    await Task.Delay(1000); // Wait for 1 second before retrying
                    retryCount++;
                }
            }

            return null;
        }

        protected virtual async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            HttpResponseMessage response;
            string json;
            IEnumerable<CountryDto> countries;

            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                var countriesUrl = _configuration["countriesUrl"];
                response = await _httpClient.GetAsync(countriesUrl);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    countries = JsonConvert.DeserializeObject<IEnumerable<CountryDto>>(json);
                    return countries;
                }
                else
                {
                    await Task.Delay(1000); // Wait for 1 second before retrying
                    retryCount++;
                }
            }

            return null;
        }

        public async Task<AggregatorDto> GetAggregatedDataAsync(AggregatorDataFilter aggregatorDataFilter)
        {
            var weatherRersponse = this.GetWeatherAsync();
            var newsResponse = this.GetNewsAsync();
            var countriesResponse = this.GetCountriesAsync();

            await Task.WhenAll(weatherRersponse, newsResponse, countriesResponse);
            return new AggregatorDto
            {
                Articles = newsResponse.Result?.Articles?.
                    FilterNewsBy(aggregatorDataFilter?.NewsFilter)?
                    .SortNewsBy(aggregatorDataFilter?.NewsFilter?.NewsSortBy)?
                    .ToList(),
                WeatheForecasts = weatherRersponse?.Result?.List,
                CountryDto = countriesResponse.Result?.
                    FilterCountriesBy(aggregatorDataFilter?.CountriesFilter)?
                    .SortCountriesBy(aggregatorDataFilter?.CountriesFilter?.CountriesSortBy)?
                    .ToList()
            };
        }
    }
}
