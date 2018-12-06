using System;

namespace PlainElastic.Net.Base.Entities
{
    public class TenderPeriodElasticDocument
    {
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }

        public long Id { get; set; }
    }
}