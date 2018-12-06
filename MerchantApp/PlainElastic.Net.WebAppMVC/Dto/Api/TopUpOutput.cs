using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class TopUpOutput: Models.CryptoBox.Model
    {
        public TopUpOutput()
        {

        }
        public TopUpOutput(ReserveTransferOutput result)
        {
            if (result.data == null) return;

            this.ResultCode = result.data.error_code;
            this.ResultDescription = result.data.error_txt;

            this.PinData = new string[]{ result.data.pin_based,
                result.data.pin_code,
                result.data.pin_ivr,
                result.data.pin_serial,
                result.data.pin_value,
                result.data.pin_option_3,
                result.data.pin_option_2,
                result.data.pin_option_1}.Where(str => !String.IsNullOrEmpty(str)).ToArray();
            this.PinData = this.PinData.Any() ? this.PinData : null;

            this.AuthKey = result.data.authentication_key;
        }

        public string ResultCode { get;  set; }
        public string ResultDescription { get;  set; }
        public string[] PinData { get; set; }
        public string AuthKey { get; set; }
    }
}