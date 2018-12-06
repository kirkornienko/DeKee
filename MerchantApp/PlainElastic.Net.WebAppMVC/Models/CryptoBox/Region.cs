using B4U.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class Region : IRepositoryEntity<string>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}