using System.Web.Mvc;
using StoreFrontLab1.UI.MVC.Models;
using System.Net.Mail;
using System.Net;
using System.Web;
using System;

namespace StoreFrontLab1.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have revieved an email from {cvm.Name} with a subject of {(cvm.Subject == null ? "Email from Codeweeks" : cvm.Subject)}. Please respond to {cvm.EmailAddress} with your response to the following message:<br/>{cvm.Message}";
            MailMessage mm = new MailMessage("admin@codeweeks.com", "wcoltonweeks@outlook.com", cvm.Subject, cvm.Message);
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(cvm.EmailAddress);
            SmtpClient client = new SmtpClient("mail.codeweeks.com");
            client.Credentials = new NetworkCredential("admin@codeweeks.com", "WZxaw4dh_");

            try
            {
                client.Send(mm);
            }
            catch(Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your message could not be sent at this time. Please try again later<br/>{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);

        }
    }
}
