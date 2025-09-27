using DemoG01.BLL.DataTransferObjects.Departments;

namespace DemoG01.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentDetailsDto? GetDepartmentById(int id);
        int updateDepartment(UpdateDepartmentDto updateDepartmentDto);
    }
}