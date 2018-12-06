using B4U.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class CustomerModel : IRepositoryEntity<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WalletAddress { get; set; }
        public TransferToServiceModel TransferToServiceInfo { get; set; }
    }
}