using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainElastic.Net.Base.Entities
{
    public class ArticleElasticDocument : BaseElasticDocument
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
    }
}
