namespace Example.Web.Infrastructure.Mvc
{
    using Microsoft.AspNetCore.Mvc;

    public sealed class ControllerRouteAttribute : RouteAttribute
    {
        public ControllerRouteAttribute()
            : base("~/[controller]")
        {
        }
    }
}
