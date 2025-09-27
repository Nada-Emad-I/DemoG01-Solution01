using DemoG01.BLL.DataTransferObjects.Departments;
using DemoG01.BLL.Factories;
using DemoG01.BLL.Services.Interfaces;
using DemoG01.DAL.Models;
using DemoG01.DAL.Repositories.Classes;
using DemoG01.DAL.Repositories.Departments;
using DemoG01.DAL.Repositories.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var department = _unitOfWork.DepartmentRepository.GetAll();
            var departmentToReturn = department.Select(D => D.ToDepartmentDto());
            return departmentToReturn;
        }
        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            //if (department is null)
            //{
            //    return null;
            //}
            //else
            //{
            //    return new DepartmentDetailsDto()
            //    {
            //        Id = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        DateOfCreation = DateOnly.FromDateTime(department.CreatedOn ?? DateTime.Now),
            //        CreatedBy = department.CreatedBy,
            //        LastModifiedBy = department.LastModifiedBy,
            //        IsDeleted = department.IsDeleted,
            //    };
            //}
            return department == null ? null : department.ToDepartmentDetailsDto();

        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
             _unitOfWork.DepartmentRepository.Add(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        public int updateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(updateDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null)
            {
                return false;
            }
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
                var Result = _unitOfWork.SaveChanges();
                return Result > 0 ? true : false;
            }
        }
    }
}
