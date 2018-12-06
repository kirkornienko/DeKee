namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class ConfirmOtpInput : BaseCryptoTransferInput
    {
        public string phoneNumber { get; set; }
        public string otpCode { get; set; }
    }
}