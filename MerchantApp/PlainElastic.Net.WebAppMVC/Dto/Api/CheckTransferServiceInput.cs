using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class CheckTransferServiceInput
    {
        public string PhoneNumber { get; set; }
        public string ReceiverId { get; internal set; }
        public string CustomerId { get; internal set; }
    }
}