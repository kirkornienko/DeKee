using DeKee.Dao.Base;
using DeKee.DomainContext.Base;
using DeKee.DomainContext.Localization;
using DeKee.DomainContext.Services;
using DeKee.DomainContext.Services.CryptoTransfer;
using DeKee.DomainContext.Services.CryptoTransfer.Dto;
using DeKee.ElasticContext.Base;
using CreditFront.Web.Dto.Api;
using PlainElastic.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace CreditFront.Web.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            _serviceFactory = new HandyApplicationServiceFactory();
            HandyContext = new HandyContext();
            //_userRepostory = HandyContext.ElasticRepositoryHost.GetRepostory<OrganizationUser>();
            _cryptoTransferService = _serviceFactory.GetService<CryptoTransferApi>();
            _exchangeService = _serviceFactory.GetService<ExchangeApi>();
        }
        protected HandyApplicationServiceFactory _serviceFactory { get; set; }
        protected HandyContext HandyContext { get; set; }
        //protected readonly IRepostory<OrganizationUser> _userRepostory;
        protected readonly ICryptoTransferService _cryptoTransferService;
        protected readonly ExchangeApi _exchangeService;
        private List<string> messages = null;

        protected string L(string code)
        {
            return Default.L(code);
        }
        protected virtual T FuncWithDbContext<T>(Func<BaseDataContext, T> func)
        {
            using (var dataContext = new BaseDataContext())
            {
                return func(dataContext);
            }
        }

        protected virtual void ActionWithDbContext(params Action<BaseDataContext>[] actions)
        {
            try
            {
                using (var dataContecxt = new BaseDataContext())
                {
                    foreach (var action in actions)
                    {
                        action(dataContecxt);
                    }
                    dataContecxt.SaveChanges();
                }
            }
            catch(Exception e)
            { }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            var result = base.BeginExecute(requestContext, callback, state);
            _serviceFactory.SessionId = Session?.SessionID ?? Guid.Empty.ToString();
            try
            {
                if (isDomainAuthorized && CurrentToken == null)
                {
                    //GetB4UToken();
                }
                else
                {
                    //requestContext.HttpContext.
                }
            }
            catch
            { }

            return result;
        }


        protected void initMessagesMicroLog()
        {
            messages = new List<string>();
        }
        protected void WriteMessageMicroLog(string message)
        {
            if (messages == null) return;
            messages.Add(message);
        }
        protected bool checkMessageMicroLog(Func<List<string>, bool> prdicate)
        {
            return messages != null && prdicate(messages);
        }
        //private void AddLocalizations(Dictionary<string, string> pairs)
        //{
        //    if (pairs == null) pairs = new Dictionary<string, string>();
        //    pairs.Add(ActionCodes.FinalizeProfile, "You have to finalize your B4U Merchant Account");
        //}
        //private void AddActionLinks(Dictionary<string, string> pairs)
        //{
        //    if (pairs == null) pairs = new Dictionary<string, string>();
        //    pairs.Add(ActionCodes.FinalizeProfile, nameof(CryptoBoxController.MerchantWizard));
        //}
        protected bool Ok(ConfirmOtpOutput resp)
        {
            return BasicOk(resp) && !string.IsNullOrEmpty(resp.token);
        }

        protected bool Ok(SignInOutput response)
        {
            return BasicOk(response) && !string.IsNullOrEmpty(response.token);
        }
        protected static bool BasicOk(BaseCryptoTransferOutput resp)
        {
            return resp != null && resp.IsOk();
        }

        //protected string getDeviceId()
        //{
        //    return CurrentUser.Id;
        //}
        //protected string GetB4UToken()
        //{
        //    var response = _cryptoTransferService.SignIn(new SignInInput()
        //    {
        //        deviceId = getDeviceId(),
        //        phoneNumber = CurrentUser.PhoneNumber,
        //        password = CurrentUser.B4UPassword
        //    });

        //    if (!Ok(response))
        //    {
        //        throw new ApplicationException("unable authorize at b4u account");
        //    }
        //    CurrentToken = response.token;

        //    return response.token;
        //}
        //protected virtual CabinetModel InitModel()
        //{
        //    CabinetModel m = new CabinetModel()
        //    {
        //        ActionLinks = new Dictionary<string, string>(),
        //        Data = new List<string>(),
        //        Localization = new Dictionary<string, string>()
        //    };

        //    AddActionLinks(m.ActionLinks);
        //    AddLocalizations(m.Localization);

        //    return m;
        //}

        public Structs.UserModel CurrentUser
        {
            set
            {
                Session["CurrentUser"] = value;
            }
            get
            {
                return Session["CurrentUser"] as Structs.UserModel;
            }
        }

        protected virtual JsonResult ApiResult<T>(T output, ApiError error = null, object data = null)
        {
            return Json(new ApiWrapper<T>()
            {
                AdditionalData = data,
                Data = output,
                Error = error
            });
        }

        protected virtual JsonResult ApiError<TError>(TError error)
            where TError: ApiError
        {
            return Json(new ApiWrapper<object>()
            {
                AdditionalData = null,
                Data = new { },
                Error = error
            });
        }

        IJsonSerializer GetJsonSerializer()
        {
            return new JsonNetSerializer();
        }

        public string CurrentToken
        {
            set
            {
                Session["CurrentToken"] = value;
            }
            get
            {
                return Session["CurrentToken"] as string;
            }
        }
        public bool isDomainAuthorized { get; set; }

    }
}