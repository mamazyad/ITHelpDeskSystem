using ITHelpDeskSystem.Models;
using ITHelpDeskSystem.ViewModels;
//using ITHelpDeskSystem.ViewModels;

namespace ITHelpDeskSystem.App_Start
{
    /// <summary>
    /// Configuration of AutoMapper class
    /// Add all the required mappings between model and view models here
    /// </summary>
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
                cfg.CreateMap<Category, CategoryViewModel>().ReverseMap();
                cfg.CreateMap<Staff, StaffViewModel>().ReverseMap();
                cfg.CreateMap<Ticket, TicketViewModel>().ReverseMap();
                cfg.CreateMap<ITStaff, ITStaffViewModel>().ReverseMap();
            });
        }
    }
}