using AutoMapper;
using DemoG01.BLL.DataTransferObjects.Employees;
using DemoG01.BLL.Services.Interfaces;
using DemoG01.DAL.Models.EmployeeModels;
using DemoG01.DAL.Repositories.Classes;
using DemoG01.DAL.Repositories.Employees;
using DemoG01.DAL.Repositories.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentSerivce _attachmentSerivce;

        public EmployeeService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttachmentSerivce attachmentSerivce)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _attachmentSerivce = attachmentSerivce;
        }
    public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName, bool withTracking = false)
        {
            //return _employeeRepository.GetAll(e => new EmployeeDto()
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Age = e.Age,
            //    Salary = e.Salary
            //}).Where(e => e.Age > 27);
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {

                 employees = _unitOfWork.EmployeeRepository.GetAll(withTracking);
            }
            else
               {
                 employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            }
            var employeesToReturn = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            ///var employeesToReturn = employees.Select(E => new EmployeeDto()
            ///{
            ///    Id = E.Id,
            ///    Name = E.Name,
            ///    Age = E.Age,
            ///    Email = E.Email,
            ///    IsActive = E.IsActive,
            ///    Salary = E.Salary,
            ///    Gender = E.Gender.ToString(),
            ///   EmployeeType = E.EmployeeType.ToString()
            ///});
            return employeesToReturn;

        }
        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return (employee == null) ? null :_mapper.Map<Employee,EmployeeDetailsDto>(employee);
            ///if (employee == null)
            ///{
            ///    return null;
            ///}
            ///return new EmployeeDetailsDto()
            ///{
            ///    Id = employee.Id,
            ///    Name = employee.Name,
            ///    Age = employee.Age,
            ///    Email = employee.Email,
            ///    IsActive = employee.IsActive,
            ///    Salary = employee.Salary,
            ///    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            ///    Gender = employee.Gender.ToString(),
            ///    EmployeeType = employee.EmployeeType.ToString(),
            ///};

        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto,Employee>(employeeDto);
            if (employeeDto.Image is not null)
            {
                employee.ImageName=_attachmentSerivce.Upload(employeeDto.Image, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if(employee == null)
            {
                return false;
            }
            ///Hard Delete
            ///var result = _employeeRepository.Remove(employee);
            ///if(result>0)
            ///    return true;
            ///return false;
            employee.IsDeleted=true;
            _unitOfWork.EmployeeRepository.Update(employee);
            var result = _unitOfWork.SaveChanges();
                
            if (result > 0) return true;
            return false;

        }
        public int updateEmployee(UpdateEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<UpdateEmployeeDto, Employee>(employeeDto);

            if (employeeDto.Image is not null)
            {

                if (!string.IsNullOrEmpty(employee.ImageName))
                {
                    var filepath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot", "Files", "Images",
                        employee.ImageName
                    );
                    _attachmentSerivce.Delete(filepath);
                }


                employee.ImageName = _attachmentSerivce.Upload(employeeDto.Image, "Images");
            }



            _unitOfWork.EmployeeRepository.Update(employee);

            return _unitOfWork.SaveChanges();
        }

        
    }
}
