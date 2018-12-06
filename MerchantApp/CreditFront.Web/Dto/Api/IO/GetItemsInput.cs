using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditFront.Web.Dto.Api.IO
{
    public class GetItemsInput
    {
        public string SearchQuery { get; set; }
        public long Offset { get; set; }
        public long Count { get; set; }
    }
}
