using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.CryptoTransfer.Dto
{
    public class CryptocurrencyRatesOutput
    {
        public List<CryptoСurrency> cryptocurrencies { get; set; }

        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool isError{ get; set; }
        public bool isSessionExpired { get; set; }
    }

    public class CryptoСurrency
    {
        public int extId { get; set; }
        public string extSystemName { get; set; }
        public string fromCurrency { get; set; }
        public string toCurrency { get; set; }
        public double price { get; set; }
        public double low24hr { get; set; }
        public double high24hr { get; set; }
        public double percentChange { get; set; }
        public double baseVolume { get; set; }
        public double quoteVolume { get; set; }
        public bool isFrozen { get; set; }
        public double lowestAsk { get; set; }
        public double highestBid { get; set; }
        public DateTime date { get; set; }
    }
}
