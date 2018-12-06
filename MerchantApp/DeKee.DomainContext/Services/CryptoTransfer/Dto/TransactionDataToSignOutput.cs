using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class TransactionDataToSignOutput : BaseCryptoTransferOutput
    {
        public decimal gasPrice { get; set; }
        public decimal gasLimit { get; set; }
    }
}
