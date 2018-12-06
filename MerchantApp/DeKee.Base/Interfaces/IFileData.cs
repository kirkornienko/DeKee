using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Interfaces
{
    public interface IFileData
    {
        byte[] Data { get; set; }
        string Url { get; set; }
        string FileName { get; set; }
        string ContentType { get; set; }
    }
}
