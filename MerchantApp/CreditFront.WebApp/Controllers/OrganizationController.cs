using CreditFront.Web.Controllers;
using CreditFront.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreditFront.WebApp.Controllers
{
    public class OrganizationController : BaseController
    {
        public OrganizationController()
        {
        }

        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Branches()
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult Branch(long id)
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult Branch(BranchInputDto inputDto)
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult OrganizationUsers()
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult OrganizationUser(long id)
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult OrganizationUser(UserInputDto inputDto)
        {
            return Json(null);
        }
    }
}