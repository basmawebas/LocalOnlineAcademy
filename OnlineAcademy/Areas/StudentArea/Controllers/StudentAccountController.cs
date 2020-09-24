using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineAcademy.Areas.StudentArea.Data;
using OnlineAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.StudentArea.Controllers
{
    public class StudentAccountController : Controller
    {

        private STSignInManager _signInManager;
        private STUserManager _userManager;
        private STRoleManager _roleManager;
        private StudentDbContext db;

        string student = "";
        string ptteacher = "";


        public StudentAccountController()
        {
            db = new StudentDbContext();

        }

        public StudentAccountController(STUserManager userManager, STSignInManager signInManager, STRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public STRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<STRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public STSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<STSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public STUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<STUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: StudentArea/StudentAccount
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("", "", new { area = "" });

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(StudentRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new STUser { UserName = model.Email, Email = model.Email, RoleName=model.RoleName};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    result = await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("StudentHome", "StudentHome",new { area= "StudentArea" });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}