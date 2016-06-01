using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<News> News { get; set; }
    }
}