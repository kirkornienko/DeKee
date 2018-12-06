using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.DataStorage
{
    public class FileType: SupportEntityGeneric<long>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
