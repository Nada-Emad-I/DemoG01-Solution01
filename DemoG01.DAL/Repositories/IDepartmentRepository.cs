using DemoG01.DAL.Models;

namespace DemoG01.DAL.Repositories
{
    public interface IDepartmentRepository
    {
        int Add(Department department);
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department? GetById(int Id);
        int Remove(Department department);
        int Update(Department department);
    }
}