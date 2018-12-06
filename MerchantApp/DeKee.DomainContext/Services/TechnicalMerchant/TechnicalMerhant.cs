using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeKee.DomainContext.Services.TechnicalMerchant.Dto;

namespace DeKee.DomainContext.Services.TechnicalMerchant
{
    public class TechnicalMerhantApi : BaseAPI, ITechnicalMerhant
    {
        protected override string getBaseUrl()
        {
            return "http://localhost:101";
        }

        public CheckTopUpOutput CheckTopUp(CheckTopUpInput input)
        {
            var response = POSTJsonDto<CheckTopUpOutput>("/TransferApi/CheckTopUp", () => GetData(input));
            return response;
        }

        public TopUpOutput TopUp(TopUpInput input)
        {
            var response = POSTJsonDto<TopUpOutput>("/TransferApi/TopUp", () => GetData(input));
            return response;
        }
    }
}
