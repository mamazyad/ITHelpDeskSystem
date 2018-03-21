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
            string adminUserName = "admin66";
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

            //Adding IT Staff 
            var ITstaffs = new List<ITStaff>
            {
                new ITStaff { UserName = "ITstaff1", Email ="ITstaff1@gmail.com", FirstName ="Jack", LastName ="White", JobTitle ="User support ", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "ITstaff2", Email ="ITstaff2@gmail.com", FirstName ="Jane", LastName ="Black", JobTitle ="Network admin", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
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
        }
    }
}
