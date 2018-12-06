using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class WalletModel : Model
    {
        public decimal Balance { get; set; }
        public string Address { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public string BalanceStr { get => Balance + " " + Currency; }
    }
}