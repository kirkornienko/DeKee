using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeKee.DomainContext.Localization
{
    public static class Default
    {
        public static string L(string code)
        {
            var result = code;
            try
            {
                var m = typeof(Default).GetMethod(code);
                if (m != null)
                {
                    result = m.Invoke(null, null).ToString();
                }
            }
            catch
            { }

            return result;
        }

        public static string WalletHasNoEnoughBeefyMoney()
        {
            return "Wallet has no enough beefy money";
        }
    }
}