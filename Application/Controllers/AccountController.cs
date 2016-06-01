using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Application.Models;
using System.Collections.Generic;

namespace Application.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public ActionResult List()
        {
            var Users = UserManager.Users.ToList();
            List<AccountInfoViewModel> Accounts = new List<AccountInfoViewModel>();

            foreach (var user in Users)
            {
                AccountInfoViewModel account = new AccountInfoViewModel();
                account.UserName = user.UserName;


                account.Roles = UserManager.GetRoles(user.Id).ToList();
                Accounts.Add(account);

            }
            return View(Accounts);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel model)
        {


            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "user");
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    Session["User"] = user.UserName;

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "News");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            Session["User"] = "Anonymous";

            return RedirectToAction("Login");


        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditRoles(string userName)
        {
            var user = UserManager.FindByName(userName);
            EditAccountRolesViewModel editRolesModel = new EditAccountRolesViewModel();

            editRolesModel.UserName = userName;

            var userRoles = UserManager.GetRoles(user.Id).ToList();

            editRolesModel.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });

            return View(editRolesModel);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> EditRoles(EditAccountRolesViewModel editRolesModel, params string[] selectedRoles)
        {
            var user = UserManager.FindByName(editRolesModel.UserName);

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            if (selectedRoles == null)
                selectedRoles = new string[] { };

            var result = await UserManager.AddToRolesAsync(user.Id,
                selectedRoles.Except(userRoles).ToArray<string>());


            result = await UserManager.RemoveFromRolesAsync(user.Id,
                userRoles.Except(selectedRoles).ToArray<string>());



            return RedirectToAction("List");
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}