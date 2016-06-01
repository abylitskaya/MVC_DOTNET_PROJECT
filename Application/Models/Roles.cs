using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class ApplicationRoles : IdentityRole
    {
        public ApplicationRoles()
        {

        }

      
        public string Description { get; set; }
    }
}