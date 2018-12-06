using B4U.DomainContext.Services.CryptoTransfer.Dto;
using PlainElastic.Net.WebAppMVC.Dto.Api;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Utils.Extensions
{
    public static class DtoExtensions
    {
        public static WalletModel Map(this WalletDto walletDto)
        {
            return new WalletModel()
            {
                Address = walletDto.address,
                Balance = walletDto.balance,
                Currency = walletDto.currency,
                IsMain = walletDto.isMain,
                Name = walletDto.name
            };
        }

        public static ApiResultWrapper GetApiResultWrapper(this TopUpOutput output)
        {
            return new ApiResultWrapper()
            {
                Data = string.IsNullOrEmpty(output.ResultCode) || output.ResultCode == "0" 
                    ? output 
                    : null,
                Error = !(string.IsNullOrEmpty(output.ResultCode) || output.ResultCode == "0") 
                    ? new ApiError()
                    {
                        Code = output.ResultCode,
                        Description = output.ResultDescription
                    }
                    : null
            };
        }
    }
}