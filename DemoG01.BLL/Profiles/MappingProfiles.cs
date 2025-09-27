using AutoMapper;
using DemoG01.BLL.DataTransferObjects.Employees;
using DemoG01.DAL.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dist => dist.EmpType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dist => dist.EmpGender, options => options.MapFrom(src => src.Gender))
                .ForMember(dist => dist.DepartmentName, options => options.MapFrom(src => src.Department ==null ?"No Department" : src.Department.Name))
                .ReverseMap();

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dist => dist.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dist => dist.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dist => dist.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dist => dist.DepartmentName, options => options.MapFrom(src => src.Department == null ? "No Department" : src.Department.Name))
                .ForMember(dist => dist.Image, options => options.MapFrom(src => src.ImageName));


            CreateMap<CreatedEmployeeDto, Employee>()
                  .ForMember(dist => dist.HiringDate, static option => option.MapFrom( scr => scr.HiringDate.ToDateTime(new TimeOnly())));

            CreateMap<UpdateEmployeeDto, Employee>()
                .ForMember(dist => dist.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())));
        }
    }
}
