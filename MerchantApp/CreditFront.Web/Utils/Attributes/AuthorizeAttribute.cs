using CreditFront.Web.Controllers;
using CreditFront.Web.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CreditFront.Web.Utils.Attributes
{
    public sealed class DeKeeAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var key = filterContext.HttpContext.Request.Cookies["X-B4U-AUTH"];
            if (key == null || key.Value == "1")//TODO implement normal authorization
            {
                //base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new Dto.Api.ApiResultWrapper()
                    {
                        Error = new Dto.Api.ApiError()
                        {
                            Code = Dto.Api.ApiError.Codes.SessionExpired,
                            Description = "API client is not authorized"
                        }
                    }
                };
                return;
            }
        }
    }
    public class DomainAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["CurrentUser"] as UserModel;
            if (user != null)
            {
                if (filterContext.Controller is BaseController)
                {
                    (filterContext.Controller as Controllers.BaseController).isDomainAuthorized = true;
                }

                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult(GetLoginFormUrl(filterContext));
                return;
            }
        }


        protected virtual string GetLoginFormUrl(AuthorizationContext filterContext)
        {
            return "/Home/Index/";
        }
    }
}