using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CreditFront.AdminPanel.Models.Dto
{
    [DataContract]
    public class AuthenticateInput
    {
        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}