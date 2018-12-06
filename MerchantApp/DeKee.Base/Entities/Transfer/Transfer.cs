using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Transfer
{
    public class Transfer: BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string B4UHash { get; set; }
        public decimal GasAmount { get; set; }
        public decimal BTCAmount { get; set; }
        public decimal BFYAmount { get; set; }
        public decimal Amount { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Currency { get; set; }
        public string TransactionCode { get; set; }
        public string ApiId { get; set; }
        public string Status { get; set; }
        public string MerchantAutoSign { get; set; }
        public string CustomersAutoSign { get; set; }
        public decimal GasLimit { get; set; }
        public decimal Product { get; set; }
        public string DestinationNumber { get; set; }
        public string CustomerNumber { get; set; }
        public decimal B4UCommission { get; set; }
    }
}
