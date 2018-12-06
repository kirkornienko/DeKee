namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class SignUpInput : BaseCryptoTransferInput
    {
        public string phoneNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}