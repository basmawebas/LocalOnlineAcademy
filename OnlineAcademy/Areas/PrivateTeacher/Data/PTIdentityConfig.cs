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

namespace OnlineAcademy.Areas.PrivateTeacher.Data
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
    public class PTUserManager : UserManager<AspNetUsers>
    {
        public PTUserManager(IUserStore<AspNetUsers> store)
            : base(store)
        {
        }

        public static PTUserManager Create(IdentityFactoryOptions<PTUserManager> options, IOwinContext context)
        {
            var manager = new PTUserManager(new UserStore<AspNetUsers>(context.Get<PrivateTeacherDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AspNetUsers>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                //RequireNonLetterOrDigit = true,
                //RequireDigit = true,
                //RequireLowercase = true,
                //RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<AspNetUsers>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<AspNetUsers>
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
                    new DataProtectorTokenProvider<AspNetUsers>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class PTSignInManager : SignInManager<AspNetUsers, string>
    {
        public PTSignInManager(PTUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AspNetUsers user)
        {
            return user.GenerateUserIdentityAsync((PTUserManager)UserManager);
        }

        public static PTSignInManager Create(IdentityFactoryOptions<PTSignInManager> options, IOwinContext context)
        {
            return new PTSignInManager(context.GetUserManager<PTUserManager>(), context.Authentication);
        }
    }

    public class PTRoleManager : RoleManager<IdentityRole>
    {
        public PTRoleManager(IRoleStore<IdentityRole, string> store) : base(store)
        {
        }
        public static PTRoleManager Create(IdentityFactoryOptions<PTRoleManager> options, IOwinContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context.Get<PrivateTeacherDbContext>());
            return new PTRoleManager(roleStore);
        }
    }
}