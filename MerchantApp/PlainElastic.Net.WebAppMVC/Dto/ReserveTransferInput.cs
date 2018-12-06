using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B4U.DomainContext.Services.Dto;

namespace PlainElastic.Net.WebAppMVC.Dto
{
    public class ReserveTransferInput
    {
        public decimal Product { get;  set; }
        public string OperatorId { get;  set; }
        public string SenderNumber { get;  set; }
        public string DestinationNumber { get;  set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public decimal RetailAmount { get; set; }
        public string RetailCurrency { get; set; }
    }

    public class ReserveTransferOutput
    {
        public TopUpResponse data { get; set; }
        public decimal BFYAmount { get; set; }
        public decimal BTCAmount { get; set; }
        public decimal GasAmount { get; set; }
    }
}