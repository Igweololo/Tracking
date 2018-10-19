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
    public class TrackController : Controller
    {
        private TrackingEntities db = new TrackingEntities();

        // GET: Admin/Track
        public ActionResult Index(int Id)
        {
           var detail = db.Details.Find(Id);
           ViewBag.Name =   detail.Name;
            Session["Id"] = Id;

            var model = db.Tracks.Where(x => x.DetailId == Id).ToList();
            return View(model);
        }



        // GET: Admin/Track/Create
        public ActionResult Create()
        {
            ViewBag.Code = new SelectList(db.Details.ToList(), "DetailId", "Name");
            return View();
        }

        // POST: Admin/Track/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Track track)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   

                    db.Tracks.Add(track);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { Area = "Admin", Id = track.DetailId });
                }
                ViewBag.Code = new SelectList(db.Details.ToList(), "DetailId", "Name");

                return View(track);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
            }
            return View(track);
        }

        // GET: Admin/Track/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            ViewBag.Code = new SelectList(db.Details.ToList(), "DetailId", "Name", track.DetailId);
            return View(track);
        }

        // POST: Admin/Track/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Track track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Area = "Admin", Id = track.DetailId });
            }

            ViewBag.Code = new SelectList(db.Details.ToList(), "DetailId", "Name", track.DetailId);
            return View(track);
        }

        

        // POST: Admin/Track/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Track track = db.Tracks.Find(id);
            db.Tracks.Remove(track);
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = Session["Id"]});
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
