using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using ApiAggregator.DTO;
using static ApiAggregator.Application.Filters.AggregatorDataFilter;

namespace ApiAggregator.Application.Filters
{

    public class AggregatorDataFilter
    {
        public CountriesFilter CountriesFilter { get; set; }
        public NewsFilter NewsFilter { get; set; }
    }

    public enum CountriesOrderByOptions
    {
            [Display(Name = "sort by...")] SimpleOrder = 0,
            [Display(Name = "Countries Asc ↑")] CountiesAsc,
            [Display(Name = "Counties Desc ↓")] CountriesDesc,
    }

    public enum CountriesFilterByOptions
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Region")]  ByRegion,
        [Display(Name = "By ByLanguage")] ByLanguage,
    }

    public class NewsFilter
    {
        public NewsOrderByOptions NewsSortBy { get; set; }
        public NewsFilterByOptions NewsFilterBy { get; set; }
        public string FilterValue { get; set; }
    }
    public enum NewsOrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Newest First")] NewestFirst,
        [Display(Name = "Oldest First")] OldestFirst,
    }
    public enum NewsFilterByOptions
    {
        [Display(Name = "All")] NoFilter = 0,
        [Display(Name = "By Source")] BySource,
        [Display(Name = "By Author")] ByAuthor,
        [Display(Name = "By Title")] ByTitle,
    }

    public class CountriesFilter
    {
        public CountriesOrderByOptions CountriesSortBy { get; set; }
        public CountriesFilterByOptions CountriesFilterBy { get; set; }
        public string FilterValue { get; set; }
    }

    public static class CountriesFilterSortOrder
    {
        public static IEnumerable<CountryDto> SortCountriesBy(this IEnumerable<CountryDto> countries, CountriesOrderByOptions ?orderBy)
        {
            switch (orderBy)
            {
                case CountriesOrderByOptions.CountiesAsc:
                    return countries.OrderBy(c => c.Name.Common);
                case CountriesOrderByOptions.CountriesDesc:
                    return countries.OrderByDescending(c => c.Name.Common);
                default:
                    return countries;
            }
        }

        public static IEnumerable<CountryDto> FilterCountriesBy(this IEnumerable<CountryDto> countries,
            CountriesFilter filter)
        {
            switch (filter?.CountriesFilterBy)
            {
                case CountriesFilterByOptions.ByRegion:
                    return countries.Where(c => c.Region == filter.FilterValue);
                case CountriesFilterByOptions.ByLanguage:
                    return countries.Where(c => c.SubRegion == filter.FilterValue);
                default:
                    return countries;
            }
        }
    }

    public static class NewsFilterSortOrder
    {
        public static IEnumerable<ArticlesDto> SortNewsBy(this IEnumerable<ArticlesDto> news,
            NewsOrderByOptions? orderBy)
        {
            switch (orderBy)
            {
                case NewsOrderByOptions.NewestFirst:
                    return news.OrderByDescending(n => n.PublishedAt);
                case NewsOrderByOptions.OldestFirst:
                    return news.OrderBy(n => n.PublishedAt);
                default:
                    return news;
            }
        }

        public static IEnumerable<ArticlesDto> FilterNewsBy(this IEnumerable<ArticlesDto> news,
            NewsFilter filter)
        {
            switch (filter?.NewsFilterBy)
            {
                case NewsFilterByOptions.BySource:
                    return news.Where(n => n.Source.Name == filter.FilterValue);
                case NewsFilterByOptions.ByAuthor:
                    return news.Where(n => n.Author == filter.FilterValue);
                case NewsFilterByOptions.ByTitle:
                    return news.Where(n => n.Title.Contains(filter.FilterValue));
                default:
                    return news;
            }
        }
    }
}
