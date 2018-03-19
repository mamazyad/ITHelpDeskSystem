namespace ITHelpDeskSystem.Migrations
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using ITHelpDeskSystem.Models;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ITHelpDeskSystem.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ITHelpDeskSystem.Models.ApplicationDbContext context)
        {
            string[] roles = { "Admin", "ITStaff", "Staff", "ITManager" };

            string adminEmail = "admin@gmail.com";
            string adminUserName = "admin";
            string adminPassword = "123456";

            // Create roles
            var roleStore = new CustomRoleStore(context);
            var roleManager = new RoleManager<CustomRole, int>(roleStore);

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new CustomRole { Name = role });
                }
            }

            // Define admin user
            var userStore = new CustomUserStore(context);
            var userManager = new ApplicationUserManager(userStore);

            //TODO Change the type of the admin user
            var admin = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminEmail,
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            // Create admin user
            if (userManager.FindByName(admin.UserName) == null)
            {
                userManager.Create(admin, adminPassword);
            }

            // Add admin user to admin role
            // roles[0] is "Admin"
            var user = userManager.FindByName(admin.UserName);
            if (!userManager.IsInRole(user.Id, roles[0]))
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }

            //Adding IT Staff example
            var ITstaffs = new List<ITStaff>
            {
                new ITStaff { UserName = "ITstaff", Email ="ITstaff@gmail.com", FirstName ="FirstIT", LastName ="LastIT", JobTitle =" ", Mobile = " ", ExtensionNumber = " ", OfficeNumber = " ",Speciality = " ", StartingDate = null, Position =" "
                    },
                 new ITStaff { UserName = "ITstaff1", Email ="ITstaff1@gmail.com", FirstName ="FirstIT1", LastName ="LastIT1", JobTitle =" ", Mobile = " ", ExtensionNumber = " ", OfficeNumber = " ",Speciality = " ", StartingDate = null, Position =" "
                    },
            };

            foreach (var ITstaff in ITstaffs)
            {
                if (userManager.FindByName(ITstaff.UserName) == null)
                {
                    userManager.Create(ITstaff, "ITstaff123");
                }

                var usertemp = userManager.FindByName(ITstaff.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[1]))
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
                }
            }


            var categories = new List<Category>
            {
                  //new Category { CategoryName = "General", CategoryDescription = " " },
                  //new Category { CategoryName = "Hardware", CategoryDescription = " " },
                  new Category { CategoryName = "Software", CategoryDescription = " " ,ITStaffId = ITstaffs.Single(d=>d.UserName=="ITstaff").Id },
                  new Category { CategoryName = "SAP System", CategoryDescription = " ", ITStaffId = ITstaffs.Single(d=>d.UserName=="ITstaff").Id }
            };

            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            context.SaveChanges();
        }
    }
}
