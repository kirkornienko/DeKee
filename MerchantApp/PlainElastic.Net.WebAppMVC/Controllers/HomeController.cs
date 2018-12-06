using PlainElastic.Net.Base.Entities;
using PlainSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
                
        public ActionResult About()
        {
            ViewBag.Message = "CryptoBox";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "support@b4u.com";

            return View();
        }
    }
}