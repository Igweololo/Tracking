using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Tracking.Configuration;
using Tracking.Data;

namespace Tracking.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private TrackingEntities db = new TrackingEntities();

        public ActionResult Dodo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dodo(User model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();

                    if (user == null)
                    {
                        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                        var passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

                        var person = new User
                        {
                            Email = model.Email,
                            Password = passwordHash
                        };

                        db.Users.Add(person);
                        db.SaveChanges();

                        TempData["LogNotification"] = "Registration Successful";
                        return RedirectToAction("Log");
                    }
                    else
                    {
                        TempData["DodoError"] = "User already exist";
                        return View();

                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["DodoError"] = ex.Message;
                return View();
            }

        }

        // GET: Account
        public ActionResult Log()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Log(User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var user = db.Users.Where(x => x.Email == model.Email).FirstOrDefault();



                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    {


                        var auth = HttpContext.GetOwinContext().Authentication;
                        var claims = new List<Claim>
                        {
                            new Claim (ClaimTypes.Email,user.Email),
                            new Claim (ClaimTypes.Name,user.Email),
                            new Claim (ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        };



                        var identity = new ClaimsIdentity(claims, Config.Type);

                        auth.SignIn(identity);
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);

                        }

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            TempData["LogError"] = "Invalid email and password";
            return View(model);
        }

        public ActionResult Logout()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public ActionResult Delete()
        {
            var user = db.Users.ToList();

            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Delete");
        }
    }
}