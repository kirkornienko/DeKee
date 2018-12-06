namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class SignInInput : BaseCryptoTransferInput
    {
        public string phoneNumber { get; set; }
        public string password { get; set; }
    }
}