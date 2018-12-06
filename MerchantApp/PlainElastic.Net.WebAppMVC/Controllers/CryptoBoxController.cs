using B4U.Dao.Base;
using B4U.DomainContext.Services;
using B4U.DomainContext.Services.CryptoTransfer;
using B4U.DomainContext.Services.CryptoTransfer.Dto;
using B4U.ElasticContext.Base;
using PlainElastic.Net.WebAppMVC.Models;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;
using PlainElastic.Net.WebAppMVC.Utils.Attributes;
using PlainElastic.Net.WebAppMVC.Utils.Extensions;
using PlainSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlainElastic.Net.WebAppMVC.Controllers
{
    public class MerchantApiController : CryptoBoxController
    {

        [HttpPost]
        [B4UAuthorize]
        public ActionResult Merchants(string searchQuery)
        {
            var items = string.IsNullOrEmpty(searchQuery)
                ? _userRepostory.GetList()
                : _userRepostory.Search(searchQuery);

            foreach (var item in items)
            {
                item.Data = null;
            }

            return new JsonResult()
            {
                Data = items.ToArray()
            };
        }
    }

    public class CryptoBoxController : BaseController
    {

        protected ElasticClient<UserModel> _elasticClient;
        protected readonly IRepostory<Branch> _branchRepository;
        protected readonly IRepostory<Region> _regionRepository;

        //private readonly IRepostory<UserModel> _userRepository;
        string indexName = nameof(UserModel);


        public CryptoBoxController()
        {
            _elasticClient = new ElasticClient<UserModel>("localhost", 9200);

            _branchRepository = HandyContext.ElasticRepositoryHost.GetRepostory<Branch>();
            _regionRepository = HandyContext.ElasticRepositoryHost.GetRepostory<Region>();
        }
        // GET: CryptoBox
        public ActionResult RegisterMerchant()
        {
            return View();
        }

        public ActionResult MerchantWizard()
        {
            return View(CurrentUser);
        }

        [HttpGet]
        [DomainAuthorize]
        public ActionResult Merchants()
        {
            return View();
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult List(string searchQuery)
        {
            var items = string.IsNullOrEmpty(searchQuery)
                ? _userRepostory.GetList()
                : _userRepostory.Search(searchQuery);

            return PartialView("List", items.ToArray());
        }
        public ActionResult Merchant()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InitAccount(InitAccountData Init)
        {
            if (Init.Password != Init.ConfirmPassword)
                return View("Result", model: "Enter your password twice!");
            var user = new Models.UserModel()
            {
                Login = Init.UserName,
                Password = Init.Password,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Confirm = false
            };


            user.ComputeId();
            if (!insertToDb(user, out string errorcode))
            {
                return View("Result", model: errorcode);
            }
            CurrentUser = user;
            return View("MerchantWizard", user);
        }

        private bool insertToDb(UserModel user, out string errorcode)
        {
            errorcode = null;
            try
            {
                //var existing_user = FuncWithDbContext(db => db.Users.FirstOrDefault(u => u.Login == user.Login));
                //if (existing_user != null)
                //{
                //    errorcode = "Such user already exists";
                //    return false;
                //}

                //B4UDataContext b4U = new B4UDataContext();

                //var isEmailInUse = b4U.Execute(
                //    B4UQueryLib.Select.ClientEmailInUse(out string paramName),
                //    p => p.AddWithValue(paramName, user.Login),
                //    r => r.Read() ? r[0].ToString() == "1" : false
                //);

                //if (isEmailInUse)
                //{
                //    errorcode = "Such user already exists";
                //    return false;
                //}
                //var entity = AutoMapper.Mapper.Map<B4U.Base.Entities.User.User>(user);
                //ActionWithDbContext(db => db.Users.Add(entity));

                //var result = _elasticClient.Index(
                //                new IndexCommand(indexName, "_doc", user.Id),
                //                user);
                //user.Id = result._id;

                return true;
            }
            catch (Exception e)
            {
                errorcode = e.Message;
                return false;
            }
        }

        [DomainAuthorize]
        public ActionResult MerchantConfiguration()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginMerchant()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LoginMerchant(LogInData logInData)
        {

            FormsAuthentication.SignOut();
            CurrentToken = null;
            
            Queries.QueryBuilder<UserModel> _q = new Queries.QueryBuilder<UserModel>().Size(1000);
            _q.Query(q => q.Match(t => t.Field("Login.keyword").Query(logInData.UserName)));
            var searchResult = _elasticClient.Search(new SearchCommand(indexName, "_doc"), _q);

            if (searchResult.hits.hits.Any())
            {
                var user = searchResult.hits.hits.First()._source;
                if(user.Password != logInData.Password)
                    return View("Result", model:"Invalid password");

                FormsAuthentication.SetAuthCookie(logInData.UserName, true);
                CurrentUser = searchResult.hits.hits.First()._source;
                CurrentUser.Id = searchResult.hits.hits.First()._id;
            }
            else
            {
                return View("Result", model:"Invalid login or password");

            }
            return RedirectToAction("MerchantCabinet");
        }

        [DomainAuthorize]
        public ActionResult MerchantCabinet()
        {
            CabinetModel model = InitModel();
            if(!CurrentUser.Confirm)
                AddNotifications(model);

            return View(model);
        }

        private void AddNotifications(CabinetModel model)
        {
            if (model.Data == null) model.Data = new List<string>();
            model.Data.Add(ActionCodes.FinalizeProfile);
        }

        //public ActionResult MerchantDetails(string Id)
        //{
        //    var entity = FuncWithDbContext(d => d.Users.FirstOrDefault(u => u.Id == Id));
        //    ViewBag.AggreementId = entity?.AggreementId;
        //    ViewBag.CertificateId = entity?.CertificateId;
        //    var merchant = _userRepostory.GetInstance(Id);
        //    return View(merchant);
        //}

        //public ActionResult MerchantProfile()
        //{
        //    var entity = FuncWithDbContext(d => d.Users.FirstOrDefault(u => u.Id == CurrentUser.Id));
        //    if(entity == null && CurrentUser.IsPrimary)
        //    {
        //        entity = FuncWithDbContext(d => d.Users.Single(u => u.IsPrimary));
        //    }
        //    ViewBag.AggreementId = entity?.AggreementId;
        //    ViewBag.CertificateId = entity?.CertificateId;
        //    return View(CurrentUser);
        //}

        //public ActionResult GetDocument(string docId)
        //{
        //    var docEntity = FuncWithDbContext(d => d.UserFiles.First(f => f.Id == docId));
        //    return File(docEntity.Data, docEntity.Type, docEntity.FileName);

        //}

        //[HttpPost]
        //public ActionResult UploadCertificate(HttpPostedFileBase file)
        //{
        //    var entity = FuncWithDbContext(d => d.Users.FirstOrDefault(u => u.Id == CurrentUser.Id));
        //    //file.InputStream.Read
        //    return Json(new { data = file.FileName });
        //}

        //[HttpPost]
        //public ActionResult UploadAgreement(HttpPostedFileBase file)
        //{
        //    var entity = FuncWithDbContext(d => d.Users.FirstOrDefault(u => u.Id == CurrentUser.Id));
        //    //file.InputStream.Read
        //    return Json(new { data = file.FileName });
        //}
        public ActionResult UploadDocument(HttpPostedFileBase agreementFile, HttpPostedFileBase certificateFile)
        {
            return PartialView("");
        }
        //public ActionResult CheckContacts(string phoneNumber, string b4uPassword, bool? useMerchantPassword)
        //{
        //    if(!string.IsNullOrEmpty(phoneNumber) && CurrentUser.PhoneNumber == phoneNumber)
        //    {
        //        return Json(new { data = "Ok" });
        //    }
        //    try
        //    {
        //        CurrentUser.PhoneNumber = phoneNumber.Replace("+","");
        //        CurrentUser.B4UPassword = CurrentUser.Password;

        //        CurrentUser.Confirm = false;
        //        //else/

        //        //if (!useMerchantPassword.HasValue || !useMerchantPassword.Value)
        //        {
        //            var result = _cryptoTransferService.SignIn(new SignInInput()
        //            {
        //                deviceId = getDeviceId(),
        //                password = CurrentUser.B4UPassword,
        //                phoneNumber = CurrentUser.PhoneNumber
        //            });
        //            if(!Ok(result))
        //            {
        //                if (result.responseText == "WrongPassword")
        //                {
        //                    return PartialView("ResultInfo", "This phone number already in use...");
        //                }

        //                //if (string.IsNullOrEmpty(b4uPassword) && !useMerchantPassword.Value)
        //                //{
        //                //    return PartialView("B4UPassword", result.responseText);
        //                //}

        //                if (result.responseText == "ClientNotFoundByPhone")
        //                {
        //                    return Json(new { data = "Ok" });
        //                }
        //                else
        //                {
        //                    return PartialView("ResultInfo", "You can't use this phone number right now. Try again later");
        //                }

        //            }
        //            else
        //            {
        //                //return PartialView("ResultInfo", "This phone number already in use...");
        //                CurrentToken = result.token;
        //                var PhoneNumberIsInUse = FuncWithDbContext(d => d.Users.Any(u => u.PhoneNumber == CurrentUser.PhoneNumber && u.Id != CurrentUser.Id));

        //                if (PhoneNumberIsInUse)
        //                {
        //                    return PartialView("ResultInfo", "This phone number already in use...");
        //                }
        //                CurrentUser.Confirm = true;
        //            }
        //        }
        //        //else if(useMerchantPassword.Value)
        //        //{
        //        //}
        //        //else
        //        //{
        //        //    return PartialView("B4UPassword", "Enter password or click the checkbox");
        //        //}
        //        return Json(new { data = "Ok" });
        //    }
        //    catch (ApplicationException e)
        //    {
        //        if (e.InnerException == null)
        //        {
        //            return PartialView("ResultInfo", e.Message);
        //        }
        //        else
        //        {
        //            if(e.InnerException.Message == "226")
        //            {
        //                return PartialView("B4UPassword", "");
        //            }
        //            return PartialView("ResultInfo", e.Message);
        //        }
        //    }
        //    finally
        //    {
        //        CurrentUser = CurrentUser;
        //    }
        //}
        //public ActionResult EditProfile(Dictionary<string, string> Data, HttpPostedFileBase agreementFile, HttpPostedFileBase certificateFile)
        //{
        //    UserModel currentUser = CurrentUser;
        //    if(currentUser == null)
        //    {
        //        currentUser = _userRepostory.GetInstance(Data["Id"]);
        //        CurrentUser = currentUser;
        //    }

        //    try
        //    {
        //        currentUser.ModifiedDate = DateTime.Now;
        //        //CurrentUser.Data = Data;

        //        const string phoneNumberKey = "ContactPhoneNumber";
        //        const string nameKey = "Name";
        //        readPhone(Data, phoneNumberKey);
        //        readName(Data, nameKey);

        //        B4U.Base.Entities.User.UserFile entityFileAgr = null;
        //        if (agreementFile != null)
        //        {
        //            entityFileAgr = getFileEntity(agreementFile);
        //            //ActionWithDbContext(d => d.UserFiles.Add(entityFileAgr));
        //        }
        //        B4U.Base.Entities.User.UserFile entityFileCert = null;
        //        if (certificateFile != null)
        //        {
        //            entityFileCert = getFileEntity(certificateFile);
        //            //ActionWithDbContext(d => d.UserFiles.Add(entityFileCert));
        //        }

        //        CurrentUser.PhoneNumber = CurrentUser.PhoneNumber.Replace("+", "");

        //        base.ActionWithDbContext(d =>
        //        {
        //            var userEntity = d.Users.First(u => u.Id == CurrentUser.Id);
        //            userEntity.Aggreement = entityFileAgr;
        //            userEntity.Certificate = entityFileCert;

        //            CurrentUser.Data = CurrentUser.Data ?? new MerchantDataModel();
        //            userEntity.ModifiedDate = DateTime.Now;
        //            userEntity.DateCreated = DateTime.Now;
        //            userEntity.ContactPerson = CurrentUser.Data.ContactPerson = Data["ContactPerson"];
        //            userEntity.AgreementNumber = CurrentUser.Data.AgreementNumber = Data["AgreementNumber"];
        //            userEntity.AgreementNumberDate = CurrentUser.Data.AgreementNumberDate = Data["AgreementNumberDate"];
        //            userEntity.CertificateNumber = CurrentUser.Data.CertificateNumber = Data["CertificateNumber"];
        //            userEntity.CertificateDate = CurrentUser.Data.CertificateDate = Data["CertificateDate"];

        //            string regionId = Data["Region"];
        //            var searchResult = _regionRepository.Search(regionId.ToString());

        //            string region = searchResult.Any() 
        //                    ? searchResult.FirstOrDefault()?.Name ?? regionId 
        //                : regionId;

        //            userEntity.Country = CurrentUser.Country = region ?? CurrentUser.Country;
        //            //userEntity.BrunchNumber = CurrentUser.BrunchNumber = Data["BrunchNumber"];
        //            userEntity.BrunchAddress = CurrentUser.BrunchAddress = Data["BrunchAddress"];

        //            userEntity.PhoneNumber = CurrentUser.PhoneNumber;
        //            userEntity.FirstName = CurrentUser.FirstName;
        //            userEntity.LastName = CurrentUser.LastName;
        //            userEntity.B4UPassword = CurrentUser.B4UPassword;

        //            d.SaveChanges();
        //        });
        //        FormsAuthentication.SetAuthCookie(CurrentUser.Login, true);

        //        if (!currentUser.Confirm)
        //        {
        //            SignUpInB4U(currentUser);
        //            currentUser.Confirm = true;
        //            CurrentUser = currentUser;
        //            return RedirectToAction(nameof(CryptoBoxController.ConfirmB4UAccount));
        //        }
                
        //        return RedirectToAction(nameof(CryptoBoxController.GenerateWallet));
        //    }
        //    catch (Exception e)
        //    {
        //        return View("Result", (e.InnerException ?? e).Message);
        //    }
        //    finally
        //    {
        //        var result = _elasticClient.Index(
        //            new IndexCommand(indexName, "_doc", currentUser.Id),
        //            currentUser);
        //        CurrentUser = currentUser;


        //    }
        //}

        private void SignUpInB4U(UserModel currentUser)
        {
            string pass = Guid.NewGuid().ToString();
            currentUser.B4UPassword = string.IsNullOrEmpty(currentUser.B4UPassword) 
                ? pass.Replace("-", "").Substring(0, 17)
                : currentUser.B4UPassword;
            
            var resp = _cryptoTransferService.SignUp(new SignUpInput()
            {
                deviceId = getDeviceId(),
                email = currentUser.Login,
                firstName = "Merchant",
                lastName = currentUser.FirstName + " " + currentUser.LastName,
                password = currentUser.B4UPassword,
                phoneNumber = currentUser.PhoneNumber,
                token = null
            });
            if (!BasicOk(resp))
            {
                throw new ApplicationException("unable signup at b4u account. " + resp.responseText, new Exception(resp.responseCode.ToString()));
            }
            else
            {
                
            }
        }

        private B4U.Base.Entities.User.UserFile getFileEntity(HttpPostedFileBase agreementFile)
        {
            var entityFile = new B4U.Base.Entities.User.UserFile(); //FuncWithDbContext(d => d.UserFiles.Create());
            entityFile.Id = Guid.NewGuid().ToString();
            entityFile.DateCreated = DateTime.Now;
            entityFile.ModifiedDate = DateTime.Now;
            entityFile.FileName = agreementFile.FileName;
            entityFile.Type = agreementFile.ContentType;
            entityFile.Length = agreementFile.ContentLength;

            var buffer = new byte[agreementFile.ContentLength];
            using (agreementFile.InputStream)
            {
                agreementFile.InputStream.Read(buffer, 0, buffer.Length);
                entityFile.Data = buffer;
            }
            return entityFile;
        }

        [HttpPost]
        public ActionResult Branches(string regionId)
        {
            var items = _branchRepository.GetList().Where(b => b.RegionId == regionId);
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult Regions()
        {
            var items = _regionRepository.GetList();
            if (CurrentUser != null)
            {
                ViewBag.Country = CurrentUser.Country;
            }
            return PartialView(items);
        }

        [HttpGet]
        //[DomainAuthorize]
        public ActionResult ConfirmB4UAccount()
        {
            return View();
        }

        [HttpPost]
        //[DomainAuthorize]
        public ActionResult ConfirmB4UAccount(string code)
        {
            if (CurrentUser == null)
            {
                return new RedirectResult("/Home/Index/");
            }

            var resp = _cryptoTransferService.ConfirmOtp(new ConfirmOtpInput()
            {
                deviceId = getDeviceId(),
                otpCode = code,
                phoneNumber = CurrentUser.PhoneNumber,
                token = null
            });
            if(Ok(resp))
            {
                CurrentToken = resp.token;
                return Json(new { data = "Ok" });
            }
            return new ContentResult()
            {
                Content = (resp.responseText ?? "Success")
            };
        }

           

        private void readName(Dictionary<string, string> data, string nameKey)
        {
            string name;
            string[] split;
            if (data.TryGetValue(nameKey, out name))
            {
                split = name.Split(' ');
                UserModel currentUser = CurrentUser;
                if (split.Length > 0)
                    currentUser.FirstName = split[0];
                else currentUser.FirstName = name;

                if (split.Length > 1)
                    currentUser.LastName = split[1];
                else currentUser.LastName = name;

                CurrentUser = currentUser;
            }
        }

        private void readPhone(Dictionary<string, string> Data, string phoneNumberKey)
        {
            string phone;
            if (Data.TryGetValue(phoneNumberKey, out phone))
            {
                UserModel currentUser = CurrentUser;
                currentUser.PhoneNumber = phone.Replace("+", "");
                CurrentUser = currentUser;
            }
        }

        public ActionResult AddUserInfo(string Id, Dictionary<string, string> info)
        {
            return Redirect("/Home/");
        }

        [DomainAuthorize]
        public ActionResult WalletInfo()
        {
            if (!string.IsNullOrEmpty(CurrentUser.WalletAddress))
            {
                var walletsResult = _cryptoTransferService.GetWallets(
                    new GetWalletsInput()
                    {
                        deviceId = getDeviceId(),
                        token = CurrentToken ?? GetB4UToken()
                    });
                return PartialView(walletsResult.IsOk() 
                    ? walletsResult.wallets.Select(w => w.Map()).ToArray()
                    : new WalletModel[] { });
            }
            return PartialView("ResultInfo", "There are no crypto wallets generated for your B4U Account");
        }
        //[DomainAuthorize]
        //public ActionResult GenerateWallet()
        //{
        //    if (string.IsNullOrEmpty(CurrentUser.WalletAddress))
        //    {
        //        CurrentToken = null;
        //        var walletsResult = _cryptoTransferService.GetWallets(
        //            new GetWalletsInput()
        //            {
        //                deviceId = getDeviceId(),
        //                token = CurrentToken ?? GetB4UToken()
        //            });
        //        if (!BasicOk(walletsResult) || walletsResult.wallets == null || walletsResult.wallets.Count() == 0)
        //        {
        //            //throw new ApplicationException("unable to generate wallet. " + result?.responseText ?? "Uknown reason");

        //            var result = _cryptoTransferService.CreateWallet(new CreateWalletInput()
        //            {
        //                walletName = Guid.NewGuid().ToString(),
        //                deviceId = getDeviceId(),
        //                isMain = true,
        //                phone = CurrentUser.PhoneNumber,
        //                currency = "BFY",
        //                token = CurrentToken ?? GetB4UToken()
        //            });
        //            if (!BasicOk(result))
        //            {
        //                throw new ApplicationException("unable to generate wallet. " + result?.responseText ?? "Uknown reason");
        //            }
        //            CurrentUser.WalletAddress = result.address;
        //            CurrentUser.MnemonicList = result.mnemonicList;
        //            CurrentUser.PrivateKey = result.privateKey;
        //        }
        //        else
        //        {
        //            WalletDto wallet = walletsResult.wallets.OrderByDescending(w => w.balance).FirstOrDefault();
        //            if (wallet != null)
        //            {
        //                CurrentUser.WalletAddress = wallet.address;
        //                CurrentUser.MnemonicList = null;
        //                CurrentUser.PrivateKey = "";
        //            }
        //        }
        //        var elasticResult = _elasticClient.Index(
        //            new IndexCommand(indexName, "_doc", CurrentUser.Id),
        //        CurrentUser);
        //        ActionWithDbContext(d =>
        //        {
        //            var user = d.Users.FirstOrDefault(u => u.Id == CurrentUser.Id);
        //            user.WalletAddress = CurrentUser.WalletAddress;
        //            d.SaveChanges();
        //        });
        //    }
        //    return View("GenerateWallet", (object)CurrentUser.WalletAddress);
        //}

    }
}