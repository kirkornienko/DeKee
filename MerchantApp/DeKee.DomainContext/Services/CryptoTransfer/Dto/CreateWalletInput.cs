namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class CreateWalletInput : BaseCryptoTransferInput
    {
        public string phone { get; set; }
        public bool isMain { get; set; }
        public string walletName { get; set; }
        public string currency { get; set; }
    }
}