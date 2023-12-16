namespace Example.Web.Areas.Default.Controllers;

using System.Diagnostics;

using Example.Web.Areas.Default.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
[Area("default")]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
[ApiExplorerSettings(IgnoreApi = true)]
public sealed class ErrorController : Controller
{
#pragma warning disable CA5395
    [Route("~/[controller]/{statusCode:int}")]
    public IActionResult Index(int statusCode)
    {
        return View(new ErrorViewModel
        {
            StatusCode = statusCode,
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
#pragma warning restore CA5395
}
