using B4U.Dao.Base;
using B4U.ElasticContext.Base;
using PlainElastic.Net.WebAppMVC.Dto;
using PlainElastic.Net.WebAppMVC.Models;
using PlainElastic.Net.WebAppMVC.Models.CryptoBox;
using PlainElastic.Net.WebAppMVC.Utils.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Controllers
{
    public class CustomerController: BaseController
    {
        protected readonly IRepostory<CustomerModel> _customerRepostory;
        public CustomerController()
        {
            _customerRepostory = HandyContext.ElasticRepositoryHost.GetRepostory<CustomerModel>();
        }

        [HttpGet]
        [DomainAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult List(string searchQuery)
        {
            //var items = string.IsNullOrEmpty(searchQuery)
            //    ? _customerRepostory.GetList()
            //    : _customerRepostory.Search(searchQuery);

            CustomerModel[] model = new CustomerModel[] { };
            if (string.IsNullOrEmpty(searchQuery))
            {
                model = _customerRepostory.GetList().ToArray();
            }
            else
            {
                searchQuery = searchQuery.ToLower();
                Queries.QueryBuilder<CustomerModel> _q = new Queries.QueryBuilder<CustomerModel>()
                        .Size(100)
                        .From(0);

                string wildCardQuery = $"*{searchQuery}*";
                _q.Query(q => q.Bool(
                        b => b.Should(
                            should => should.
                                MultiMatch(mm => mm.Query(searchQuery)).
                                Wildcard(wc => wc.Field(f => f.Name).Value(wildCardQuery)).
                                Wildcard(wc => wc.Field(f => f.PhoneNumber).Value(wildCardQuery)).
                                Wildcard(wc => wc.Field(f => f.WalletAddress).Value(wildCardQuery)).
                                Wildcard(wc => wc.Field(f => f.Email).Value(wildCardQuery)).
                                Wildcard(wc => wc.Field(f => f.Address).Value(wildCardQuery))
                    )));

                //MultiMatch(t => t.Query(search)));

                var r = _customerRepostory.CustomAction(c => c.Search(new SearchCommand(nameof(CustomerModel), "_doc"), _q));
                model = r.Documents.ToArray();// _transferRepository.Search(input.SearchQuery);
            }

            return PartialView("List", model);
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult GetUser(string Id)
        {
            var data = _customerRepostory.GetInstance(Id);
            data.Id = Id;
            return PartialView("SearchItems", new object[] { data });
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult SearcCustomers(CustomerSearchInput input)
        {
            //_customerRepostory.CustomAction(ec => ec.Search())
            var data = _customerRepostory.GetList();
            
            return PartialView("SearchItems", data);
        }


        [HttpGet]
        [DomainAuthorize]
        public ActionResult CreateCustomer()
        {
            //TODO validation, search such customer first. Some flow is required
            return View();
        }
        [HttpPost]
        [DomainAuthorize]
        public ActionResult CreateCustomer(CustomerModel customer)
        {
            //TODO validation, search such customer first. Some flow is required

            var exsitingClient = _customerRepostory.Search(customer.PhoneNumber);
            //if(exsitingClient.Any(c => c.PhoneNumber == customer.PhoneNumber && c.WalletAddress == cus))
            //{
            //    return View("Result", "This phone number is allready used as customer's phone number");
            //}

            customer.Id = Guid.NewGuid().ToString();
            searchWallet(customer);
            var id = _customerRepostory.Push(customer);
            customer.Id = id;
            _customerRepostory.Update(customer, id);
            return RedirectToAction("PhoneInfo", customer);
        }

        private void searchWallet(CustomerModel customer)
        {
            try
            {
                B4UDataContext b4U = new B4UDataContext();
                var wallet = b4U.Execute(
                    B4UQueryLib.Select.ClientMainWallet(out string paramName),
                    p => p.AddWithValue(paramName, customer.PhoneNumber), 
                    r => r.Read() ? r[0].ToString() : null
                );
                customer.WalletAddress = wallet;
            }
            catch(Exception)
            { }
            //throw new NotImplementedException();
        }

        [DomainAuthorize]
        public ActionResult PhoneInfo(CustomerModel customer)
        {
            return View(customer);
        }
        [DomainAuthorize]
        public ActionResult CheckCryptoInfo(string customerId)
        {
            var customer = _customerRepostory.GetInstance(customerId);
            customer.Id = customerId;
            return View(customer);
        }
        [DomainAuthorize]
        [HttpPost]
        public ActionResult SaveCryptoInfo(string wallet, string customerId)
        {
            var customer = _customerRepostory.GetInstance(customerId);
            customer.Id = customerId;
            customer.WalletAddress = wallet;
            _customerRepostory.Update(customer, customerId);
            return PartialView("CheckCryptoService");
        }
        public ActionResult CheckCryptoService(string wallet, string customerId)
        {
            return PartialView();
        }
       

        //[HttpPost]
        //public ActionResult VerifyPhoneNumber(string phone)
        //{

        //}
    }
}