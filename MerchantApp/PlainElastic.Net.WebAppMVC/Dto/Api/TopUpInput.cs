using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class TopUpInput
    {
        public decimal Product { get; set; }
        public string ReceiverNumber { get; set; }
        public string SenderNumber { get; set; }
        public string TransactionId { get; set; }
    }
}