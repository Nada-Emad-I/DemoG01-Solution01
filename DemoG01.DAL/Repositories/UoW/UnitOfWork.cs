using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Repositories.Classes;
using DemoG01.DAL.Repositories.Departments;
using DemoG01.DAL.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private Lazy<IDepartmentRepository> _departmentRepository;
        private Lazy<IEmployeeRepository> _employeeRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(IDepartmentRepository departmentRepository,
            IEmployeeRepository employeeRepository,
            ApplicationDbContext dbContext)
        {
                _dbContext = dbContext; 
            _departmentRepository = new Lazy<IDepartmentRepository>(()=>new DepartmentRepository(_dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(()=>new EmployeeRepository(_dbContext));
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;
        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
