using DemoG01.BLL.DataTransferObjects.Departments;
using DemoG01.BLL.DataTransferObjects.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoG01.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
        IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName,bool withTracking = false);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int updateEmployee(UpdateEmployeeDto employeeDto);
    }
}
