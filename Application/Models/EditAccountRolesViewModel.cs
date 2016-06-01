using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Models
{
    public class EditAccountRolesViewModel
    {
        public string UserName { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}