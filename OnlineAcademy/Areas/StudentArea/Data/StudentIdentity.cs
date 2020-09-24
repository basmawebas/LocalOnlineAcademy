using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OnlineAcademy.Areas.StudentArea.Data
{
    public class STUser : IdentityUser
    {
        public string RoleName { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<STUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class StudentDbContext : IdentityDbContext<STUser>
    {
        public StudentDbContext()
            : base("StudentConnection", throwIfV1Schema: false)
        {
        }

        public static StudentDbContext Create()
        {
            return new StudentDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<IdentityUserRole>()
            //.HasKey(r => new { r.UserId, r.RoleId })
            //.ToTable("AspNetUserRoles");

            //modelBuilder.Entity<IdentityUserLogin>()
            //            .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
            //            .ToTable("AspNetUserLogins");
        }

        public System.Data.Entity.DbSet<OnlineAcademy.Areas.StudentArea.Data.StudentProfile> StudentProfiles { get; set; }

        public System.Data.Entity.DbSet<OnlineAcademy.Areas.StudentArea.Data.CurrentTerm> CurrentTerms { get; set; }

        public System.Data.Entity.DbSet<OnlineAcademy.Models.Schoole> Schooles { get; set; }

        public System.Data.Entity.DbSet<OnlineAcademy.Areas.StudentArea.Data.StudyYear> StudyYears { get; set; }
    }
}