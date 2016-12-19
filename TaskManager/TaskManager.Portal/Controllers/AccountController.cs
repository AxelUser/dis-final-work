using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Portal.BLL.DTO;

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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
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