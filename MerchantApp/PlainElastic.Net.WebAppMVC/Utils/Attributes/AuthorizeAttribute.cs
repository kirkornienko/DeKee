using PlainElastic.Net.WebAppMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Utils.Attributes
{
    sealed class B4UAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
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
                            Code = Dto.Api.ApiError.Codes.B4UAuthorize,
                            Description = "API client is not authorized"
                        }
                    }
                };
                return;
            }
        }
    }
    sealed class DomainAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.Session["UserData"] as UserModel;
            if (user != null)
            {
                if (filterContext.Controller is Controllers.BaseController)
                {
                    (filterContext.Controller as Controllers.BaseController).isDomainAuthorized = true;
                }

                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("/Home/Index/");
                return;
            }
        }
        
    }
}