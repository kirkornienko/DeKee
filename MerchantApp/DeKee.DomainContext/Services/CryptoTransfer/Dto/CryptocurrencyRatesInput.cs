using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class CryptocurrencyRatesInput
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string FromCryptocurrency { get; set; }
        public string ToCryptocurrency { get; set; }
        public bool Hourly { get; set; }
        public string DeviceId { get; set; }
    }
}
