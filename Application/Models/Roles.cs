using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class Roles : IdentityRole
    {
        public Roles()
        {

        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}