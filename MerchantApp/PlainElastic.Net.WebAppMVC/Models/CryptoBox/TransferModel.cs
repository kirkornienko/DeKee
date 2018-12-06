using B4U.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class TransferModel : IRepositoryEntity<string>
    {
        public static class TransactionCodes
        {
            public static readonly string PersonToPerson = "P2P";
            public static readonly string MerchantToPerson = "M2P";
            public static readonly string MerchantToMobile = "M2Mb";
            public static readonly string PersonToMobile = "P2M";
            public static readonly string MerchantToMerchant = "M2M";
        }
        public static class Statuses
        {
            public static readonly string Created = "Created";
            public static readonly string Executed = "Executed";
            public static readonly string Failed = "Failed";
        }
        public string Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string B4UHash { get; set; }
        public decimal GasAmount { get; set; }
        public decimal BTCAmount { get; set; }
        public decimal BFYAmount { get; set; }
        public decimal Amount { get; set; }
        public CustomerModel Sender { get; set; }
        public CustomerModel Receiver { get; set; }
        public string Currency { get; set; }
        public string TransactionCode { get; set; }
        public string ApiId { get; set; }
        public string Status { get; set; }
        public string MerchantAutoSign { get; set; }
        public string CustomersAutoSign { get; set; }
        public UserModel Merchant { get; set; }
        public decimal GasLimit { get; set; }
        public decimal Product { get; set; }
        public string DestinationNumber { get; set; }
        public string CustomerNumber { get; set; }
        public decimal B4UCommission { get; set; }
    }
}