﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregator.Application
{
    public interface IOperations
    {
        public  Task GetWeather();
    }
}
