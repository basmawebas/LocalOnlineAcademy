namespace OnlineAcademy.Areas.StudentArea.StudentMigration
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.Data.Entity.Migrations;
    using OnlineAcademy.Areas.StudentArea.Data;

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
        }
    }
}