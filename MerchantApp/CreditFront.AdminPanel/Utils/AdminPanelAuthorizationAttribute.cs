using CreditFront.AdminPanel.Controllers;
using CreditFront.Web.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CreditFront.AdminPanel.Utils
{
    public class AdminPanelAuthorizeAttribute : DomainAuthorizeAttribute
    {
        protected override string GetLoginFormUrl(AuthorizationContext requestContext)
        {
            return "/Security/Login";
        }
    }
}