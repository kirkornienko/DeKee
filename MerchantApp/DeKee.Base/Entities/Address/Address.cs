using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Address
{
    //Test Comment 12
    public class Level4 : BaseEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public long? Level3Id { get; set; }
        [ForeignKey("Level3Id")]
        public virtual Level3 Level3 { get; set; }
    }

    public class Level3 : BaseEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public long? Level2Id { get; set; }
        [ForeignKey("Level2Id")]
        public virtual Level2 Level2 { get; set; }

        public virtual ICollection<Level4> Level4 { get; set; }
    }

    public class Level2 : BaseEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public long? Level1Id { get; set; }
        [ForeignKey("Level1Id")]
        public virtual Level1 Level1 { get; set; }

        public virtual ICollection<Level3> Level3 { get; set; }
    }

    public class Level1 : BaseEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Level2> Level2 { get; set; }
    }
}
