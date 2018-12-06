using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class GetWalletsOutput : BaseCryptoTransferOutput
    {
        public List<WalletDto> wallets { get; set; }
        public override bool IsOk()
        {
            return base.IsOk() && wallets != null;
        }
    }
    public class WalletDto
    {
        public decimal balance { get; set; }
        public string address { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public bool isMain { get; set; }
    }
}
