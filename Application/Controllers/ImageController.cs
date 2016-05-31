using Application.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Image
        public FileResult Index(int id)
        {
            News news = db.News.Find(id);
            if (news.SavedFileName == null)
            {
                var path = Path.Combine(Server.MapPath("~/App_Data/Images/"), "no-image.jpg");
                return File(path, "image/jpeg", "no-image.jpg");
            }
            else
            {
                var path = Path.Combine(Server.MapPath("~/App_Data/Images/"), news.SavedFileName);
                return File(path, "image/jpeg", news.CustomFileName);
            }
        }
    }
}