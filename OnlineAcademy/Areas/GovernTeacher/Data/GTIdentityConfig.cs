using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace OnlineAcademy.Areas.GovernTeacher.Data
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
    public class GTUserManager : UserManager<GTUser>
    {
        public GTUserManager(IUserStore<GTUser> store)
            : base(store)
        {
        }

        public static GTUserManager Create(IdentityFactoryOptions<GTUserManager> options, IOwinContext context)
        {
            var manager = new GTUserManager(new UserStore<GTUser>(context.Get<GovernTeacherDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<GTUser>(manager)
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
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<GTUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<GTUser>
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
                    new DataProtectorTokenProvider<GTUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class GTSignInManager : SignInManager<GTUser, string>
    {
        public GTSignInManager(GTUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(GTUser user)
        {
            return user.GenerateUserIdentityAsync((GTUserManager)UserManager);
        }

        public static GTSignInManager Create(IdentityFactoryOptions<GTSignInManager> options, IOwinContext context)
        {
            return new GTSignInManager(context.GetUserManager<GTUserManager>(), context.Authentication);
        }
    }

    public class GTRoleManager : RoleManager<IdentityRole>
    {
        public GTRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
        public static GTRoleManager Create(IdentityFactoryOptions<GTRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<GovernTeacherDbContext>());
            return new GTRoleManager(roleStore);
        }
    }
}