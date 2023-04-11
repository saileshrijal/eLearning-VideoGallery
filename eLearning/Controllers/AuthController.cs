using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eLearning.Models;
using eLearning.Repository.Interface;
using eLearning.ViewModels;
using eLearning.Manager.Interface;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace eLearning.Controllers;

[AllowAnonymous]
public class AuthController : Controller
{
    private readonly IAuthManager _authManager;
    private readonly INotyfService _notyfService;
    public AuthController(IAuthManager authManager, INotyfService notyfService)
    {
        _authManager = authManager;
        _notyfService = notyfService;
    }

    public IActionResult Login(string? returnUrl)
    {
        if (User.Identity!.IsAuthenticated)
        {
            return Redirect("/");
        }
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginVM());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm, string? returnUrl)
    {
        if (!ModelState.IsValid) { return View(vm); }

        try
        {
            var result = await _authManager.Login(vm.Username!, vm.Password!);
            if (result.Success)
            {
                _notyfService.Success($"Welcome {vm.Username}!");
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return Redirect("/");
            }
            ModelState.AddModelError(nameof(vm.Username), string.Join(", ", result.Errors));
            return View(vm);
        }
        catch (Exception ex)
        {
            _notyfService.Error(ex.Message);
            return View(vm);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _authManager.Logout();
        _notyfService.Success("You have been logged out!");
        return Redirect("/");
    }
}
