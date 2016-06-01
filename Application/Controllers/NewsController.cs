using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Application.Models;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.IO;

namespace Application.Controllers
{
    public class NewsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager userManager = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();

        // GET: News
        public ActionResult Index()
        {

            //return View(db.News.Include(n => n.ApplicationUser).Include(n => n.Tags).OrderByDescending(n => n.CreationDate).ToList());
            return View(db.News.ToList());

        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        //[System.Web.Http.Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: News/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        [System.Web.Http.Authorize]
        public ActionResult Create([Bind(Include = "Id,Title,Content")] News news, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                news.CreationDate = DateTime.Now;
                //news.ApplicationUser = user;

                if (upload != null)
                {
                    news.SavedFileName = Guid.NewGuid().ToString();
                    news.CustomFileName = upload.FileName;

                    var path = Path.Combine(Server.MapPath("~/App_Data/Images/"), news.SavedFileName);
                    upload.SaveAs(path);
                }

                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: News/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    News news = db.News.Find(id);
        //    if (news == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(news);
        //}

        // POST: News/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            //var selectedNews = db.News.Include(n => n.ApplicationUser).Include(n => n.Tags).Where(n => n.Id == id).Single();

            //ApplicationUser currentUser = userManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            //if (selectedNews.ApplicationUser.Id != currentUser.Id && !userManager.IsInRole(currentUser.Id, "admin"))
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //db.News.Remove(selectedNews);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
