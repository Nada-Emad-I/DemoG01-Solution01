using DemoG01.BLL.DataTransferObjects.Departments;
using DemoG01.BLL.Services.Interfaces;
using DemoG01.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoG01.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _env;

        
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger
            , IWebHostEnvironment env)
        {
            _departmentService = departmentService;
            _logger = logger;
            _env = env;
        }
        #region Index
        public IActionResult Index()
        {
            //ViewData["Message"] = new DepartmentDto { Name = "Hello From ViewData" };
            //ViewBag.Message= new DepartmentDto { Name = "Hello From ViewBag" };
            var department = _departmentService.GetAllDepartments();
            return View(department);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentEditViewModels departmentVM)
        {
            var Message = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Code = departmentVM.Code,
                        Name = departmentVM.Name,
                        DateOfCreation = departmentVM.DateOfCreation,
                        Description = departmentVM.Description
                    };
                    int result = _departmentService.AddDepartment(departmentDto);
                    if (result > 0)
                    {
                        Message = "Department Created Successfully";
                    }
                    else
                        Message = "Department can not create Now, try later ";
                    TempData["Message"]=Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (_env.IsDevelopment())
                    {
                        _logger.LogError($"Department can not be created because :{ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"Department can not be created because :{ex}");
                        return View("ErrorView");
                    }
                }
            }
            return View(departmentVM);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);

        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit (int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            //return View(department);
            var departmentVM = new DepartmentEditViewModels()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.DateOfCreation
            };
            return View(departmentVM);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int?id,UpdateDepartmentDto departmentVM)
        {
            if (!ModelState.IsValid)
            try
            {
                    if (!id.HasValue) return BadRequest();
                var UpdatedDeptDto = new UpdateDepartmentDto()
                {
                    Id=id.Value,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    DateOfCreation = departmentVM.DateOfCreation
                };
                int result = _departmentService.updateDepartment(UpdatedDeptDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can not be updated");
                }
            }
            catch (Exception ex)
            {
                //development=> action,log error in console ,view
                if (_env.IsDevelopment())
                {
                    _logger.LogError($"Department can not be updated because :{ex.Message}");
                }
                //deployement=> action,log error in file,db, return view(ERROR)
                else
                {
                    _logger.LogError($"Department can not be updated because :{ex}");
                    return View("ErrorView",ex);
                }
            }
            return View(departmentVM);
        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if(!id.HasValue) return BadRequest();
        //    var department=_departmentService.GetDepartmentById(id.Value);
        //    if(department is null) return NotFound();
        //    return View (department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id==0) return BadRequest();
            try
            {
                bool IsDeleted = _departmentService.DeleteDepartment(id);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can not be removed");
                }
            }
            catch (Exception ex)
            {
                //development=> action,log error in console ,view
                if (_env.IsDevelopment())
                {
                    _logger.LogError($"Department can not be removed because :{ex.Message}");
                }
                //deployement=> action,log error in file,db, return view(ERROR)
                else
                {
                    _logger.LogError($"Department can not be removed because :{ex}");
                    return View("ErrorView");
                }
            }
            return RedirectToAction(nameof(Delete), new { id });
        }
        #endregion

    }
}
