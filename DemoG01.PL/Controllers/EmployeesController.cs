using DemoG01.BLL.DataTransferObjects.Departments;
using DemoG01.BLL.DataTransferObjects.Employees;
using DemoG01.BLL.Services.Classes;
using DemoG01.BLL.Services.Interfaces;
using DemoG01.DAL.Models.EmployeeModels;
using DemoG01.DAL.Models.Shared;
using DemoG01.PL.ViewModels;
using DemoG01.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace DemoG01.PL.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;

        public EmployeesController(IEmployeeService employeeService,IWebHostEnvironment env
            ,ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _env = env;
            _logger = logger;
        }

        #region Index
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        } 
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeEditViewModels employeeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name = employeeVM.Name,
                        Address = employeeVM.Address,
                        Age = employeeVM.Age,
                        Email = employeeVM.Email,
                        PhoneNumber = employeeVM.PhoneNumber,
                        HiringDate = employeeVM.HiringDate,
                        Gender = employeeVM.Gender,
                        EmployeeType = employeeVM.EmployeeType,
                        IsActive = employeeVM.IsActive,
                        Salary = employeeVM.Salary,
                        DepartmentId=employeeVM.DepartmentId,
                        Image=employeeVM.Image
                    };
                    int result = _employeeService.CreateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can not be created");
                    }
                }
                catch (Exception ex)
                {
                    //development=> action,log error in console ,view
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError($"Employee can not be created because :{ex.Message}");
                    }
                    //deployement=> action,log error in file,db, return view(ERROR)
                    else
                    {
                        _logger.LogError($"Employee can not be created because :{ex}");
                        return View("ErrorView");
                    }
                }
            }
            return View(employeeVM);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);

        }
        #endregion


        #region Edit
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(new EmployeeEditViewModels()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
            });

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeEditViewModels employeeVM)
        {
            if (id is null) return BadRequest();
            if (ModelState.IsValid)
                try
                {
                    var employeeDto = new UpdateEmployeeDto()
                    {
                        Id=id.Value,
                        Name = employeeVM.Name,
                        Address = employeeVM.Address,
                        Age = employeeVM.Age,
                        Email = employeeVM.Email,
                        PhoneNumber = employeeVM.PhoneNumber,
                        HiringDate = employeeVM.HiringDate,
                        Gender = employeeVM.Gender,
                        EmployeeType = employeeVM.EmployeeType,
                        IsActive = employeeVM.IsActive,
                        Salary = employeeVM.Salary,
                        DepartmentId = employeeVM.DepartmentId
                    };
                    int result = _employeeService.updateEmployee(employeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can not be updated");
                    }
                }
                catch (Exception ex)
                {
                    //development=> action,log error in console ,view
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError($"Employee can not be updated because :{ex.Message}");
                    }
                    //deployement=> action,log error in file,db, return view(ERROR)
                    else
                    {
                        _logger.LogError($"Employee can not be updated because :{ex}");
                        return View("ErrorView", ex);
                    }
                }
            return View(employeeVM);
        }
        #endregion

        [HttpPost]
        public IActionResult Delete([FromRoute]int? id)
        {
            if(id is null) return BadRequest();
            try
            {
                var result = _employeeService.DeleteEmployee(id.Value);
                if (result) return RedirectToAction(nameof(Index));
                else _logger.LogError("Employee Can't be deleted ");
            }
            catch (Exception ex)
            {
                //development=> action,log error in console ,view
                if (_env.IsDevelopment()) ModelState.AddModelError(string.Empty,ex.Message);
                else _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
