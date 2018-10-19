using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracking.Data;
using Tracking.Models;

namespace Tracking.Controllers
{
    public class ServiceController : Controller
    {
        private TrackingEntities db = new TrackingEntities();
        // GET: Service
        public ActionResult Transport()
        {
            return View();
        }

        public ActionResult Cargo()
        {
            return View();
        }

        public ActionResult Warehouse()
        {
            return View();
        }

        public ActionResult Trucking()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }

        public ActionResult Logistic()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Track()
        {
            var model = new TrackViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Track(TrackViewModel model)
        {
            if (model.Details.Digit != null)
            {
                var code = model.Details.Digit.Trim();

                var detail = db.Details.Where(x => x.Digit == code).FirstOrDefault();
                if (detail != null)
                {
                    var tracks = db.Tracks.Where(x => x.DetailId == detail.DetailId).ToList();
                    
                    if (tracks.Count >= 1)
                    {
                        model.Tracks = tracks;
                        model.Details = detail;
                        return View(model);
                    }
                    TempData["Error"] = "No details yet";
                }
                else
                {
                    TempData["Error"] = "The code does not exist, please check and try again";
                }
                return View(model);
            }
            else
            {
                TempData["Error"] = "Code is required";
                return View(model);
            }

        }

    }
}