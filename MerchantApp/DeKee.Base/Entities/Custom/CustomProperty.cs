using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomProperty : BaseEntityGeneric<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public CustomType CustomType { get; set; }
        public CustomValue CustomValue { get; set; }
        //public long ParentPropertyId { get; set; }
        //[ForeignKey("ParentPropertyId")]
        //public CustomProperty ParentProperty { get; set; }
    }
}
