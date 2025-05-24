using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_travels.DTOs
{
    public class CountryAPIDto
    {
        public NameInfo Name { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public List<string> Capital { get; set; }
        public FlagInfo Flags { get; set; }
        public List<string> Continents { get; set; }

        public class NameInfo
        {
            public string Common { get; set; }
            public string Official { get; set; }
            public Dictionary<string, NativeNameInfo> NativeName { get; set; }
        }

        public class NativeNameInfo
        {
            public string Official { get; set; }
            public string Common { get; set; }
        }

        public class FlagInfo
        {
            public string Png { get; set; }
            public string Svg { get; set; }
        }
    }
}
