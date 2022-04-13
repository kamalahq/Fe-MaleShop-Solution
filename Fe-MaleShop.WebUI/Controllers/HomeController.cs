using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Fe_MaleShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly Fe_MaleShopDbContext db;
        readonly IConfiguration configuration;
        public HomeController(Fe_MaleShopDbContext db,IConfiguration configuration)
        {
            this.db = db;
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Faq()
        {
            var faqs = db.Faqs.Where(f =>f.DeletedByUserId == null).ToList();
            return View(faqs);
        }


        public IActionResult AllProducts()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Sizin sorğunuz qəbul edilmişdir.Tezliklə geri dönüş edəcəyik.";
                return View();
            }
            

            return View(contact);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            if (ModelState.IsValid)
            {
                var current = db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));
                if (current!=null && current.EmailConfirmed == true)
                {
                    return Json(new
                    {
                        error = false,
                        message = "Bu e-poctladaha once qeydiyyatdan kecmisniz."
                    });
                }
                else if(current != null && (current.EmailConfirmed ?? false == false))
                {
                    return Json(new
                    {
                        error = false,
                        message = "E-pocta gonderilmis linkle tamamlanmayib."
                    });
                }

                db.Subscribes.Add(model);
                db.SaveChanges();

                string token = $"subscribetoken-{model.Id}-{DateTime.Now:yyyyMMddHHmmss}";

                string path = $"{ Request.Scheme}://{Request.Host}?token={token}";

                string fromMail = configuration["emailAccount:userName"];

                string displayName = configuration["emailAccount:displayName"];
                string smtpServer = configuration["emailAccount:smtpServer"];
                int smtpPort = Convert.ToInt32(configuration["emailAccount:smtpPort"]);
                string password = configuration["emailAccount:password"];
                string cc = configuration["emailAccount:cc"];

                MailAddress from = new MailAddress(fromMail, displayName);
                MailAddress to = new MailAddress(model.Email);
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Fe_MaleShops Newsletter subscribe ";
                message.Body = $"Zəhmət olmasa <a href={path}>link</a> vasitəsilə abunəliyi tamamlayasınız";
                message.IsBodyHtml = true;

                if(!string.IsNullOrWhiteSpace(cc))
                message.CC.Add(cc);

                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                smtpClient.Credentials = new NetworkCredential(fromMail,password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);

                //todo
                return Json(new
                {
                    error = false,
                    message = "Sorğunuz uğurla qeydə alindi.Zəhmət olmasa E-poçtunuza göndərilmiş linkdən abunəliyi tamamlayasınız"
                });
            }
            return Json(new
            {
                error = true,
                message="Sorğunun icrası zamanı xəta yarandı.Biraz sonra yeniden yoxlayın"
            });
        }
    }
}
