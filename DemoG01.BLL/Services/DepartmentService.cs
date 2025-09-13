using DemoG01.DAL.Models;
using DemoG01.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services
{
    internal class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
                _departmentRepository = departmentRepository;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            var department = _departmentRepository.GetAll();
            return department;
        }

    }
}
