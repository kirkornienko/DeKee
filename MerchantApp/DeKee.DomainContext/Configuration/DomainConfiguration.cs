using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Configuration
{
    public class DomainConfiguration
    {
        public class ResultCode
        {
            public static readonly string Success = "00001";
            public static readonly string TechnicalError = "00999";
        }
    }
}
