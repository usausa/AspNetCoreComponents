namespace Example.Web.Areas.Default.Controllers;

using System;
using System.Threading.Tasks;

using Example.Web.Areas.Default.Models;
using Example.Web.Authentication;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class AccountController : BaseDefaultController
{
    private AccountManager AccountManager { get; }

    public AccountController(AccountManager accountManager)
    {
        AccountManager = accountManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async ValueTask<IActionResult> Login([FromForm] AccountLoginForm form, string? returnUrl)
    {
        if (ModelState.IsValid && await AccountManager.LoginAsync(form.UserId, form.Password))
        {
            return Redirect(String.IsNullOrEmpty(returnUrl) ? "~/" : returnUrl);
        }

        ModelState.AddModelError(string.Empty, "UserId or Password is invalid.");

        return View(form);
    }

    [HttpGet]
    public async ValueTask<IActionResult> Logout()
    {
        await AccountManager.LogoutAsync();

        return LocalRedirect("~/");
    }
}
