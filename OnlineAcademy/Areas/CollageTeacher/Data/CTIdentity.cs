using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OnlineAcademy.Areas.CollageTeacher.Data
{
    public class CTeachernUser : IdentityUser
    {
        public string RoleName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CTeachernUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class CollageTeacherDbContext : IdentityDbContext<CTeachernUser>
    {
        public CollageTeacherDbContext()
            : base("CollageTeacherConnection", throwIfV1Schema: false)
        {
        }

        public static CollageTeacherDbContext Create()
        {
            return new CollageTeacherDbContext();
        }






    }
}