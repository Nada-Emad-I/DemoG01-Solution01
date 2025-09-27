using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Models.EmployeeModels;
using DemoG01.DAL.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories.Employees
{
    public class EmployeeRepository(ApplicationDbContext _dbContext):GenericRepository<Employee>(_dbContext), IEmployeeRepository
    {
    }
}
