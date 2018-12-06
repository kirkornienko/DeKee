using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Globalization
{
    public class Country: SupportEntityGeneric<long>
    {
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string Code { get; set; }
    }
}
