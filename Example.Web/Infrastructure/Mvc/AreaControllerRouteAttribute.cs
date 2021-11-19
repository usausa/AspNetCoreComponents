namespace Example.Web.Infrastructure.Mvc;

using Microsoft.AspNetCore.Mvc;

public sealed class AreaControllerRouteAttribute : RouteAttribute
{
    public AreaControllerRouteAttribute()
        : base("~/[area]/[controller]")
    {
    }
}
