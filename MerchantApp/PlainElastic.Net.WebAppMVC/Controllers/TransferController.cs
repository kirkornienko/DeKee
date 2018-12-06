using AutoMapper;
using B4U.Base;
using B4U.Dao.Base;
using B4U.DomainContext.Services;
using B4U.DomainContext.Services.CryptoTransfer;
using B4U.DomainContext.Services.CryptoTransfer.Dto;
using B4U.DomainContext.Services.Dto;
using B4U.ElasticContext.Base;
using PlainElastic.Net.Queries;
using PlainElastic.Net.WebAppMVC.Dto;
using PlainElastic.Net.WebAppMVC.Dto.Api;
using B4U.DomainContext.Localization;
using PlainElastic.Net.WebAppMVC.Models;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;
using PlainElastic.Net.WebAppMVC.Utils;
using PlainElastic.Net.WebAppMVC.Utils.Attributes;
using PlainElastic.Net.WebAppMVC.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Controllers
{
    public class TransferApiController : TransferController
    {
        private const string TransferErrorResultView = "TransferError";

        [HttpPost]
        [B4UAuthorize]
        public ActionResult Transfers(TransferSearchInput input)
        {
            input.Size = input.Size == 0 ? 1000 : input.Size;
            input.Merchant = input.SearchQuery;
            input.SearchQuery = null;
            var transfersModel = getTransfers(input).Select(t => t.Id).ToArray();

            var data = FuncWithDbContext(d => d.Transfers.Where(t => transfersModel.Contains(t.Id)).ToArray());
            
            return new JsonResult()
            {
                Data = Wrap(data)
            };
        }

        private ApiWrapper<T> Wrap<T>(T transfersModel)
        {
            return new ApiWrapper<T>(transfersModel);
        }

        //private const string transferErrorResultCode = "TransferError";
        [HttpPost]
        [B4UAuthorize]
        public ActionResult TopUp(TopUpInput input)
        {
            return TopUp(input.SenderNumber, input.ReceiverNumber, input.Product, input.TransactionId);
        }
        private ActionResult TopUp(string senderNumber, string receiverNumber, decimal product, string transactionId)
        {
            var result = ExecuteReservedTopUp(
                new ReserveTransferInput()
                    {
                        DestinationNumber = receiverNumber,
                        SenderNumber = senderNumber,
                        Product = product
                    },
                _transferToService.TopUpPrepare()
            );
            return new JsonResult()
            {
                Data = (new TopUpOutput(result)).GetApiResultWrapper()
            };
        }

        [HttpPost]
        [B4UAuthorize]
        public ActionResult CheckTopUp(CheckTransferServiceInput input)
        {
            return CheckTransferServiceForApi(input.PhoneNumber, input.CustomerId, input.ReceiverId);
        }        
        private ActionResult CheckTransferServiceForApi(string input, string customerId = null, string receiverId = null)
        {
            try
            {
                CustomerModel customer;
                CheckResponse resp;
                TopUpCheck(ref input, ref customerId, receiverId, out customer, out resp);
                if (TransferToOk(resp))
                {
                    TransferToServiceModel model = getTransferToModel(customer, resp);
                    var primaryWallet = getPrimaryWallet();
                    return CheckTransferResult(customerId, model, primaryWallet);
                }
                return TransferResultView(TransferErrorResultView, resp.error_txt);
            }
            catch (Exception e)
            {
                return TransferExceptionResult(e);
            }
        }

        

        private ActionResult TransferExceptionResult(Exception e)
        {
            //string message = e.ToString();
            return new JsonResult()
            {
                Data = ApiResult(e)
            };
        }

        private ActionResult TransferResultView(string transferResultView, string message)
        {
            return new JsonResult()
            {
                Data = new { code = transferResultView, message = message }
            };
        }        
        private ActionResult CheckTransferResult(string customerId, TransferToServiceModel model, string primaryWallet)
        {
            return new JsonResult()
            {
                Data = ApiResult(model, primaryWallet)
            };
        }

        private ApiResultWrapper ApiResult(TransferToServiceModel model, string primaryWallet)
        {
            return new ApiResultWrapper()
            {
                Data = model,
                AdditionalData = new { Wallet = primaryWallet }
            };
        }

        private ApiResultWrapper ApiResult(Exception e)
        {
            return new ApiResultWrapper()
            {
                Error = new ApiError(e)
            };
        }
        private ApiResultWrapper ApiResult(Model model)
        {
            return new ApiResultWrapper()
            {
                Data = model
            };
        }
    }
    public class TransferController : BaseController
    {
        private const string searchInputKey = "SearchTransfers";
        protected readonly IRepostory<TransferModel> _transferRepository;
        protected readonly IRepostory<CustomerModel> _customerRepostory;
        //protected readonly IRepostory<TopUpResponse> _topUpRepostory;
        protected readonly ITransferToService _transferToService;
        protected readonly IRepostory<Region> _regionRepository;

        public TransferController()
        {
            HandyContext.ElasticRepositoryHost.LogRequest += ElasticRepositoryHost_LogRequest;
            _transferRepository = HandyContext.ElasticRepositoryHost.GetRepostory<TransferModel>();
            _customerRepostory = HandyContext.ElasticRepositoryHost.GetRepostory<CustomerModel>();
            //_topUpRepostory = HandyContext.ElasticRepositoryHost.GetRepostory<TopUpResponse>();
            _transferToService = _serviceFactory.GetService<TransferToAPI>();

            _regionRepository = HandyContext.ElasticRepositoryHost.GetRepostory<Region>();
        }

        private bool ElasticRepositoryHost_LogRequest(object arg)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("ElasticRepositoryHost\n{0}", arg));

            return true;
            //throw new NotImplementedException();
        }

        [DomainAuthorize]
        public ActionResult Index(string searchQuery = null)
        {
            return View("History");
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult List(string searchQuery = null, int from=0, int size=1000)
        {
            var input = new TransferSearchInput()
            {
                SearchQuery = searchQuery,
                Merchant = CurrentUser.PhoneNumber,
                From = from,
                Size = size
            };

            Session[searchInputKey] = input;

            var transfers = getTransfers(input);
            
            return PartialView("List", transfers.ToArray());
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult SearchTransfers(TransferSearchInput input)
        {
            Session[searchInputKey] = input ?? new TransferSearchInput() { };
            IEnumerable<TransferModel> result = getTransfers(input);
            return Json(result);
        }

        protected IEnumerable<TransferModel> getTransfers(TransferSearchInput input)
        {
            IEnumerable<TransferModel> result = null;
            if (input == null)
            {
                result = _transferRepository.GetList();
            }
            else
            {
                Queries.QueryBuilder<TransferModel> _q = new Queries.QueryBuilder<TransferModel>()
                        .Sort(s => s.Field(t => t.Date, SortDirection.desc))
                        .Size(input.Size)
                        .From(input.From);
                _q = _q.Query(q => q.Bool(b => b.Must(m =>
                {
                    Query<TransferModel> resultQ = null;

                    if (!string.IsNullOrEmpty(input.SearchQuery))
                    {
                        resultQ = (resultQ ?? m).MultiMatch(t => t.Query(input.SearchQuery));
                    }

                    if (!string.IsNullOrEmpty(input.Merchant))
                    {
                        resultQ = (resultQ ?? m).Match(mt => mt.Field(t => t.Merchant.PhoneNumber).Query(input.Merchant));
                    }
                    return resultQ ?? m.MatchAll();
                })));
                var r = _transferRepository.CustomAction(c => c.Search(new SearchCommand(nameof(TransferModel), "_doc"), _q));
                result = r.Documents;// _transferRepository.Search(input.SearchQuery);
            }

            return result;
        }

        [HttpGet]
        [DomainAuthorize]
        public ActionResult ExportToExcel(string name=null)
        {
            var input = (Session[searchInputKey] as TransferSearchInput) ?? new TransferSearchInput() { };

            input.From = 0;
            input.Size = 10000;

            var data = getTransfers(input).Select(t => t.Id).ToArray();
            var entities = FuncWithDbContext(d => d.Transfers.Where(t => data.Contains(t.Id)).OrderByDescending(t => t.DateCreated).ToList());

            string[] columns = typeof(B4U.Base.Entities.Transfer.Transfer).GetProperties().Select(p => p.Name).ToArray();
            string Heading = name ?? "Transfers";
            string fileName = name ?? "Transfers.xlsx";
            byte[] filecontent = Utils.Extensions.ExcelExportHelper.ExportExcel(entities, Heading, true, columns);
            return File(filecontent, Utils.Extensions.ExcelExportHelper.ExcelContentType, fileName);
        }

        //private void writeData(IWorksheet worksheet, object[] data)
        //{
        //    if (data == null) return;

        //    var props = data[0].GetType().GetProperties().Where(p => p.CanRead).ToArray();
        //    int i = 0;
        //    int j = 1;
        //    foreach (var p in props)
        //    {
        //        var indexStr = ((char)(i + 'A'));
        //        var cellIndex = indexStr.ToString() + j.ToString();
        //        var val = (p.Name);
        //        worksheet.Range[cellIndex].Text = val;
        //        i++;
        //    }
        //    j++;

        //    foreach (var item in data)
        //    {
        //        i = 0;
        //        foreach (var p in props)
        //        {
        //            var indexStr = ((char)(i + 'A'));
        //            var cellIndex = indexStr.ToString() + j.ToString();
        //            worksheet.Range[cellIndex].Text = (p.GetValue(item) ?? "").ToString();
        //            i++;
        //        }
        //        j++;
        //    }    
        //}

        //private static void writeData(IWorksheet worksheet)
        //{

        //    //Adding text to a cell
        //    worksheet.Range["A1"].Text = "Hello World";
        //}

        [HttpPost]
        public virtual ActionResult GetTransferToInfo(string input, string receiverPhoneNumber = null, string receiverId = null)
        {
            try
            {
                CustomerModel customer;
                CheckResponse resp;
                string i = null;
                DirectTopUpCheck(ref input, ref i, receiverPhoneNumber, out customer, out resp);
                if (TransferToOk(resp))
                {
                    //resp.country
                    InsertRegion(resp);
                    if (CurrentUser != null)
                    {
                        CurrentUser.Country = resp.country;
                    }

                    TransferToServiceModel model = getTransferToModel(customer, resp);

                    return PartialView("TransferToServiceInfo", model);
                }
                return Json(new { result = "NotOk", data = resp });
            }
            catch (Exception e)
            {
                return PartialView("TransferError", e.ToString());
            }
        }

        private void InsertRegion(CheckResponse resp)
        {
            var country = _regionRepository.Search(resp.country);
            if (!country.Any())
            {
                _regionRepository.Push(new Region()
                {
                    Code = resp.countryid.ToString(),
                    Id = Guid.NewGuid().ToString(),
                    Name = resp.country
                });
            }
        }

        [HttpPost]
        [DomainAuthorize]
        public virtual ActionResult CheckTransferService(string input, string customerId = null, string receiverPhoneNumber = null, string receiverId = null)
        {
            try
            {
                CustomerModel customer;
                CheckResponse resp;
                DirectTopUpCheck(ref input, ref customerId, receiverPhoneNumber, out customer, out resp);
                if (TransferToOk(resp))
                {
                    TransferToServiceModel model = getTransferToModel(customer, resp);

                    if (string.IsNullOrEmpty(customerId))
                        return PartialView("ServiceInfo", model);
                    else
                        return PartialView("TransferToServiceInfo", model);
                }
                return Json(new { result = "NotOk", data = resp });
            }
            catch (Exception e)
            {
                return PartialView("TransferError", e.ToString());
            }
        }
        protected virtual bool TransferToOk(CheckResponse resp)
        {
            return resp.error_code == "0";
        }
        protected virtual void DirectTopUpCheck(ref string input, ref string customerId, string receiverPhoneNumber, out CustomerModel customer, out CheckResponse resp)
        {
            customer = null;
            if (!string.IsNullOrEmpty(receiverPhoneNumber))
            {
                var items = _customerRepostory.Search(receiverPhoneNumber);
                if (items.Any())
                {
                    customer = items.First();
                }
                else
                {
                    InitCustomer(out customerId, receiverPhoneNumber, out customer);
                }
                input = customer.PhoneNumber ?? input;
            }
            if (!string.IsNullOrEmpty(customerId) && customer == null)
            {
                customer = _customerRepostory.GetInstance(customerId);
                customer.Id = customerId;
            }
            input = input.Trim();
            input = input.Replace(" ", "").Replace("+", "");

            resp = _transferToService.TopUpCheck(input);
        }

        private static void InitCustomer(out string customerId, string receiverPhoneNumber, out CustomerModel customer)
        {
            customer = new CustomerModel()
            {
                PhoneNumber = receiverPhoneNumber,
                Id = Guid.NewGuid().ToString()
            };
            customerId = null;
        }

        protected virtual void TopUpCheck(ref string input, ref string customerId, string receiverId, out CustomerModel customer, out CheckResponse resp)
        {
            customer = null;
            if (!string.IsNullOrEmpty(receiverId))
            {
                customer = _customerRepostory.GetInstance(receiverId);
                customer.Id = receiverId;
                customerId = null;
                input = customer.PhoneNumber ?? input;
            }
            if (!string.IsNullOrEmpty(customerId))
            {
                customer = _customerRepostory.GetInstance(customerId);
                customer.Id = customerId;
            }
            input = input.Trim();
            input = input.Replace(" ", "").Replace("+", "");

            resp = _transferToService.TopUpCheck(input);
        }

        protected virtual TransferToServiceModel getTransferToModel(CustomerModel customer, CheckResponse resp)
        {
            var model = new TransferToServiceModel()
            {
                Country = resp.country,
                CountryId = resp.countryid,
                Operator = resp.@operator,
                OperatorId = resp.operatorid,
                PhoneNumber = resp.destination_msisdn,
                TopUpAmounts = resp.product_list.Split(',').Select(str => decimal.Parse(str)).ToArray(),
                ReceiverCurrency = resp.destination_currency,
                RetailCurrency = "EUR",
                ConnectionRate = resp.connection_status,
                WholesalePriceList = resp.wholesale_price_list.Split(',').Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToArray(),
                RetailPrices = resp.retail_price_list.Split(',').Select(str => decimal.Parse(str, CultureInfo.InvariantCulture)).ToArray()
                
            };
            if (customer != null)
            {
                customer.TransferToServiceInfo = model;
                _customerRepostory.Update(customer, customer.Id);
            }

            return model;
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult ConfirmMerchantToPerson(string transactionId)
        {
            var entity = FuncWithDbContext(d => d.Transfers.FirstOrDefault(t => t.Id == transactionId));
                var transfer = _transferRepository.GetInstance(transactionId);
            try
            {
                if (entity != null && transfer != null)
                {
                    ExecuteTransfer(ref transfer);
                    //entity = Mapper.Map<B4U.Base.Entities.Transfer.Transfer>(transfer);
                    entity.Status = TransferModel.Statuses.Executed;
                    entity.ModifiedDate = DateTime.Now;

                    //InsertTransferToDb(transfer);

                    return PartialView(transfer);
                }
                else
                {
                    return PartialView("TransferError", transactionId + " transaction not found");
                }
            }
            catch (Exception e)
            {
                transfer.Status = entity.Status = TransferModel.Statuses.Failed;
                
                return PartialView("TransferError", e.ToString());
            }
            finally
            {
                ActionWithDbContext(d => d.SaveChanges());
                _transferRepository.Update(transfer, transfer.Id);
            }
        }

        private void ExecuteTransfer(ref TransferModel transfer)
        {
            checkAndSignTransfer(ref transfer);
            
            
            ResolveAndExecuteTrnsferChain(ref transfer);
        }

        private void ResolveAndExecuteTrnsferChain(ref TransferModel transfer)
        {

            if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToPerson)
                MerchantToWallet(transfer);
            else if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToMobile)
                MerchantToMobile(transfer);

            transfer.Status = TransferModel.Statuses.Executed;
            transfer.Date = DateTime.Now;


        }

        private void MerchantToMobile(TransferModel transfer)
        {
            //var primaryMechant = ;
            
            //Merchant To Wallet
            var cryptoTransferResp = _cryptoTransferService.SendTransfer(new SendTransferInput()
            {
                amount = transfer.BFYAmount + transfer.B4UCommission,
                deviceId = getDeviceId(),
                fromAddress = CurrentUser.WalletAddress,
                toAddress = getPrimaryWallet(),
                gasLimit = transfer.GasLimit,
                gasPrice = transfer.GasAmount,
                token = CurrentToken ?? GetB4UToken()
            });
            if (!BasicOk(cryptoTransferResp))
            {
                throw new ApplicationException(cryptoTransferResp.responseText);
            }

            var resp = _transferToService.TopUpPrepare();

            var topUpRequest = new TopUpRequest()
            {
                ReservedId = resp.reserved_id,
                msisdn = "bank4you",
                destination_msisdn = transfer.Receiver.PhoneNumber,
                product = transfer.Product.ToString(),
            };
            var topResp = _transferToService.TopUpConfirm(topUpRequest);
            //_topUpRepostory.Push(topResp);

            if(!Ok(topResp))
            {
                //var rollbackResp = _cryptoTransferService.SendTransfer(new SendTransferInput()
                //{
                //    amount = transfer.BFYAmount,
                //    deviceId = getDeviceId(),
                //    fromAddress = getPrimaryWallet(),
                //    toAddress = transfer.Merchant.WalletAddress,                    
                //    gasLimit = transfer.GasLimit,
                //    gasPrice = transfer.GasAmount,
                //    token = CurrentToken ?? GetB4UToken()
                //});
                //if (!BasicOk(rollbackResp))
                //{
                //    throw new ApplicationException(rollbackResp.responseText);
                //}
                throw new ApplicationException(topResp.error_txt);
                //Execute rollback

            }
        }

        private bool Ok(TopUpResponse topResp)
        {
            return !string.IsNullOrEmpty(topResp.balance);
        }

        private void MerchantToWallet(TransferModel transfer)
        {

            //Merchant To Wallet
            var cryptoTransferResp = _cryptoTransferService.SendTransfer(new SendTransferInput()
            {
                amount = transfer.BFYAmount + transfer.B4UCommission,
                deviceId = getDeviceId(),
                fromAddress = transfer.Merchant.WalletAddress,
                toAddress = transfer.Receiver.WalletAddress,
                gasLimit = transfer.GasLimit,
                gasPrice = transfer.GasAmount,
                token = CurrentToken ?? GetB4UToken()
            });
            if (!BasicOk(cryptoTransferResp))
            {
                throw new ApplicationException(cryptoTransferResp.responseText);
            }
        }

        private void checkMerchants(ref TransferModel transfer)
        {
            transfer.MerchantAutoSign = Guid.NewGuid().ToString();
        }

        private void checkAmount(ref TransferModel transfer)
        {
            //throw new NotImplementedException();
        }

        private void checkCustomers(ref TransferModel transfer)
        {

            transfer.CustomersAutoSign = Guid.NewGuid().ToString();
            //throw new NotImplementedException();
        }

        private void checkAndSignTransfer(ref TransferModel transfer)
        {
            //throw new NotImplementedException();

            checkCustomers(ref transfer);
            checkAmount(ref transfer);
            checkMerchants(ref transfer);
        }
        [HttpGet]
        [DomainAuthorize]
        public ActionResult MerchantToPhoneNumber()
        {
            return View();
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult PrepareMerchantToPerson(string amount, string receiverId)
        {
            try
            {
                if (string.IsNullOrEmpty(receiverId))
                {
                    return PartialView("TransferError", "Transaction is not ready");
                }
                CustomerModel customerModel = _customerRepostory.GetInstance(receiverId);
                TransferModel transfer = new TransferModel()
                {
                    Amount = decimal.Parse(amount, CultureInfo.InvariantCulture),
                    Receiver = customerModel,
                    Merchant = CurrentUser,
                    Date = DateTime.Now,
                    Status = TransferModel.Statuses.Created,
                    TransactionCode = TransferModel.TransactionCodes.MerchantToPerson,
                    Currency = Consts.Currencies.USD
                };

                var entity = FuncWithDbContext(d => d.Transfers.Create());

                entity.Amount = transfer.Amount;
                entity.B4UHash = Guid.NewGuid().ToString();
                entity.Currency = transfer.Currency;
                entity.CustomersAutoSign = Guid.NewGuid().ToString();
                entity.Date = DateTime.Now;
                entity.DateCreated = DateTime.Now;
                entity.MerchantAutoSign = Guid.NewGuid().ToString();
                entity.ModifiedDate = DateTime.Now;
                entity.Id = Guid.NewGuid().ToString();
                entity.Status = TransferModel.Statuses.Created;
                
                PrepareTransfer(ref transfer);

                entity.B4UCommission = transfer.B4UCommission;
                entity.TransactionCode = transfer.TransactionCode;
                entity.BFYAmount = transfer.BFYAmount;
                entity.BTCAmount = transfer.BTCAmount;
                entity.GasAmount = transfer.GasAmount;
                entity.GasLimit = transfer.GasLimit;
                entity.Currency = transfer.Currency = entity.Currency ?? transfer.Currency;
                entity.Description = $"From {CurrentUser.WalletAddress} to {customerModel.WalletAddress}";
                entity.Sender = CurrentUser.WalletAddress;
                entity.Receiver = customerModel.WalletAddress;

                transfer.DestinationNumber = entity.Receiver;
                transfer.CustomerNumber = customerModel.PhoneNumber ?? entity.Receiver;

                transfer.Id = entity.Id;
                var result = _transferRepository.Push(transfer);
                //InsertTransferToDb(transfer);
                ActionWithDbContext(d => d.Transfers.Add(entity));

                return PartialView(transfer);
            }
            catch(ApplicationException ae)
            {
                return PartialView("TransferResult", L(ae.Message));
            }
            catch (Exception e)
            {
                return PartialView("TransferError", e.ToString());
            }
        }

        

        private void PrepareTransfer(ref TransferModel transfer)
        {
            initMessagesMicroLog();
            calculateAmounts(ref transfer);
            checkAndSignTransfer(ref transfer);

            getGas(ref transfer);
            getB4UCommission(ref transfer);
            formatDescription(ref transfer);
        }

        private void getB4UCommission(ref TransferModel transfer)
        {
            if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToPerson)
            {
                transfer.B4UCommission = transfer.BFYAmount * 0.005M;
            }
            else if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToMobile)
            {
                transfer.B4UCommission = transfer.BFYAmount * 0.005M;
            }
        }

        private void formatDescription(ref TransferModel transfer)
        {
            try
            {
                if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToPerson)
                {
                    transfer.Description = $"From {transfer.Sender?.WalletAddress ?? CurrentUser?.WalletAddress ?? "Unknown"} to {transfer.Receiver?.WalletAddress}";
                }
                else if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToMobile)
                {
                    transfer.Description = $"From {transfer.Sender?.WalletAddress ?? "Unknown"} to {transfer.Receiver?.PhoneNumber}";
                }
            }
            catch (Exception)
            {

            }
        }

        private void calculateAmounts(ref TransferModel transfer)
        {
            if (transfer.Amount == 0)
            {
                WriteMessageMicroLog("transfer.Amount");
                throw new ApplicationException("transfer.Amount");
            }
            var bfy_btc_ratesData = _exchangeService.CryptocurrencyRates(new CryptocurrencyRatesInput()
            {
                DeviceId = getDeviceId(),
                FromCryptocurrency = B4U.Base.Consts.Currencies.BFY,
                ToCryptocurrency = B4U.Base.Consts.Currencies.BTC,

            });
            var bfy_btc_rate = bfy_btc_ratesData.cryptocurrencies.OrderByDescending(r => r.date).FirstOrDefault().price;
            var btc_usd_ratesData = _exchangeService.CryptocurrencyRates(new CryptocurrencyRatesInput()
            {
                DeviceId = getDeviceId(),
                FromCryptocurrency = B4U.Base.Consts.Currencies.BTC,
                ToCryptocurrency = B4U.Base.Consts.Currencies.USD,

            });
            var btc_usd_rate = btc_usd_ratesData.cryptocurrencies.OrderByDescending(r => r.date).FirstOrDefault().price;
            var usd_eur_ratesData = _exchangeService.CryptocurrencyRates(new CryptocurrencyRatesInput()
            {
                DeviceId = getDeviceId(),
                FromCryptocurrency = B4U.Base.Consts.Currencies.USD,
                ToCryptocurrency = B4U.Base.Consts.Currencies.EUR,

            });
            var usd_eur_rate = usd_eur_ratesData.cryptocurrencies.OrderByDescending(r => r.date).FirstOrDefault().price;

            var usdAmount = transfer.Currency == "USD" ? transfer.Amount : transfer.Amount / ((decimal)usd_eur_rate);
            transfer.BTCAmount = usdAmount / ((decimal)btc_usd_rate);
            transfer.BFYAmount = transfer.BTCAmount / ((decimal)bfy_btc_rate);

        }

        private void getGas(ref TransferModel transfer)
        {
            TransactionDataToSignOutput getDataForSignOutput =null;
            if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToPerson)
            {
                getDataForSignOutput = _cryptoTransferService.TransactionDataToSign(new TransactionDataToSignInput()
                {
                    amount = (int)transfer.BFYAmount,
                    amountString = transfer.BFYAmount.ToString(),
                    deviceId = getDeviceId(),
                    fromAddress = CurrentUser.WalletAddress,
                    toAddress = transfer.Receiver.WalletAddress,
                    token = CurrentToken ?? GetB4UToken()
                });
            }
            else if (transfer.TransactionCode == TransferModel.TransactionCodes.MerchantToMobile)
            {
                getDataForSignOutput = _cryptoTransferService.TransactionDataToSign(new TransactionDataToSignInput()
                {
                    amount = (int)transfer.BFYAmount,
                    amountString = transfer.BFYAmount.ToString(),
                    deviceId = getDeviceId(),
                    fromAddress = CurrentUser.WalletAddress,
                    toAddress = getPrimaryWallet(),
                    token = CurrentToken ?? GetB4UToken()
                });
            }
            else if(transfer.TransactionCode == TransferModel.TransactionCodes.PersonToMobile)
            {
                getDataForSignOutput = _cryptoTransferService.TransactionDataToSign(new TransactionDataToSignInput()
                {
                    amount = (int)transfer.BFYAmount,
                    amountString = transfer.BFYAmount.ToString(),
                    deviceId = getDeviceId(),
                    fromAddress = transfer.Sender.WalletAddress,
                    toAddress = CurrentUser.WalletAddress,
                    token = CurrentToken ?? GetB4UToken(),
                    currency = B4U.Base.Consts.Currencies.BFY
                });
            }
            else
            {
                throw new ApplicationException($"TransactionCode {transfer.TransactionCode} is not supported");
            }

            if (getDataForSignOutput != null && getDataForSignOutput.responseCode == 0)
            {
                transfer.GasAmount = getDataForSignOutput.gasPrice;
                transfer.GasLimit = getDataForSignOutput.gasLimit;
            }
            else
            {
                throw new ApplicationException(getDataForSignOutput.responseText);
            }
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult PrepareMerchantToPhoneNumber(ReserveTransferInput input)
        {
            try
            {
                CustomerModel customerModel ;

                //var resp = _transferToService.TopUpPrepare();
                if (string.IsNullOrEmpty(input.ReceiverId))
                {
                    customerModel = new CustomerModel()
                    {
                        PhoneNumber = input.DestinationNumber
                    };
                    //return PartialView("TransferError", "Transaction is not ready");
                }
                else
                {
                    customerModel = _customerRepostory.GetInstance(input.ReceiverId);
                }
                {
                    TransferModel transfer = new TransferModel()
                    {
                        Amount = input.RetailAmount,
                        Product = input.Product,
                        Receiver = customerModel,
                        Merchant = CurrentUser,
                        Date = DateTime.Now,
                        TransactionCode = TransferModel.TransactionCodes.MerchantToMobile
                    };

                    PrepareTransfer(ref transfer);

                    var result = _transferRepository.Push(transfer);
                    transfer.Id = result;
                    _transferRepository.Update(transfer, transfer.Id);
                    InsertTransferToDb(transfer);

                    //var sender = _customerRepostory.GetInstance(input.SenderId);

                    //if (!BasicOk(cryptoTransferResp))
                    //{
                    //    throw new ApplicationException(cryptoTransferResp.responseText);
                    //}
                    return PartialView("PrepareMerchantToPhoneNumber", transfer);
                }
            }
            catch (Exception e)
            {
                return PartialView("TransferError", e.ToString());
            }
            return PartialView();
        }

        private static void InsertTransferToDb(TransferModel transfer)
        {
            try
            {
                var entity = AutoMapper.Mapper.Map<B4U.Base.Entities.Transfer.Transfer>(transfer);
                using (var baseDataContext = new BaseDataContext())
                {
                    baseDataContext.Transfers.Add(entity);
                    baseDataContext.SaveChanges();
                }
            }
            catch { }
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult Prepare(ReserveTransferInput input)
        {
            TransferModel transferModel = null;
            var entity = FuncWithDbContext(d => d.Transfers.Create());
            
            entity.Amount = input.RetailAmount;
            entity.B4UHash = Guid.NewGuid().ToString();
            entity.Currency = input.RetailCurrency;
            entity.CustomersAutoSign = Guid.NewGuid().ToString();
            entity.Date = DateTime.Now;
            entity.DateCreated= DateTime.Now;
            entity.MerchantAutoSign = Guid.NewGuid().ToString();
            entity.ModifiedDate = DateTime.Now;
            entity.Id = Guid.NewGuid().ToString();
            entity.Status = TransferModel.Statuses.Created;
            entity.DestinationNumber = input.DestinationNumber;
            entity.Product = input.Product;
            
            try
            {
                var sender = _customerRepostory.GetInstance(input.SenderId);
                entity.CustomerNumber = sender?.PhoneNumber ?? sender?.Name ?? "Unknown";

                CustomerModel customerModel = null;

                if (!string.IsNullOrEmpty(input.DestinationNumber))
                {
                    customerModel = _customerRepostory.Search(input.DestinationNumber).FirstOrDefault();
                    entity.Receiver = input.DestinationNumber;                   
                    entity.Sender = sender.WalletAddress;
                }

                transferModel = new TransferModel()
                {
                    Amount = input.RetailAmount,
                    Product = input.Product,
                    Receiver = customerModel,
                    Sender = sender,
                    Merchant = CurrentUser,
                    Date = entity.Date,
                    TransactionCode = TransferModel.TransactionCodes.MerchantToMobile,
                    Id = entity.Id
                };

                PrepareTransfer(ref transferModel);

                entity.TransactionCode = transferModel.TransactionCode;
                entity.BFYAmount = transferModel.BFYAmount;
                entity.BTCAmount = transferModel.BTCAmount;
                entity.B4UCommission = transferModel.B4UCommission;
                entity.GasAmount = transferModel.GasAmount;
                entity.GasLimit = transferModel.GasLimit;
                entity.Currency = transferModel.Currency = entity.Currency ?? transferModel.Currency;
                entity.Description = $"From {entity.Sender ?? sender?.PhoneNumber ?? "Unknown"} to {entity.Receiver ?? "Unknown"}";
                
                //return ConfirmMerchantToPhoneNumber(input, transferModel, entity);
                return PartialView("PrepareMerchantToPhoneNumber", transferModel);
            }
            catch (ApplicationException e)
            {
                entity.Status = TransferModel.Statuses.Failed;
                entity.Description = e.Message;
                return PartialView("TransferResult", L(e.Message));
            }
            catch (Exception e)
            {
                entity.Status = TransferModel.Statuses.Failed;
                entity.Description = e.Message;
                return PartialView("TransferError", e.ToString());
            }
            finally
            {
                entity.Sender = CurrentUser.WalletAddress;
                entity.Receiver = getPrimaryWallet();

                transferModel.DestinationNumber = entity.DestinationNumber ?? entity.Receiver;
                transferModel.CustomerNumber = entity.CustomerNumber ?? entity.Sender;

                entity.ModifiedDate = DateTime.Now;

                ActionWithDbContext(d => d.Transfers.Add(entity));
                //Mapper.Map(entity, transferModel);
                transferModel.Status = entity.Status;
                _transferRepository.Push(transferModel);
            }
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult ConfirmMerchantToPhoneNumber(string transactionId)
        {
            var transferModel = _transferRepository.GetInstance(transactionId);
            try
            {
                var m = FuncWithDbContext(d =>
                {
                    var entity = d.Transfers.FirstOrDefault(t => t.Id == transactionId);
                    ReserveTransferOutput model = ExecuteMecrantToPhoneNumber(transferModel, entity);
                    transferModel.Status = entity.Status;
                    d.SaveChanges();
                    return model;
                });

                return PartialView("Prepare", m);
            }
            catch (Exception e)
            {
                return PartialView("TransferError", (e.InnerException ?? e).Message);
            }
            finally
            {
                _transferRepository.Update(transferModel, transferModel.Id);
            }
        }

        private ReserveTransferOutput ExecuteMecrantToPhoneNumber(TransferModel transferModel, B4U.Base.Entities.Transfer.Transfer entity)
        {
            var resp = _transferToService.TopUpPrepare();

            {
                //var receiver = _customerRepostory.GetInstance(input.ReceiverId);
                var cryptoTransferResp = _cryptoTransferService.SendTransfer(new SendTransferInput()
                {
                    amount = transferModel.BFYAmount + transferModel.B4UCommission,
                    deviceId = getDeviceId(),
                    fromAddress = entity.Sender,
                    toAddress = entity.Receiver,
                    gasLimit = transferModel.GasLimit,
                    gasPrice = transferModel.GasAmount,
                    token = CurrentToken ?? GetB4UToken()
                });

                if (!BasicOk(cryptoTransferResp))
                {
                    throw new ApplicationException(cryptoTransferResp.responseText);
                }
                entity.B4UHash = cryptoTransferResp.transactionHash;
            }

            ReserveTransferOutput model = ExecuteReservedTopUpNew(resp, entity);
            return model;
        }

        protected virtual ReserveTransferOutput ExecuteReservedTopUpNew(ReserveIdResponse resp, B4U.Base.Entities.Transfer.Transfer transfer)
        {
            try
            {
                var topUpRequest = new TopUpRequest()
                {
                    ReservedId = resp.reserved_id,
                    msisdn = getB4USenderName(),
                    destination_msisdn = transfer.DestinationNumber,
                    product = transfer.Product.ToString(),
                };
                var topResp = _transferToService.TopUpConfirm(topUpRequest);

                //transfer.Id = _topUpRepostory.Push(topResp);
                transfer.Status = TransferModel.Statuses.Executed;
                transfer.ModifiedDate = DateTime.Now;

                ReserveTransferOutput model = new ReserveTransferOutput()
                {
                    data = topResp,
                    BFYAmount = transfer.BFYAmount + transfer.B4UCommission,
                    BTCAmount = transfer.BTCAmount,
                    GasAmount = transfer.GasAmount
                };
                return model;
            }
            catch (Exception e)
            {
                transfer.Status = TransferModel.Statuses.Failed;
                transfer.Description = e.Message;

                throw;
            }
            finally
            {
                //if (transfer == null) ActionWithDbContext(d => d.Transfers.Add(transfer));
            }
        }

        private string getB4USenderName()
        {
            return "bank4you";
        }

        protected virtual ReserveTransferOutput ExecuteReservedTopUp(ReserveTransferInput input, ReserveIdResponse resp, B4U.Base.Entities.Transfer.Transfer transfer = null)
        {
            var entity = transfer ?? FuncWithDbContext(d => d.Transfers.Create());
            if (transfer == null)
            {
                entity.Amount = input.RetailAmount == 0 ? input.Product : input.RetailAmount;
                entity.Status = TransferModel.Statuses.Created;
                entity.DateCreated = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.Currency = "EUR";
            }
            try
            {
                var topUpRequest = new TopUpRequest()
                {
                    ReservedId = resp.reserved_id,
                    msisdn = input.SenderNumber,
                    destination_msisdn = input.DestinationNumber,
                    product = input.Product.ToString(),
                };
                var topResp = _transferToService.TopUpConfirm(topUpRequest);

                //entity.Id = _topUpRepostory.Push(topResp);
                entity.Status = TransferModel.Statuses.Executed;
                entity.ModifiedDate = DateTime.Now;

                ReserveTransferOutput model = new ReserveTransferOutput()
                {
                    data = topResp,
                    BFYAmount = entity.BFYAmount,
                    BTCAmount = entity.BTCAmount,
                    GasAmount = entity.GasAmount
                };
                return model;
            }
            catch (Exception e)
            {
                entity.Status = TransferModel.Statuses.Failed;
                entity.Description = e.Message;

                throw;
            }
            finally
            {
                if(transfer == null) ActionWithDbContext(d => d.Transfers.Add(entity));
            }
        }

        protected string getPrimaryWallet()
        {
            var id = ConfigurationManager.AppSettings["primaryId"] ?? "6366915749475421370-20831348370-559164597";
            //var primary = _userRepostory.GetInstance(id);

            var primary = FuncWithDbContext(d => d.Users.Where(u => u.Id == id || u.IsPrimary == true).OrderByDescending(u => u.DateCreated).FirstOrDefault());

            if (primary == null)
                throw new ApplicationException("Primary not set");

            return primary.WalletAddress;
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult ReserveTransfer(ReserveTransferInput input)
        {

            var guid = Guid.NewGuid();
            //CustomerModel sender = _customerRepostory.GetInstance(input.SenderId);
            //CustomerModel receiver = _customerRepostory.GetInstance(input.ReceiverId);
            //var transfer = new TransferModel()
            //{
            //    Date = DateTime.Now,
            //    Id = guid.ToString(),
            //    ApiId = System.Convert.ToBase64String(guid.ToByteArray()),
            //    Amount = input.Product,
            //    //Currency = input.Currency,
            //    //Description = input.Description,
            //    Sender = sender,
            //    Receiver = receiver,
            //    Status = TransferModel.Statuses.Created,
            //    TransactionCode = "MT01"
            //};
            //_transferRepository.Push(transfer);
            return Json(new { result = "Ok" });
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult CommitTransfer(CommitTransferInput input)
        {
            return Json(new { result = "Ok" });
        }

        [HttpGet]
        [DomainAuthorize]
        public ActionResult UserToUser()
        {
            return View();
        }
        [HttpGet]
        [DomainAuthorize]
        public ActionResult MerchantToWallet()
        {
            return View();
        }

        [HttpPost]
        [DomainAuthorize]
        public ActionResult SearchUserForTransfer(string search)
        {
            //_customerRepostory.CustomAction(ec => ec.Search())
            CustomerModel[] model = new CustomerModel[] { };
            if (string.IsNullOrEmpty(search))
            {
                model = _customerRepostory.GetList().ToArray();
            }
            else
            {
                search = search.ToLower();
                Queries.QueryBuilder<CustomerModel> _q = new Queries.QueryBuilder<CustomerModel>()
                        .Size(100)
                        .From(0);

                _q.Query(q => q.Bool(
                        b => b.Should(
                            should => should.
                                MultiMatch(mm => mm.Query(search)).
                                Wildcard(wc => wc.Field(f => f.Name).Value($"*{search}*")).
                                Wildcard(wc => wc.Field(f => f.PhoneNumber).Value($"*{search}*")).
                                Wildcard(wc => wc.Field(f => f.WalletAddress).Value($"*{search}*")).
                                Wildcard(wc => wc.Field(f => f.Email).Value($"*{search}*")).
                                Wildcard(wc => wc.Field(f => f.Address).Value($"*{search}*"))
                    )));
                
                //MultiMatch(t => t.Query(search)));

                var r = _customerRepostory.CustomAction(c => c.Search(new SearchCommand(nameof(CustomerModel), "_doc"), _q));
                model = r.Documents.ToArray();// _transferRepository.Search(input.SearchQuery);
            }

            return base.PartialView("SelectItem", model);
        }
    }
}