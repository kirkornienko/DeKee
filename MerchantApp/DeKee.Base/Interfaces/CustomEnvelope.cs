using DeKee.Base.Entities.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Interfaces
{
    public interface CustomEnvelope
    {
        CustomProperty[] CustomProperties { get; set; }
    }
}
