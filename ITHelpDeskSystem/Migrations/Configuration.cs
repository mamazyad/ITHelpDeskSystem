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

                 new ITStaff { UserName = "itstaff1", Email ="ITstaff1@gmail.com", FirstName ="Jane", LastName ="Black", JobTitle ="Network admin", Department="IT Department", ExtensionNumber = "577", Mobile = "0553334447", OfficeNumber = "C04", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                 new ITStaff { UserName = "itstaff2", Email ="ITstaff2@gmail.com", FirstName ="Majid", LastName ="Salem", JobTitle ="SAP specialist", Department="IT Department", ExtensionNumber = "322", Mobile = "0553334448", OfficeNumber = "C05", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                  new ITStaff { UserName = "mbrown", Email ="ITstaff3@gmail.com", FirstName ="Mark", LastName ="Brown", JobTitle ="System engineer", Department="IT Department", ExtensionNumber = "322", Mobile = "0553334448", OfficeNumber = "C06", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
                    },

                   new ITStaff { UserName = "mbrown", Email ="ITstaff3@gmail.com", FirstName ="Ali", LastName ="Kareem", JobTitle ="Factory technician", Department="IT Department", ExtensionNumber = "322", Mobile = "0553334448", OfficeNumber = "C06", Speciality = " ", StartingDate = null, Position =" ", IsManager = false,
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
                if (!userManager.IsInRole(usertemp.Id, roles[1]) && ITstaff.IsManager == false)
                {
                    userManager.AddToRole(usertemp.Id, roles[1]);
                }
                if (ITstaff.IsManager == true)
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
                new Staff { UserName = "staff", Email ="HelpDesk@gmail.com", FirstName ="Khalid", LastName ="Saad", JobTitle ="Recruiter", Department="HR", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.Regular,
                  },

                new Staff { UserName = "staff1", Email ="Staff1@gmail.com", FirstName ="Suzan", LastName ="Adam", JobTitle ="Brand manager", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                new Staff { UserName = "staff2", Email ="Staff2@gmail.com", FirstName ="Ahmad", LastName ="Ali", JobTitle ="HR manager", Department="HR", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                new Staff { UserName = "staff3", Email ="Staff3@gmail.com", FirstName ="Malek", LastName ="Ahmad", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                 new Staff { UserName = "staff4", Email ="Staff4@gmail.com", FirstName ="Sara", LastName ="Murad", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                 new Staff { UserName = "staff5", Email ="Staff5@gmail.com", FirstName ="Salam", LastName ="Abdulrahman", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                 new Staff { UserName = "staff6", Email ="Staff6@gmail.com", FirstName ="Masood", LastName ="Khan", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                 new Staff { UserName = "staff7", Email ="Staff7@gmail.com", FirstName ="Muhannad", LastName ="Droobi", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
                  },

                 new Staff { UserName = "staff8", Email ="Staff8@gmail.com", FirstName ="Sana", LastName ="Akram", JobTitle ="Sales", Department="Marketing", ExtensionNumber = " ", Mobile = " ", OfficeNumber = " ", StaffLevel = " ", ManagerialPosition = ManagerialPosition.High,
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

                 new Category { CategoryName = "SAP", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id},

               new Category { CategoryName = "WiFi", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff").Id,},

                 new Category { CategoryName = "Printer", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id,},

                 new Category { CategoryName = "Email", CategoryDescription = "No description", ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id,},


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
                new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff1").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Software", CreationDate=DateTime.Now, IncidentTitle="Uninstall software", IncidentDescription ="For windows users facing any troubles while uninstalling a software", SolutionDescription="Select a program you wish to uninstall from Control Panel and click either Remove, or Change/Remove" },

                new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff").Id, Topic = "Hardware", CreationDate=DateTime.Now, IncidentTitle="Connect to projector", IncidentDescription ="Mac users cannot connect to projector", SolutionDescription="Connect the video cable then select System Preferences and click the Detect Displays" },

                new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff2").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff2").Id, Topic = "WiFi", CreationDate=DateTime.Now, IncidentTitle="Connect to WiFi", IncidentDescription ="Connecting mobile devices to WiFi", SolutionDescription="Go to your device settings, WiFi options then enter your username and password" },

                 new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff").Id, Topic = "Booting", CreationDate=DateTime.Now, IncidentTitle="Booting error from disck", IncidentDescription ="Error disck at booting", SolutionDescription="If you can hear a repeated scraping noise, power off the computer as soon as possible, as there may be a physical problem with the hard disk and you may lose data." },

   
                   new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Connectivity", CreationDate=DateTime.Now, IncidentTitle="USB not recognized", IncidentDescription ="USB not recognized when blugged in to computer", SolutionDescription="This method resolves issues where the currently loaded USB driver has become unstable or corrupt. Select Start, type Device Manager in the Search box Select Device Manager from the returned list. Select Disk Drives from the list of hardware Press and hold (or right-click) the USB external hard drive with the issue, and select Uninstall. After the hard drive is uninstalled, unplug the USB cable. Wait for 1 minute and then reconnect the USB cable. The driver should automatically load Check for the USB drive in Windows Explorer" },

                    new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Printer", CreationDate=DateTime.Now, IncidentTitle="Printer jammed", IncidentDescription ="Printer jammed when printing more than 50 pages", SolutionDescription="Removing a rear access panel Locate the knob or access tab on the back of the printer by or on the panel itself. If it's a knob, move it to the Unlocked position. Remove the panel and carefully pull out the jammed paper. Locate and clear away any small bits of paper that remain. Replace and secure the rear panel. Remove the two-sided printing accessory to clear the paper jam Press both RELEASE buttons on either end of the module at the same time and remove it." },

                     new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Email", CreationDate=DateTime.Now, IncidentTitle="Connot login to email", IncidentDescription ="Connot log in to email feom this computer", SolutionDescription="Clear the cache and remove cookies only from websites that cause problems. Clear the Cache: Edit > Preferences > Advanced > Network > Cached Web Content: Clear Now, Remove Cookies from sites causing problems: Edit > Preferences > Privacy >  Use custom settings for history  > Cookies:  Show Cookies." },

                     new KnowledgeBase { CreatedBy= ITstaffs.Single(d=>d.UserName=="itstaff").Id, ITStaffId = ITstaffs.Single(d=>d.UserName=="itstaff1").Id, Topic = "Software", CreationDate=DateTime.Now, IncidentTitle="Deleting browse history", IncidentDescription ="Connot log in to email feom this computer", SolutionDescription="On your computer, open Chrome. At the top right, click More  . Click History   History. On the left, click Clear browsing data. A box will appear. From the drop-down menu, select how much history you want to delete. To clear everything, select the beginning of time. Check the boxes for the info you want Chrome to clear, including “browsing history." },



            };
            knowledgeBase.ForEach(s => context.KnowledgeBases.AddOrUpdate(p => p.Topic, s));
            context.SaveChanges();

            var criteria = new List<Criterion>
            {
                new Criterion { CreationDate=DateTime.Now, ActiveCriterion = true, CriterionDescription = "Waiting time", EditionDate = DateTime.Now },
                new Criterion { CreationDate=DateTime.Now, ActiveCriterion = true, CriterionDescription = "Effectiveness of the solution", EditionDate = DateTime.Now },
                new Criterion { CreationDate=DateTime.Now, ActiveCriterion = true, CriterionDescription = "Friendliness of IT staff", EditionDate = DateTime.Now },
                new Criterion { CreationDate=DateTime.Now, ActiveCriterion = true, CriterionDescription = "Overall satisfaction", EditionDate = DateTime.Now },
            };
            criteria.ForEach(s => context.Criteria.AddOrUpdate(p => p.CriterionDescription, s));
            context.SaveChanges();
        }
    }
}