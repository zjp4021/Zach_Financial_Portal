namespace Zach_Financial_Portal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;
    using Zach_Financial_Portal.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Zach_Financial_Portal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Zach_Financial_Portal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            #region Role Manager Section

            //Fire up a RoleManager so I can create Roles...
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                roleManager.Create(new IdentityRole { Name = "Member" });
            }

            if (!context.Roles.Any(r => r.Name == "HeadOfHousehold"))
            {
                roleManager.Create(new IdentityRole { Name = "HeadOfHousehold" });
            }

            if (!context.Roles.Any(r => r.Name == "Guest"))
            {
                roleManager.Create(new IdentityRole { Name = "Guest" });
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            #endregion


            #region Demo Role Section...Testing this out...
            //if (!context.Roles.Any(r => r.Name == "DemoAdmin"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "DemoAdmin" });
            //}

            //if (!context.Roles.Any(r => r.Name == "DemoPM"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "DemoPM" });
            //}

            //if (!context.Roles.Any(r => r.Name == "DemoDeveloper"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "DemoDeveloper" });
            //}

            //if (!context.Roles.Any(r => r.Name == "DemoSubmitter"))
            //{
            //    roleManager.Create(new IdentityRole { Name = "DemoSubmitter" });
            //}

            #endregion

            #region User Creation Section

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            var demoPassword = WebConfigurationManager.AppSettings["DemoUserPassword"];

            if (!context.Users.Any(u => u.Email == "ZachPruitt@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "ZachPruitt@Mailinator.com",
                    Email = "ZachPruitt@Mailinator.com",
                    FirstName = "Zach",
                    LastName = "Pruitt",
                    DisplayName = "ZJP",
                    AvatarPath = "/Avatars/AdminUpdated.png"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "Zion@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Zion@Mailinator.com",
                    Email = "Zion@Mailinator.com",
                    FirstName = "Zion",
                    LastName = "Williamson",
                    DisplayName = "Zion",
                    AvatarPath = "/Avatars/AvatarGuyPinkTie.png"
                }, demoPassword);
            }

            if (!context.Users.Any(u => u.Email == "Jimi@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Jimi@Mailinator.com",
                    Email = "Jimi@Mailinator.com",
                    FirstName = "Jimi",
                    LastName = "Hendrix",
                    DisplayName = "Jimi",
                    AvatarPath = "/Avatars/Submitter-Male.png"
                }, demoPassword);
            }

         

            ////Seeded Demo Users-----------------------------------------------------------------------------

            if (!context.Users.Any(u => u.Email == "Susan@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Susan@Mailinator.com",
                    Email = "Susan@Mailinator.com",
                    FirstName = "Susan",
                    LastName = "Davidson",
                    DisplayName = "Susan",
                    AvatarPath = "/Avatars/AvatarLady2.png"
                }, demoPassword);
            }

          

            if (!context.Users.Any(u => u.Email == "Ashley@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "Ashley@Mailinator.com",
                    Email = "Ashley@Mailinator.com",
                    FirstName = "Ashley",
                    LastName = "Welborn",
                    DisplayName = "Ashley",
                    AvatarPath = "/Avatars/AvatarLady1.png"
                }, demoPassword);
            }

          
            #endregion

            #region Role Assignment Section
            var userId = userManager.FindByEmail("ZachPruitt@Mailinator.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("Jimi@Mailinator.com").Id;
            userManager.AddToRole(userId, "Member");

            userId = userManager.FindByEmail("Zion@Mailinator.com").Id;
            userManager.AddToRole(userId, "Member");

            userId = userManager.FindByEmail("Susan@Mailinator.com").Id;
            userManager.AddToRole(userId, "Guest");

            userId = userManager.FindByEmail("Ashley@Mailinator.com").Id;
            userManager.AddToRole(userId, "Guest");

            #endregion







        }









    }
}
