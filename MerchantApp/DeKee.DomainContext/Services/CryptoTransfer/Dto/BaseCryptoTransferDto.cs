using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class BaseCryptoTransferInput
    {
        public string token { get; set; }
        public string deviceId { get; set; }
    }
    public class BaseCryptoTransferOutput
    {
        public int responseCode { get; set; }
        public string responseText { get; set; }

        public virtual bool IsOk()
        {
            return responseCode == 0;
        }
    }
}
