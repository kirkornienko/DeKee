using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class CabinetModel
    {
        public List<string> Data { get; set; }
        public Dictionary<string, string> Localization { get; set; }
        public Dictionary<string, string> ActionLinks { get; set; }
    }
}