using EventsMS.Models.Auth;
using EventsMS.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static EventsMS.Auth_IdentityModel.IdentityModel;

namespace EventsMS.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IAuthService _authService;
    private readonly IRolePermissionService _permissionService;
    public AccountController(
      SignInManager<User> signInManager,
      UserManager<User> userManager,
      IAuthService authService,
    IRolePermissionService permissionService
    
      )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _authService = authService;
        _permissionService = permissionService;
    }
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
    //[HttpPost]
    //[AllowAnonymous]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Register(RegisterViewModel model)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(model);
    //    }

    //    var result = await _authService.Register(model);

    //    if (!result.Success)
    //    {
    //        result.Errors.ForEach(e => ModelState.AddModelError("", e));
    //        return View(model);
    //    }

    //    var user = await _signInManager.UserManager
    //        .FindByIdAsync(result.UserId.ToString());

    //    if (user != null)
    //    {
    //        await _signInManager.SignInAsync(user, false);
    //    }
    //    return RedirectToAction("Index", "Dashboard");
    //}

    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _authService.Register(model);

        if (!result.Success)
        {
            result.Errors.ForEach(e => ModelState.AddModelError("", e));
            return View(model);
        }

        var user = await _userManager.FindByIdAsync(result.UserId.ToString());

        if (user != null)
        {
            await _signInManager.SignInAsync(user, false);

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            // 🔐 Permission check
            var permission = _permissionService.CheckPermission(roles);

            // Admin / Manager → Dashboard
            if (roles.Contains("Administrator") || roles.Contains("Mangement"))
            {
                if (permission.View)
                    return RedirectToAction("Index", "Dashboard");
            }

            // Student → Home
            if (roles.Contains("Student"))
                return RedirectToAction("Index", "Home");

            // Default fallback
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction("Index", "Home");
    }




    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["AlertMessage"] = "Please enter valid login details.";
                TempData["AlertType"] = "error";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                TempData["AlertMessage"] = "Invalid email or password.";
                TempData["AlertType"] = "error";
                return View(model);
            }

            // ✅ Login successful, fetch user & roles
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                TempData["AlertMessage"] = "User not found after login.";
                TempData["AlertType"] = "error";
                return View(model);
            }


            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            var permission = _permissionService.CheckPermission(roles);

            // 🔐 Role-based redirect
            if ((roles.Contains("Administrator") || roles.Contains("Mangement")) && permission.View)
                return RedirectToAction("Index", "Dashboard");

            if (roles.Contains("Student"))
                return RedirectToAction("Index", "Home");

            // Default fallback
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["AlertMessage"] = "An error occurred during login.";
            TempData["AlertType"] = "error";
            // Log ex.Message if you have a logger
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

}
