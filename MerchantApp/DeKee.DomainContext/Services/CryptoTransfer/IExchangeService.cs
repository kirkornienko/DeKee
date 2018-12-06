using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer
{
    public interface IExchangeService
    {
        Dto.CryptocurrencyRatesOutput CryptocurrencyRates(Dto.CryptocurrencyRatesInput input);
    }
}
