using Application.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles

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


        public ActionResult Index()
        {

            var roles = RoleManager.Roles.ToList();
            List<AccountInfoViewModel> Accounts = new List<AccountInfoViewModel>();
            return View(roles);


        }
    }
}