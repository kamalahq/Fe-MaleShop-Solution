using Fe_MaleShop.WebUI.AppCode.Extensions;
using Fe_MaleShop.WebUI.Models.DataContexts;
using Fe_MaleShop.WebUI.Models.Entities.Membership;
using Fe_MaleShop.WebUI.Models.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
//using Fe_MaleShop.WebUI.AppCode.Extensions;

namespace Fe_MaleShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        readonly SignInManager<Fe_MaleUser> signinManager;
        readonly UserManager<Fe_MaleUser> userManager;
        readonly Fe_MaleShopDbContext db;
        public AccountController(SignInManager<Fe_MaleUser> signinManager,
           UserManager<Fe_MaleUser> userManager,
           Fe_MaleShopDbContext db)
          
        {
            this.signinManager = signinManager;
            this.userManager = userManager;
            this.db = db;
        }
        


        [AllowAnonymous]
        [Route("signin.html")]
        public IActionResult Signin()
        {
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        [Route("signin.html")]
        public async Task<IActionResult> Signin(LoginFormModel model)
        {
            if (ModelState.IsValid)
            {
                Fe_MaleUser founded = null;
                bool appLiedPhoneLogin = false;
                //if (model.UserName.IsPhone())
                //{
                //    appLiedPhoneLogin = true;
                //    founded = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(model.UserName));
                //}
                //else if (model.UserName.IsEmail())
                //{
                //    founded = await userManager.FindByEmailAsync(model.UserName);
                //}
                //else
                //{
                //    founded = await userManager.FindByNameAsync(model.UserName);
                //}


                //if (founded == null)
                //{
                //    goto end;
                //}

                //if (appLiedPhoneLogin && founded.PhoneNumberConfirmed = false)
                //{
                //    ViewBag.Message = "Telefon nomreniz tesdiqlenmeyib!";
                //    return View();
                //}
                //else if (founded.EmailConfirmed == false)
                //{
                //    ViewBag.Message = "Email tesdiqlenmeyib!";
                //    return View();
                //}
                var signinResult = await signinManager.PasswordSignInAsync(founded, model.Password, true, true);
                if (signinResult.Succeeded)
                {
                    string callbackUrl = Request.Query["ReturnUrl"];
                    if (!string.IsNullOrEmpty(callbackUrl))
                    {
                        return Redirect(callbackUrl);
                    }


                    return Redirect("/admin");
                }
                if (signinResult.IsLockedOut)
                {
                    ViewBag.Message = "Sehv qeyd etdiyinizden hesabiniz muveqqeti  mehdudlashdirilib" +
                        "5  deq sonra yeniden yolayin!";
                    return View();
                }
                if (signinResult.IsNotAllowed)
                {
                    ViewBag.Message = "Sisteme girishiniz mehdudlashdirilib,zehmet olmasa" +
                        "sistem inzibatcilarina muraciet edin!";
                    return View();
                }


            }
        end:
            ViewBag.Message = "Istifadeci adi ve ya shifre yanlishdir";
            return View();
        }

        [AllowAnonymous]
        [Route("accessdenied.html")]
        public IActionResult Denied()
        {
            return Content("icazeniz olmayan sehifeye kecme cehdi");
        }
        [Route("signout.html")]
        public async Task<IActionResult> Signout()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("Signin", "Account");
        }

        public IActionResult Profile()
        {
            return View();
        }

        
    }
}
