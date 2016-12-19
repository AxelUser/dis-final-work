using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.Portal.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login()
        {
            
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            return View();
        }
    }
}