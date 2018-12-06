using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainElastic.Net.Base.Entities
{
    public class TenderElasticDocument : BaseElasticDocument
    {
        public TenderElasticDocument()
        {
            Value = new TenderValueElasticDocument();
            Period = new TenderPeriodElasticDocument();
            Region = new TenderRegionElasticDocument();
            Organization = new OrganizationElasticDocument();
            Classifications = new TenderClassificationElasticDocument[] { };
            AdditionalClassifications = new TenderAdditionalClassificationElasticDocument[] { };
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public string DescriptionEN { get; set; }

        public string Mode { get; set; }

        public long OrganizationId { get; set; }

        public long ProcedureModeId { get; set; }

        public long StatusId { get; set; }

        public long TenderPeriodId { get; set; }

        public TenderValueElasticDocument Value { get; set; }

        public TenderPeriodElasticDocument Period { get; set; }

        public TenderRegionElasticDocument Region { get; set; }

        public OrganizationElasticDocument Organization { get; set; }

        public TenderClassificationElasticDocument[] Classifications { get; set; }

        public TenderAdditionalClassificationElasticDocument[] AdditionalClassifications { get; set; }

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
