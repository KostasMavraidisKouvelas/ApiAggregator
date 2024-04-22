using ApiAggregator.Application.Filters;
using ApiAggregator.DTO;
using Newtonsoft.Json;

namespace ApiAggregator.Application
{
    public class Operations : IOperations
    {
        private HttpClient _httpClient;
        private const int maxRetryCount = 3;

        public Operations(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected virtual async Task<WeatherResponseDto> GetWeatherAsync()
        {
            HttpResponseMessage response;
            string json;
            WeatherResponseDto weatherForecast;

            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                response = await _httpClient.GetAsync("http://api.openweathermap.org/data/2.5/forecast?lat=44.34&lon=10.99&appid=e5dcaa673b9fa708fdbb48a6cbe10e28");
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

            int retryCount = 0;
            while (retryCount < maxRetryCount)
            {
                response = await _httpClient.GetAsync("https://newsapi.org/v2/everything?q=tesla&from=2024-03-22&sortBy=publishedAt&apiKey=89ab964822464fba93a0025f8cfd7948");
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
                response = await _httpClient.GetAsync("https://restcountries.com/v3.1/all");
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
                Articles = newsResponse.Result.Articles.
                    FilterNewsBy(aggregatorDataFilter.NewsFilter)
                    .SortNewsBy(aggregatorDataFilter.NewsFilter.NewsSortBy)
                    .ToList(),
                WeatheForecasts = weatherRersponse.Result.List,
                CountryDto = countriesResponse.Result.
                    FilterCountriesBy(aggregatorDataFilter.CountriesFilter)
                    .SortCountriesBy(aggregatorDataFilter.CountriesFilter.CountriesSortBy)
                    .ToList()
            };
        }
    }
}
