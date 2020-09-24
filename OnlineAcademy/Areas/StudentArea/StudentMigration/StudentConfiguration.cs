namespace OnlineAcademy.Areas.StudentArea.StudentMigration
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.Data.Entity.Migrations;
    using OnlineAcademy.Areas.StudentArea.Data;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class StudentConfiguration : DbMigrationsConfiguration<StudentDbContext>
    {
        public StudentConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(StudentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Roles.AddOrUpdate(x => x.Name,
                new IdentityRole() { Name="طالب"}
                );

            context.CurrentTerms.AddOrUpdate(x => x.Name,

                new CurrentTerm() { Name="الترم لاول"},
                new CurrentTerm() { Name="الترم الثاني"}
                );

            context.StudyYears.AddOrUpdate(X => X.Name,
                new StudyYear() { Name= "الصف الاول الاعدادي" },
                new StudyYear() { Name= "اصف الثاني لابتدائي" },
                new StudyYear() { Name= "الصف الثالث الابتدائي" },
                new StudyYear() { Name= "الصف الرابع لابتدائي" },
                new StudyYear() { Name= "الصف الخامس الابتدائي" },
                new StudyYear() { Name= "الصف السادس الابتدائي" },
                new StudyYear() { Name= "الصف الاول لاعدادي " },
                new StudyYear() { Name= "الصف الثاني الاعدادي" },
                new StudyYear() { Name= "الصف الثالث الاعدادي " },
                new StudyYear() { Name= "الصف الاول الثانوي " },
                new StudyYear() { Name= "الصف الثاني الثانوي " },
                new StudyYear() { Name= "الصف الثالث الثانوي " }
                );
        }
    }
}