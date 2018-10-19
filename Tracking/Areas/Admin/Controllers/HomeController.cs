using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tracking.Data;

namespace Tracking.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private TrackingEntities db = new TrackingEntities();

        // GET: Admin/Home
        public ActionResult Index()
        {
            try
            {
                return View(db.Details.ToList());
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;

                return View(db.Details.ToList());
            }


        }

        // GET: Admin/Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Detail detail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var name = db.Details.Where(x => x.Name == detail.Name).FirstOrDefault();

                    if (name != null)
                    {
                        throw new Exception("Name Already Exist");
                    }

                    var digit = db.Details.Where(x => x.Digit == detail.Digit).FirstOrDefault();

                    if (digit != null)
                    {
                        throw new Exception("Code Already Exist");
                    }


                    detail.CreatedDate = DateTime.Now;
                    db.Details.Add(detail);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }

                return View(detail);
            }
            catch (Exception ex)
            {
                TempData["CreateError"] = ex.Message;
            }
            return View(detail);
        }

        // GET: Admin/Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // POST: Admin/Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Detail detail)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    detail.CreatedDate = DateTime.Now;

                    db.Entry(detail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }

                return View(detail);
            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message;
            }
            return View(detail);

        }



        // POST: Admin/Home/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Detail detail = db.Details.Find(id);
            db.Details.Remove(detail);
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
