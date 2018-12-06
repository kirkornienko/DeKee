using System.Collections.Generic;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class CreateWalletOutput : BaseCryptoTransferOutput
    {
        public string address { get; set; }
        public string privateKey { get; set; }
        public List<string> mnemonicList { get; set; }
    }
}