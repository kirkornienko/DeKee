using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainElastic.Net.Base.Entities
{
    public class SearchTenderElasticDocument: BaseElasticDocument
    {
        public TenderElasticDocument tender { get; set; }
        public string Id { get; set; }
        public DateTime DateModified { get; set; }
    }
}
