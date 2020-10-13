using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineAcademy.Areas.PrivateTeacher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class PTAccountController : Controller
    {

        private PTSignInManager _signInManager;
        private PTUserManager _userManager;
        private PTRoleManager _roleManager;
        private PrivateTeacherDbContext db;

        string student = "";
        string ptteacher = "";


        public PTAccountController()
        {
            db = new PrivateTeacherDbContext();

        }

        public PTAccountController(PTUserManager userManager, PTSignInManager signInManager, PTRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public PTRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<PTRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public PTSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<PTSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public PTUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PTUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult PTLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Redirect", "PrivateTeacherHome", new { area = "PrivateTeacherArea" });
            }
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PTLogin(PTLoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("PTHome", "PTHome", new { area = "PrivateTeacher" });
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        // GET: PrivateTeacherArea/PTAccount
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult PTRegister()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("", "", new { area = "" });

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var role in RoleManager.Roles)
                list.Add(new SelectListItem() { Value = role.Name, Text = role.Name });
            ViewBag.Roles = list;
            //ViewBag.Roles = new SelectList(db.Roles,"Id", "Name").ToList();
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        
        public async Task<ActionResult> PTRegister(PTRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AspNetUsers { UserName = model.Email, Email = model.Email, RoleName = model.RoleName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //1#
                    //هنا عندي كان واخد result بالشكل دا
                    //تمام
                    //كدا حفظنا الداتا المشكله بقا في الريترن  هسبهالك بقا تعملي سرش عليها 
                    await UserManager.AddToRoleAsync(user.Id, model.RoleName);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("PTHome", "PTHome", new { area = "PrivateTeacher" });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void AddErrors(Microsoft.AspNet.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

       
    }


}