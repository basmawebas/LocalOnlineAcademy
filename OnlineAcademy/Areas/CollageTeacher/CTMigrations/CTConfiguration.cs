namespace OnlineAcademy.Areas.CollageTeacher.CTMigrations
{
using OnlineAcademy.Areas.CollageTeacher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
    using System.Data.Entity.Migrations;


    internal sealed class CTConfiguration : DbMigrationsConfiguration<CollageTeacherDbContext>
    {
        public CTConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CollageTeacherDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}