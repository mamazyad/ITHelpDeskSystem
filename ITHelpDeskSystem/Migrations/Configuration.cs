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

            //Adding IT Staff 
            var ITstaffs = new List<ITStaff>
            {
                new ITStaff { UserName = "ITstaff", Email ="ITstaff1@gmail.com", FirstName ="Jack", LastName ="White", JobTitle ="User support ", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "ITstaff1", Email ="ITstaff1@gmail.com", FirstName ="Jane", LastName ="Black", JobTitle ="Network admin", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "ITstaff2", Email ="ITstaff2@gmail.com", FirstName ="Mark", LastName ="Brown", JobTitle ="SAP specialist", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "ITManager", Email ="ITManager@gmail.com", FirstName ="Mike", LastName ="Smith", JobTitle ="IT manager", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = true,
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

            //Adding IT Help Desk system admimn
            var IThelpDeskAdmin = new List<ITHelpDeskAdmin>
            {
                new ITHelpDeskAdmin { UserName = "HelpDeskAdmin", Email ="HelpDeskAdmin@gmail.com", FirstName ="Woody", LastName ="Allen", JobTitle ="Help desk admin", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false, Degree = "",
                    },
            };

            foreach (var ITHelpDeskAdmin in IThelpDeskAdmin)
            {
                if (userManager.FindByName(ITHelpDeskAdmin.UserName) == null)
                {
                    userManager.Create(ITHelpDeskAdmin, "Admin123");
                }

                var usertemp = userManager.FindByName(ITHelpDeskAdmin.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[0]))
                {
                    userManager.AddToRole(usertemp.Id, roles[0]);
                }
            }

            //Adding Staff 
            var Staffs = new List<Staff>
            {
                new Staff { UserName = "Staff", Email ="Staff@gmail.com", FirstName ="Jack", LastName ="White", JobTitle ="Recruiter", Department="HR", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.Regular,
                  },

                new Staff { UserName = "Staff1", Email ="Staff1@gmail.com", FirstName ="Will", LastName ="Adam", JobTitle ="Brand manager", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },
            };

            foreach (var staff in Staffs)
            {
                if (userManager.FindByName(staff.UserName) == null)
                {
                    userManager.Create(staff, "Staff123");
                }

                var usertemp = userManager.FindByName(staff.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[2]))
                {
                    userManager.AddToRole(usertemp.Id, roles[2]);
                }
            }

            //var categories = new List<Category>
            //{
            //    new Category { CategoryName = "Hardware", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="ITstaff2").Id,},

            //     new Category { CategoryName = "Software", CategoryDescription = " " ,ITStaffId = ITstaffs.Single(d=>d.UserName=="ITstaff").Id },

            //     // new Category { CategoryName = "SAP", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="ITstaff2").Id}
            //};
            //categories.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            //context.SaveChanges();
        }
    }
}
