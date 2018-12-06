using DeKee.DomainContext.ResultCodes;
using CreditFront.AdminPanel.Models.Dto;
using CreditFront.Web.Controllers;
using CreditFront.Web.Structs;
using CreditFront.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CreditFront.AdminPanel.Controllers.Api
{
    public class SecurityApiController : BaseController
    {

        [HttpPost]
        public JsonResult Authenticate(AuthenticateInput input)
        {
            FormsAuthentication.SignOut();

            var user = FuncWithDbContext(data => data.OrganizationUsers.FirstOrDefault(u => u.Login == input.Login));

            if(user != null && user.PasswordHash == SecurityHelper.GetHash(input.Password))
            {
                FormsAuthentication.SetAuthCookie(input.Login, true);
                CurrentUser = new UserModel(); // ToDo Mapper.Map(user)
                return ApiResult(new AuthenticateOutput() { });
            }//TODO auth logic
            else
            {
                return ApiError(
                    new Web.Dto.Api.ApiError()
                    {
                        Code = SecurityResultCodes.Authentication.InvalidLoginOrPassword
                    }
                );
            }
        }
    }
}