using DeKee.DomainContext.Services.TechnicalMerchant.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services.TechnicalMerchant
{
    public interface ITechnicalMerhant
    {
        TopUpOutput TopUp(TopUpInput input);
        CheckTopUpOutput CheckTopUp(CheckTopUpInput input);
    }
}
