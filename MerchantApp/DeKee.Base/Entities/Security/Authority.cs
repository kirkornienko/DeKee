using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Security
{
    public class Authority : BaseEntity
    {
        //public string SourceBranchId { get; set; }
        //[ForeignKey("SourceBranchId")]
        //public Organization.Branch Source { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public int Level { get; set; }
    }
}
