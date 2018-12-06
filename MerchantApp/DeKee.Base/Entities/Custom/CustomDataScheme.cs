using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomDataScheme : BaseEntity
    {
        public List<CustomDataProperty> Properties { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
