using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models
{
    public class ArticleModel
    {
        public string Id { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public string Index { get; set; }
    }
}