using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models
{
    public class TenderModel
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string Mode { get; set; }

        public long OrganizationId { get; set; }

        public long ProcedureModeId { get; set; }

        public long StatusId { get; set; }

        public long TenderPeriodId { get; set; }

        public string Value { get; set; }

        public string Period { get; set; }

        public string Region { get; set; }

        public string Organization { get; set; }

        public string Classifications { get; set; }

        public string AdditionalClassifications { get; set; }

        public string TenderId { get; set; }

        public string Title { get; set; }

        public string TitleEN { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime CreationTime { get; set; }

        public string ProcedureTypeId { get; set; }

        public long CreatorUserId { get; set; }

        public int TenderTemplateType { get; set; }

        public bool IsOwn { get; set; }

        public bool NotExport { get; set; }

        public string Factor { get; set; }
    }
}