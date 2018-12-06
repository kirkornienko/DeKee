using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class SearchInput
    {
        public int From { get; set; }
        public int Size { get; set; }
        public string SearchQuery { get; set; }
    }
}