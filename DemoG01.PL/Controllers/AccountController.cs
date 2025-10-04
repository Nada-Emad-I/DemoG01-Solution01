using DemoG01.DAL.Models.IdentityModels;
using DemoG01.PL.Utilities;
using DemoG01.PL.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DemoG01.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser>userManager
            ,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Register
        //Register
        public IActionResult Register()
        {
            return View();
        }
        //P@ssw0rd


        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Register
                var user = _userManager.FindByNameAsync(viewModel.UserName).Result;
                if (user is null)
                {
                    //Create Account
                    user = new ApplicationUser()
                    {
                        UserName = viewModel.UserName,
                        Email = viewModel.Email,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName
                    };

                    var result = _userManager.CreateAsync(user, viewModel.Password).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LogIn");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    //Already Created
                    ModelState.AddModelError(string.Empty, "This User Name already exist ,Please Try another one");
                }
            }
            return View(viewModel);
        }
        #endregion

        //Login
        #region LogIn

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
               var user= _userManager.FindByEmailAsync(viewModel.Email).Result;
                if(user is not null)
                {
                    var flag=_userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                    if(flag)
                    {
                       var result= _signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false).Result;
                        if(result.IsNotAllowed)
                        {
                            ModelState.AddModelError(string.Empty, "Your Account is not Allowed");
                        }
                        if(result.IsLockedOut)
                        {
                            ModelState.AddModelError(string.Empty, "Your Account is Locked out");
                        }
                        if(result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "InCorrect Email or Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "InVaild LogIn");
                }
            }
            return View(viewModel);
        }
        #endregion

        #region LogOut
        [Authorize]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region ForgetPassword
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var user=_userManager.FindByEmailAsync(viewModel.Email).Result;
                if(user is not null)
                {
                    var Token =_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email ,Token},Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = ResetPasswordLink//To Do
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(string.Empty, "InValid Operation");
            return View(nameof(ForgetPassword),viewModel);
        }
        public IActionResult CheckYourInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email,string Token)
        {
            TempData["Email"]=email;
            TempData["Token"]=Token;

            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string email = TempData["Email"] as string??string.Empty;
            string Token = TempData["Token"] as string??string.Empty;
            var user =_userManager.FindByEmailAsync(email).Result;
            if(user is not null)
            {
                var Result=_userManager.ResetPasswordAsync(user, Token, viewModel.Password).Result;
                if(Result.Succeeded)
                {
                    return RedirectToAction(nameof(LogIn));
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                }
            return View(nameof(viewModel));
        }
        #endregion

       public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
