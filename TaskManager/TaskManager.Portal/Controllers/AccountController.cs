using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TaskManager.Portal.BLL.DTO;
using TaskManager.Portal.BLL.Servicies;

namespace TaskManager.Portal.Controllers
{
    public class AccountController : Controller
    {
        private AuthService authService;
        public AccountController()
        {
            authService = new AuthService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await authService.IsUserCredentialsCorrectAsync(model))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "There is no such user!");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(!await authService.IsRegisteredUserExists(model))
                {
                    await authService.RegisterUser(model);

                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User exists!");
                }
            }
            return View();
        }

        public ActionResult LogOff()
        {
            return View();
        }
    }
}