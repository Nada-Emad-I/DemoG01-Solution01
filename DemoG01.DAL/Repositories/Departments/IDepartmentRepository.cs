using DemoG01.DAL.Models;
using DemoG01.DAL.Models.DepartmentModels;
using DemoG01.DAL.Repositories.Interfaces;

namespace DemoG01.DAL.Repositories.Departments
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
    }
}