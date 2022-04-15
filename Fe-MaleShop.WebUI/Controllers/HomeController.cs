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
using Fe_MaleShop.WebUI.AppCode.Extensions;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Fe_MaleShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        readonly Fe_MaleShopDbContext db;
        readonly IConfiguration configuration;
        //public object Crypto { get; private set; }
        public HomeController(Fe_MaleShopDbContext db, IConfiguration configuration)
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
            var faqs = db.Faqs.Where(f => f.DeletedByUserId == null).ToList();
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
        [HttpPost/*("/subscribe-register")*/]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var current = db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));
                    if (current != null && current.EmailConfirmed == true)
                    {
                        return Json(new
                        {
                            error = false,
                            message = "Bu e-poctla daha once qeydiyyatdan kecmisniz."
                        });
                    }
                    else if (current != null && (current.EmailConfirmed ?? false == false))
                    {
                        return Json(new
                        {
                            error = false,
                            message = "E-pocta gonderilmis linkle tamamlanmayib."
                        });
                    }

                    var t = db.Database.BeginTransaction();
                    //var subscriber = db Subscribers.FirsOrDefaultAsync(s=>s.DeletedByUserId == null)

                    db.Subscribes.Add(model);

                    db.SaveChanges();

                    string token = $"{model.Email}-{DateTime.Now.AddMinutes(20):yyyyMMddHHmm}-{model.Id}";
                    string securityKey = configuration["securityKey"];
                    token = token.Encrypt(securityKey);

                    string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}";

                    var mailSended = configuration.SendEmail(model.Email, "Fe_MaleShops Newsletter subscribe ", $"Zəhmət olmasa <a href={path}>link</a> vasitəsilə abunəliyi tamamlayasınız");

                    if (mailSended == false)
                    {

                        db.Database.RollbackTransaction();

                        return Json(new
                        {
                            error = true,
                            message = "E-mail göndərilən zaman xəta baş verdi.Biraz sonra yeniden yoxlayın"
                        });
                    }

                    db.Database.CommitTransaction();

                    return Json(new
                    {
                        error = false,
                        message = "Sorğunuz uğurla qeyde alındı.Zəhmət olmasa E-poçtunuza göndərilmiş linkdən abunəliyinizi tamamlayın."
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }

            }


            return Json(new
            {
                error = true,
                message = "Sorğunun icrası zamanı xəta yarandı.Biraz sonra yeniden yoxlayın"
            });

        }
        //[HttpPost]
        //[Route("subscribe-confirm")]
        //public IActionResult SubscribeConfirm(string token)
        //{
        //    Match match = Regex.Match(token, @"subscribetoken-(?<id>\d+)-(?<executeTimeStamp>\d{14})");

        [HttpGet("subscribe-confirm")]
        public async Task<IActionResult> SubscriberConfirm(string token)
        {
            ViewBag.Message = string.Empty;
            ViewBag.Status = null;

            string securityKey = configuration["securityKey"];
            token = token.Replace(" ", "+");
            token = token.Decrypt(securityKey);
            // 
            Match match = Regex.Match(token, @"^(?<email>[\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)-(?<date>\d{12})-(?<id>\d+)$");
            if (!match.Success)
            {
                ViewBag.Status = true;
                ViewBag.Message = "xetali token!";
            }
            string email = token.Split('-')?[0];
            string dateString = match.Groups["date"].Value;
            int id = int.Parse(match.Groups["id"].Value);

            if (!DateTime.TryParseExact(dateString, "yyyyMMddHHmm", null, DateTimeStyles.None, out DateTime expiredDate))
            {
                ViewBag.Status = true;
                ViewBag.Message = "xetali token!";
            }
            if (expiredDate < DateTime.UtcNow.AddHours(4))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Token-in istifadə vaxtı bitib!";
            }
            var foundedSubscriber = await db.Subscribes.FirstOrDefaultAsync(s => s.Id == id && s.DeletedByUserId == null);
            if (foundedSubscriber == null)
            {
                ViewBag.Status = true;
                ViewBag.Message = "İstifadəçi mövcud deyil!";
            }
            if (!foundedSubscriber.Email.Equals(email))
            {
                ViewBag.Status = true;
                ViewBag.Message = "Göndərilən email istifadəçiyə aid deyil!";
            }

            if (foundedSubscriber.EmailConfirmedDate != null)
            {
                ViewBag.Status = true;
                ViewBag.Message = "Sizin abunəliyiniz artıq təsdiq edilib!";
            }

            foundedSubscriber.EmailConfirmed = true;
            foundedSubscriber.EmailConfirmedDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();

            ViewBag.Status = false;
            ViewBag.Message = "Abunəliyiniz uğurla təsdiq edildi!";

            return View();
        }
    }
}
