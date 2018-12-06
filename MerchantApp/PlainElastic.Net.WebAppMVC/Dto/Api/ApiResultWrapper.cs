using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;

namespace PlainElastic.Net.WebAppMVC.Dto.Api
{
    public class ApiWrapper<T>
    {
        public ApiWrapper()
        {
        }

        public ApiWrapper(T data)
        {
            Data = data;
        }

        public ApiError Error { get; set; }
        public T Data { get; set; }
        public object AdditionalData { get; set; }
    }
    public class ApiResultWrapper
    {
        public ApiError Error { get; set; }
        public Model Data { get; set; }
        public object AdditionalData { get; set; }
    }
}