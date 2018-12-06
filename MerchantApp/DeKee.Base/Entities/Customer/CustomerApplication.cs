using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Customer
{
    public class CustomerApplication : BaseEntityGeneric<long>
    {
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public virtual ICollection<Custom.CustomProperty> CustomProperties { get; set; }
    }
}
