namespace Example.Web.Areas.Admin
{
    using Example.Web.Authentication;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("admin")]
    [Route("[area]/[controller]/[action]")]
    [Authorize]
    [Authorize(Roles = Role.Admin)]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true, Duration = 0)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public abstract class BaseAdminController : Controller
    {
    }
}
