Fallback Mechanism has been implemented through retry.
You can run the code through visual studio.It has been implemented in .NET 8.
Authentication and caching has which were optional have not been implemented.
Below are two httpRequest with  some filters that you can use.

https://localhost:44387/Aggregator?CountriesFilter.CountriesSortBy=0&CountriesFilter.CountriesFilterBy=1&CountriesFilter.FilterValue=Europe&NewsFilter.NewsSortBy=1&NewsFilter.NewsFilterBy=3&NewsFilter.FilterValue=War
https://localhost:44387/Aggregator?CountriesFilter.CountriesSortBy=0&CountriesFilter.CountriesFilterBy=1&CountriesFilter.FilterValue=Europe&NewsFilter.NewsSortBy=1&NewsFilter.NewsFilterBy=2&NewsFilter.FilterValue=Cheyenne%20MacDonald'