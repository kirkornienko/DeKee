using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.User
{
    public class UserFile : BaseEntity
    {

        public int Length { get; set; }

        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
    }
}
