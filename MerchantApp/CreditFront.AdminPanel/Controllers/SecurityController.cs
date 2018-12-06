using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditFront.AdminPanel.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult LogInVue()
        {
            return View();
        }
    }
}