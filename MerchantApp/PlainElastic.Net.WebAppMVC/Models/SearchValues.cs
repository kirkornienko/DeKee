using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models
{
    public class SearchValues
    {
        public string TenderId { get; set; }
        public string StatusId { get; set; }
        public string ProcedureType { get; set; }
        public string RegionId { get; set; }
        public string TaxId { get; set; }
        public string Funder { get; set; }
        public string Title { get; set; }
        public string OrganizationName { get; set; }
        public DateTime CreateDateFrom { get; set; }
        public DateTime CreateDateTill { get; set; }
        public DateTime PeriodStartFrom { get; set; }
        public DateTime PeriodStartTill { get; set; }
        public DateTime PeriodEndFrom { get; set; }
        public DateTime PeriodEndTill { get; set; }
        public string TenderValueAmountFrom { get; set; }
        public string TenderValueAmountTill { get; set; }
        public string CPV { get; set; }
        public string Description { get; set; }
    }
}