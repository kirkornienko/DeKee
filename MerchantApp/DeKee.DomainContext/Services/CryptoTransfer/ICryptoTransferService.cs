
namespace DeKee.DomainContext.Services
{
    public interface ICryptoTransferService
    {
        CryptoTransfer.Dto.ConfirmOtpOutput ConfirmOtp(CryptoTransfer.Dto.ConfirmOtpInput input);
        CryptoTransfer.Dto.SignInOutput SignIn(CryptoTransfer.Dto.SignInInput input);
        CryptoTransfer.Dto.SignUpOutput SignUp(CryptoTransfer.Dto.SignUpInput input);
        CryptoTransfer.Dto.CreateWalletOutput CreateWallet(CryptoTransfer.Dto.CreateWalletInput input);
        CryptoTransfer.Dto.SendTransferOutput SendTransfer(CryptoTransfer.Dto.SendTransferInput input);
        CryptoTransfer.Dto.TransactionDataToSignOutput TransactionDataToSign(CryptoTransfer.Dto.TransactionDataToSignInput input);
        CryptoTransfer.Dto.GetBalanceOutput GetBalance(CryptoTransfer.Dto.GetBalanceInput input);
        CryptoTransfer.Dto.GetWalletsOutput GetWallets(CryptoTransfer.Dto.GetWalletsInput input);
    }
}
