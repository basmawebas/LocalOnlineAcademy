using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OnlineAcademy.Areas.PrivateTeacher.Data
{
    public class PTUser : IdentityUser
    {
        public string RoleName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<PTUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class PrivateTeacherDbContext : IdentityDbContext<PTUser>
    {
        public PrivateTeacherDbContext()
            : base("PrivateTeacherConnection", throwIfV1Schema: false)
        {
        }

        public static PrivateTeacherDbContext Create()
        {
            return new PrivateTeacherDbContext();
        }

    }
}