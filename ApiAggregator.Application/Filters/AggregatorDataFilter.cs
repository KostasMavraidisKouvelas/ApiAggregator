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

    public class CountriesFilter
    {
        public CountriesOrderByOptions CountriesSortBy { get; set; }
        public CountriesFilterByOptions CountriesFilterBy { get; set; }
        public string FilterValue { get; set; }
    }

    public static class CountriesSortOrder
    {
        public static IEnumerable<CountryDto> SortCountriesBy(this IEnumerable<CountryDto> countries, CountriesOrderByOptions orderBy)
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
            switch (filter.CountriesFilterBy)
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
}
