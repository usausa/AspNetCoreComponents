namespace Example.Web.Areas.Default.Controllers;

using Example.Web.Infrastructure.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public sealed class DashboardController : BaseDefaultController
{
    [DefaultRoute]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
