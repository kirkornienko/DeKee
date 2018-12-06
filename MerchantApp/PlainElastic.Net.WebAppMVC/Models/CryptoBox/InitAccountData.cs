using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlainElastic.Net.WebAppMVC.Models.CryptoBox
{
    public class LogInData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class InitAccountData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}