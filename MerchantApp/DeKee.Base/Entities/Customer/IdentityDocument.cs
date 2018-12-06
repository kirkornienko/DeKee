using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Customer
{
    public class IdentityDocument:SupervisedEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
