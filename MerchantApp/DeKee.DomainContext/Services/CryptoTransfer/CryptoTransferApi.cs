using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeKee.DomainContext.Services.CryptoTransfer.Dto;

namespace DeKee.DomainContext.Services.CryptoTransfer
{
    public class ExchangeApi : BaseAPI, IExchangeService
    {
        protected override string getBaseUrl()
        {
            return ConfigurationManager.AppSettings["ExchangeApiURL"] ?? "http://localhost:90";
        }
        public CryptocurrencyRatesOutput CryptocurrencyRates(CryptocurrencyRatesInput input)
        {
            var response = GETJsonDto<CryptocurrencyRatesOutput>("/api/CryptocurrencyRates", () => GetUrlData(input));
            return response;
        }

        private string GetUrlData(CryptocurrencyRatesInput input)
        {
            if (input == null) return null;

            StringBuilder builder = null;
            StringBuilder initBuilder() => (builder != null ? builder.Append("&") : new StringBuilder());
            if (input.From != default(DateTime))
            {
                builder = initBuilder().AppendFormat("From={0}", input.From.ToString("o"));
            }
            if (input.To != default(DateTime))
            {
                builder = initBuilder().AppendFormat("To={0}", input.To.ToString("o"));
            }
            if (!string.IsNullOrEmpty(input.FromCryptocurrency))
            {
                builder = initBuilder().AppendFormat("FromCryptocurrency={0}", input.FromCryptocurrency);
            }
            if (!string.IsNullOrEmpty(input.ToCryptocurrency))
            {
                builder = initBuilder().AppendFormat("ToCryptocurrency={0}", input.ToCryptocurrency);
            }
            //if (!string.IsNullOrEmpty(input.Hourly))
            {
                builder = initBuilder().AppendFormat("Hourly={0}", input.Hourly.ToString().ToLower());
            }
            if (!string.IsNullOrEmpty(input.DeviceId))
            {
                builder = initBuilder().AppendFormat("DeviceId={0}", input.DeviceId);
            }

            return builder.ToString();
        }
    }
    public class CryptoTransferApi : BaseAPI, ICryptoTransferService
    {
        protected override string getBaseUrl()
        {
            return ConfigurationManager.AppSettings["CryptoTransferApiURL"] ?? "http://localhost:91";
        }
        public SendTransferOutput SendTransfer(SendTransferInput input)
        {
            var response = POSTJsonDto<SendTransferOutput>("/api/SendTransaction", () => GetData(input));
            return response;
        }        

        public CreateWalletOutput CreateWallet(CreateWalletInput input)
        {
            var response = POSTJsonDto<CreateWalletOutput>("/api/CreateWallet", () => GetData(input));
            return response;
        }

        public SignInOutput SignIn(SignInInput input)
        {
            var response = POSTJsonDto<SignInOutput>("/api/SignIn", () => GetData(input));
            return response;
        }

        public SignUpOutput SignUp(SignUpInput input)
        {
            var response = POSTJsonDto<SignUpOutput>("/api/SignUp", () => GetData(input));
            return response;
        }
        public ConfirmOtpOutput ConfirmOtp(ConfirmOtpInput input)
        {
            var response = POSTJsonDto<ConfirmOtpOutput>("/api/ConfirmOtpCode", () => GetData(input));
            return response;
        }

        public TransactionDataToSignOutput TransactionDataToSign(TransactionDataToSignInput input)
        {
            var response = POSTJsonDto<TransactionDataToSignOutput>("/api/TransactionDataToSign", () => GetData(input));
            return response;
        }

        public GetWalletsOutput GetWallets(GetWalletsInput input)
        {
            var response = GETJsonDto<GetWalletsOutput>("/api/Wallets", () => GetUrlData(input));
            return response;
        }

        private string GetUrlData(BaseCryptoTransferInput input)
        {
            if (input == null) return null;

            StringBuilder builder = null;
            StringBuilder initBuilder() => (builder != null ? builder.Append("&") : new StringBuilder());
            
            if (!string.IsNullOrEmpty(input.deviceId))
            {
                builder = initBuilder().AppendFormat("DeviceId={0}", input.deviceId);
            }
            if (!string.IsNullOrEmpty(input.token))
            {
                builder = initBuilder().AppendFormat("Token={0}", input.token);
            }

            return builder.ToString();
        }

        public GetBalanceOutput GetBalance(GetBalanceInput input)
        {
            throw new NotImplementedException();
        }
    }
}
