using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Tracking.Models;

namespace Tracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var body = "<p>Email From: {0} ({1})</p><p> Message: </p><p>{2}</p>";
                    var message = new MailMessage();

                    message.To.Add(new MailAddress("parcelcourier75@gmail.com")); // replace with valid value 
                    message.From = new MailAddress(model.Email); // replace with valid value,you cannot commend it, since it's required
                    message.Subject = model.Subject;
                    message.Body = string.Format(body, model.Name, model.Email, model.Message);
                    message.IsBodyHtml = true;

                    using (var smtp = new SmtpClient())
                    {
                        var credential = new NetworkCredential
                        {
                            UserName = "godwinanderson509@gmail.com", // replace with valid value
                            Password = "andrew75" // Password = "password"  // replace with valid value
                        };
                        //smtp.Credentials = credential;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential("godwinanderson509@gmail.com", "andrew75"); //You will be receive email from this email address
                                                                                                                   //smtp.Host = "smtp-mail.outlook.com";
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(message);

                        TempData["Notification"] = "Email sent successfully";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                TempData["ContactError"] = "Error Occurred";
                return View();
            }

        }
    }
}