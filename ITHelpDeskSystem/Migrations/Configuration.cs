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
            string adminUserName = "mainAdmin";
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
                new ITStaff { UserName = "itstaff", Email ="ITstaff1@gmail.com", FirstName ="Jack", LastName ="White", JobTitle ="User support ", Department="IT Department", ExtensionNumber = "544", Mobile = "0553334445", OfficeNumber = "B17", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "itstaff1", Email ="ITstaff1@gmail.com", FirstName ="Jane", LastName ="Black", JobTitle ="Network admin", Department="IT Department", ExtensionNumber = "577", Mobile = "0553334447", OfficeNumber = "A15", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "itstaff2", Email ="ITstaff2@gmail.com", FirstName ="Mark", LastName ="Brown", JobTitle ="SAP specialist", Department="IT Department", ExtensionNumber = "322", Mobile = "0553334448", OfficeNumber = "C05", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "itmanager", Email ="itmanager@gmail.com", FirstName ="Mike", LastName ="Smith", JobTitle ="IT manager", Department="IT Department", ExtensionNumber = "741", Mobile = "0553334448", OfficeNumber = "A13", Speciality = " ", StartingDate = null, Position =" ", IsManager = true,
                    },
            };

            foreach (var ITstaff in ITstaffs)
            {
                if (userManager.FindByName(ITstaff.UserName) == null)
                {
                    userManager.Create(ITstaff, "123456");
                }

                var usertemp = userManager.FindByName(ITstaff.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[1])&& ITstaff.IsManager == false)
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
                }
                if (ITstaff.IsManager==true)
                {
                    userManager.AddToRole(usertemp.Id, roles[3]);
                }
            }

            //Adding IT Help Desk system admimn
            var IThelpDeskAdmin = new List<ITHelpDeskAdmin>
            {
                new ITHelpDeskAdmin { UserName = "admin", Email ="HelpDeskAdmin@gmail.com", FirstName ="Wiliam", LastName ="Allen", JobTitle ="Help desk admin", Department="IT Department", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", Speciality = " ", StartingDate = null, Position =" ", IsManager = false, Degree = "",
                    },
            };

            foreach (var ITHelpDeskAdmin in IThelpDeskAdmin)
            {
                if (userManager.FindByName(ITHelpDeskAdmin.UserName) == null)
                {
                    userManager.Create(ITHelpDeskAdmin, "123456");
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
                new Staff { UserName = "staff", Email ="HelpDesk@gmail.com", FirstName ="Peter", LastName ="Swan", JobTitle ="Recruiter", Department="HR", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.Regular,
                  },

                new Staff { UserName = "staff1", Email ="Staff1@gmail.com", FirstName ="Suzan", LastName ="Adam", JobTitle ="Brand manager", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },
            };

            foreach (var staff in Staffs)
            {
                if (userManager.FindByName(staff.UserName) == null)
                {
                    userManager.Create(staff, "123456");
                }

                var usertemp = userManager.FindByName(staff.UserName);
                if (!userManager.IsInRole(usertemp.Id, roles[2]))
                {
                    userManager.AddToRole(usertemp.Id, roles[2]);
                }
            }

            var categories = new List<Category>
            {
                new Category { CategoryName = "Hardware", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff").Id,},

                 new Category { CategoryName = "Software", CategoryDescription = "No description" ,ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id },

                 new Category { CategoryName = "SAP", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff2").Id},

                 new Category { CategoryName = "WiFi", CategoryDescription = "No description", ITStaffId = 2}

            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            context.SaveChanges();

            var tickets = new List<Ticket>
            {
                new Ticket { Subject = "Issue", IncidentDescription = "Cannot log in to email", CategoryId = categories.Single(d=>d.CategoryName=="Software").CategoryId, Status=TicketStatus.Closed, CreationDate =DateTime.Now, TicketOwner = Staffs.Single(m=>m.UserName=="staff").Id, CreatedBy= Staffs.Single(m=>m.UserName=="staff").Id, IncidentSolution="Follow the steps of resetting password", Priority=TicketPriority.Critical, ResultionDate = DateTime.Now, DueDate= DateTime.Now.AddDays(1), CreatedByName = Staffs.Single(m=>m.UserName=="staff").FullName,},
            };
            tickets.ForEach(s => context.Tickets.AddOrUpdate(p => p.Subject, s));
            context.SaveChanges();

            var knowledgeBase = new List<KnowledgeBase>
            {
                new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff1").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Software", CreationDate=DateTime.Now, IncidentTitle="Uninstall software", IncidentDescription ="For windows users facing any troubles while uninstalling a software", SolutionDescription="Select a program you wish to uninstall from Control Panel and click either Remove, or Change/Remove." },

                new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff1").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Software", CreationDate=DateTime.Now, IncidentTitle="Uninstall software", IncidentDescription ="For windows users facing any troubles while uninstalling a software", SolutionDescription="Select a program you wish to uninstall from Control Panel and click either Remove, or Change/Remove." },
            };
            knowledgeBase.ForEach(s => context.KnowledgeBases.AddOrUpdate(p => p.Topic, s));
            context.SaveChanges();
        }
    }
}
