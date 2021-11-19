namespace Example.Web.Areas.Admin.Controllers;

using Example.Web.Infrastructure.Mvc;

using Microsoft.AspNetCore.Mvc;

public class UserController : BaseAdminController
{
    [AreaControllerRoute]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
