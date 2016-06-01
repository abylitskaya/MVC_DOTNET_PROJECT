using Application.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    class ApplicationRoleManager : RoleManager<ApplicationRoles>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRoles> store)
                    : base(store)
        { }
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
                                                Microsoft.Owin.IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<ApplicationRoles>(context.Get<ApplicationDbContext>()));
        }
    }
}