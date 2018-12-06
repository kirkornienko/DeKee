using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.ResultCodes
{
    public static class SecurityResultCodes
    {
        public static class Authentication
        {
            public static readonly string Ok = "Ok";
            public static readonly string InvalidLoginOrPassword = "InvalidLoginOrPassword";
            public static readonly string SessionExpired = "SessionExpired";
            public static readonly string UserAccountIsBlocked = "UserAccountIsBlocked";
            public static readonly string UserAccountIsDeleted = "UserAccountIsDeleted";
        }

    }
}
