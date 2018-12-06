using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class TransferToServiceModel: Model
    {
        public string Country { get; set; }
        public long CountryId { get; set; }
        public string Operator { get; set; }
        public long OperatorId { get; set; }
        public long ConnectionRate { get; set; }
        public string PhoneNumber { get; set; }
        public string ReceiverCurrency { get; set; }
        public decimal[] TopUpAmounts { get; set; }
        public decimal[] ServiceFees { get; set; }
        public string RetailCurrency { get; set; }
        public decimal[] RetailPrices { get; set; }
        public decimal[] WholesalePriceList { get; set; }

    }
}