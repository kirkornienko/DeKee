using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomType : BaseEntityGeneric<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public long? AggrigateTypeId { get; set; }
        [ForeignKey("AggrigateTypeId")]
        public virtual CustomType AggrigateType { get; set; } 
    }
}
