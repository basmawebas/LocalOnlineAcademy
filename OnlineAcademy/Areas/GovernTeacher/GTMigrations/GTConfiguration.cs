namespace OnlineAcademy.Areas.GovernTeacher.GTMigrations
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
    using OnlineAcademy.Areas.GovernTeacher.Data;

    internal sealed class GTConfiguration : DbMigrationsConfiguration<GovernTeacherDbContext>
    {
        public GTConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(GovernTeacherDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }

}