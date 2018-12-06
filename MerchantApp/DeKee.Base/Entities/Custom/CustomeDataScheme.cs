using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomDataProperty
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
    }
}
