using B4U.Base.Entities;
using B4U.ElasticContext.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class Branch: IRepositoryEntity<string>
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string RegionId { get; set; }
    }
}