using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomValue: BaseEntityGeneric<long>
    {
        //public long CustomPropertyId { get; set; }
        //[ForeignKey("CustomPropertyId")]
        //public CustomProperty CustomProperty { get; set; }
        public long EntityId { get; set; }
        public string Value { get; set; }
    }
}
