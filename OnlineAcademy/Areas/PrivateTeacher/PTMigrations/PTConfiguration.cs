namespace OnlineAcademy.Areas.PrivateTeacher.PTMigrations
{
using System;
using System.Collections.Generic;
using System.Linq;
    using System.Data.Entity.Migrations;
using System.Web;
    using OnlineAcademy.Areas.PrivateTeacher.Data;

    internal sealed class PTConfiguration : DbMigrationsConfiguration<PrivateTeacherDbContext>
    {
        public PTConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PrivateTeacherDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}