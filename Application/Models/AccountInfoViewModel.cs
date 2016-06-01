using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class AccountInfoViewModel
    {
        public string UserName { get; set; }
        public List<String> Roles { get; set; }
    }
}