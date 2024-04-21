using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiAggregator.DTO
{
    public class CountryDto
    {
        public FlagsDto Flags { get; set; }
        public NameDto Name { get; set; }
    }
    public class FlagsDto
    {
        public string Png { get; set; }
        public string Svg { get; set; }
        public string Alt { get; set; }
    }

    public class NativeNameDto
    {
        public string Official { get; set; }
        public string Common { get; set; }
    }

    public class NameDto
    {
        public string Common { get; set; }
        public string Official { get; set; }
        public NativeNameDto NativeName { get; set; }
    }
}
