namespace Example.Web.Areas.Default.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public sealed class MiddlewareController : BaseDefaultController
{
    [HttpGet]
    public async ValueTask<IActionResult> Time()
    {
        await Task.Delay(5000);

        return View();
    }
}
