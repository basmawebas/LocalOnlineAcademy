using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OnlineAcademy.Areas.CollageTeacher.Data
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class CTUserManager : UserManager<CTeachernUser>
    {
        public CTUserManager(IUserStore<CTeachernUser> store)
            : base(store)
        {
        }

        public static CTUserManager Create(IdentityFactoryOptions<CTUserManager> options, IOwinContext context)
        {
            var manager = new CTUserManager(new UserStore<CTeachernUser>(context.Get<CollageTeacherDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CTeachernUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<CTeachernUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<CTeachernUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CTeachernUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class CTSignInManager : SignInManager<CTeachernUser, string>
    {
        public CTSignInManager(CTUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(CTeachernUser user)
        {
            return user.GenerateUserIdentityAsync((CTUserManager)UserManager);
        }

        public static CTSignInManager Create(IdentityFactoryOptions<CTSignInManager> options, IOwinContext context)
        {
            return new CTSignInManager(context.GetUserManager<CTUserManager>(), context.Authentication);
        }
    }

    public class CTRoleManager : RoleManager<IdentityRole>
    {
        public CTRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
        public static CTRoleManager Create(IdentityFactoryOptions<CTRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<CollageTeacherDbContext>());
            return new CTRoleManager(roleStore);
        }
    }
}