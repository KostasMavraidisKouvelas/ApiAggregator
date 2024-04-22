using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAggregator.Application.Filters;
using ApiAggregator.DTO;

namespace ApiAggregator.Application
{
    public interface IOperations
    {
        public Task<AggregatorDto> GetAggregatedDataAsync(AggregatorDataFilter aggregatorDataFilter);
    }
}
