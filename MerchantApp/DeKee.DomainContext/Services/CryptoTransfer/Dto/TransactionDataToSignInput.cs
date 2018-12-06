using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class TransactionDataToSignInput : BaseCryptoTransferInput
    {
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public int amount { get; set; }
        public string amountString { get; set; }
        public string currency { get; set; }
    }
}
