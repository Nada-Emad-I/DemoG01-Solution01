using DemoG01.DAL.Data.Contexts;
using DemoG01.DAL.Models.DepartmentModels;
using DemoG01.DAL.Repositories.Departments;
using DemoG01.DAL.Repositories.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories.Classes
{
    public class DepartmentRepository(ApplicationDbContext _dbContext ): GenericRepository<Department>(_dbContext),IDepartmentRepository
    {
    }
}
