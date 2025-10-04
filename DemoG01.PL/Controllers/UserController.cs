using DemoG01.DAL.Models.IdentityModels;
using DemoG01.PL.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoG01.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        #region Index
        public IActionResult Index(string? SearchInput)
        {
            var users = new List<UserViewModel>();
            if (string.IsNullOrEmpty(SearchInput))
            {
                users = _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToList();
            }
            else
            {
                users = _userManager.Users.Where(U => U.NormalizedEmail.Contains(SearchInput.ToUpper()))
                    .Select(U => new UserViewModel()
                    {
                        Id = U.Id,
                        FirstName = U.FirstName,
                        LastName = U.LastName,
                        Email = U.Email,
                        Roles = _userManager.GetRolesAsync(U).Result
                    }).ToList();
            }
            return View(users);
        }
        #endregion

        #region Details
        public IActionResult Details(string? id, string viewname = "Details")
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(viewname, userVM);
        }

        #endregion

        #region Edit
        public IActionResult Edit(string? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] string? id, UserViewModel viewModel)
        {
            if (id is null || id != viewModel.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null) return NotFound();
                user.Email = viewModel.Email;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                var result = _userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }
            }
            return View(viewModel);
        }
        #endregion

        #region Delete
        public IActionResult Delete(string? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] string? id, UserViewModel viewModel)
        {
            if (id is null || id != viewModel.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null) { return NotFound(); }
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(viewModel);
        } 
        #endregion


    }
}
