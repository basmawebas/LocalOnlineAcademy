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

namespace OnlineAcademy.Areas.StudentArea.Data
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        Task IIdentityMessageService.SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
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
    public class STUserManager : UserManager<STUser>
    {
        public STUserManager(IUserStore<STUser> store)
            : base(store)
        {
        }

        public static STUserManager Create(IdentityFactoryOptions<STUserManager> options, IOwinContext context)
        {
            var manager = new STUserManager(new UserStore<STUser>(context.Get<StudentDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<STUser>(manager)
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
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<STUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<STUser>
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
                    new DataProtectorTokenProvider<STUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class STSignInManager : SignInManager<STUser, string>
    {
        public STSignInManager(STUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(STUser user)
        {
            return user.GenerateUserIdentityAsync((STUserManager)UserManager);
        }

        public static STSignInManager Create(IdentityFactoryOptions<STSignInManager> options, IOwinContext context)
        {
            return new STSignInManager(context.GetUserManager<STUserManager>(), context.Authentication);
        }
    }

    public class STRoleManager : RoleManager<IdentityRole>
    {
        public STRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
        public static STRoleManager Create(IdentityFactoryOptions<STRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<StudentDbContext>());
            return new STRoleManager(roleStore);
        }
    }
}