using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Custom
{
    public class CustomPropertyRegestry: BaseEntityGeneric<long>
    {
        public long CustomPropertyId { get; set; }
        [ForeignKey("CustomPropertyId")]
        public CustomProperty CustomProperty { get; set; }

        public long CustomValueId { get; set; }
        [ForeignKey("CustomValueId")]
        public CustomValue CustomValue { get; set; }

        public long? RelatedEntityId { get; set; }
        [ForeignKey("RelatedEntityId")]
        public BaseEntityGeneric<long> RelatedEntity { get; set; }
    }
}
