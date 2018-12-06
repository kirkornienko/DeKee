using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Organization
{
    public class Branch : BaseEntityGeneric<long>
    {
        public Branch()
        {
            ParrentBranch = null;
            //ChildBranches = new List<Division>();
        }
        //public Branch(Branch _parrentBranch)
        //{
        //    ParrentBranch = _parrentBranch;
        //}
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Address { get; set; }
        public int RegionCode { get; set; }
        public string TaxId { get; set; }
        public string FirstPerson { get; set; }
        public string SecondPerson { get; set; }
        public string Phone { get; set; }

        public DateTime? CloseDate { get; set; }

        public bool IsTemporarilySuspended { get; set; }

        public long? ParrentId { get; set; }
        [ForeignKey("ParrentId")]
        public Branch ParrentBranch { get; set; }
        public long BranchTypeId { get; set; }
        [ForeignKey("BranchTypeId")]
        public BranchType BranchType { get; set; }
    }
}
