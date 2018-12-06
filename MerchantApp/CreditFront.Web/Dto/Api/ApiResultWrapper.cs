using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CreditFront.Web.Dto.Api
{
    [DataContract]
    public class ApiWrapper<T>
    {
        public ApiWrapper()
        {
        }

        public ApiWrapper(T data)
        {
            Data = data;
        }

        [DataMember(Name = "error", EmitDefaultValue = false)]
        public ApiError Error { get; set; }
        [DataMember(Name = "data", EmitDefaultValue = true)]
        public T Data { get; set; }
        [DataMember(Name = "additionalData", EmitDefaultValue = false)]
        public object AdditionalData { get; set; }
    }
    public class ApiResultWrapper
    {
        public ApiError Error { get; set; }
        public Model Data { get; set; }
        public object AdditionalData { get; set; }
    }
}