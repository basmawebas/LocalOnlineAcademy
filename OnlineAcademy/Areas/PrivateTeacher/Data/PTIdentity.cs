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
    public class AspNetUsers : IdentityUser
    {
        public string RoleName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUsers> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class PrivateTeacherDbContext : IdentityDbContext<AspNetUsers>
    {
        public PrivateTeacherDbContext()
            : base("PrivateTeacherConnection", throwIfV1Schema: false)
        {
        }

        public static PrivateTeacherDbContext Create()
        {
            return new PrivateTeacherDbContext();
        }
        public System.Data.Entity.DbSet<OnlineAcademy.Areas.PrivateTeacher.Data.PTProfile> PTProfiles { get; set; }

        public System.Data.Entity.DbSet<OnlineAcademy.Areas.PrivateTeacher.Data.PTAssistant> PTAssistants { get; set; }
        public System.Data.Entity.DbSet<OnlineAcademy.Areas.PrivateTeacher.Data.ApplierInfo> ApplierInfos { get; set; }
    }
}