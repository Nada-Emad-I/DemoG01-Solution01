using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DepartmentRepository(ApplicationDbContext dbContext)//1.Injection
                                                                   //Ask CLR For Create Object

        {
            _dbContext = dbContext;
        }
        //CRUD Operations
        //Get All
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return _dbContext.Departments.ToList();
            }
            else
            {
                return _dbContext.Departments.AsNoTracking().ToList();
            }
        }

        //Get By Id
        public Department? GetById(int Id)
        {
            var department = _dbContext.Departments.Find(Id);
            return department;
        }

        //Insert
        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        //Update
        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }

        //Remove
        public int Remove(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }
    }
}
