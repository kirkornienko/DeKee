namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class SendTransferInput : BaseCryptoTransferInput
    {
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public decimal amount { get; set; }
        public decimal gasLimit { get; set; }
        public decimal gasPrice { get; set; }
    }
}