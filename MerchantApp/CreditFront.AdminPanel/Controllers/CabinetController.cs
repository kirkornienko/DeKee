using CreditFront.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditFront.AdminPanel.Controllers
{
    public class CabinetController : BaseController
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult SchemeDetails(string id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult Schemes()
        {
            return View();
        }

        public ActionResult Data()
        {
            return View();
        }
    }
}