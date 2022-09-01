using AutoMapper;
using LeaveManagenet.Web.Data;
using LeaveManagenet.Web.Models;

namespace LeaveManagenet.Web.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
            CreateMap<Employee, EmployeeListVM>().ReverseMap();
        }
    }
}
