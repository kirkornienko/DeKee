using DeKee.Base.Entities.DataStorage;
using DeKee.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Customer
{
    public class CustomerFile: SupervisedEntityGeneric<long>, IFileData
    {
        public long FileTypeId { get; set; }
        [ForeignKey("FileTypeId")]
        public FileType FileType { get; set; }
        public long CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public byte[] Data { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
