using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ApplicationContext() : base("name=ApplicationContext")
        {
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }


        //public System.Data.Entity.DbSet<Application.Models.News> News { get; set; }

        //public System.Data.Entity.DbSet<Application.Models.Tag> Tags { get; set; }
    }
}