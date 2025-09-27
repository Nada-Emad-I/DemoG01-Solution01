using DemoG01.DAL.Models.EmployeeModels;
using DemoG01.DAL.Repositories.Classes;
using DemoG01.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.DAL.Repositories.Employees
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
    }
}
