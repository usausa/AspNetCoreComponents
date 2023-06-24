namespace AspNetCoreComponents.IpFilter;

using System.Net;
using System.Runtime.CompilerServices;

using Microsoft.AspNetCore.Http;

internal sealed class PathRestrictMiddleware
{
    private readonly RequestDelegate next;

    private readonly string path;

    private readonly IPNetwork[] networks;

    public PathRestrictMiddleware(RequestDelegate next, PathRestrictConfig config)
    {
        this.next = next;
        path = config.Path;
        networks = config.Networks;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(path, StringComparison.OrdinalIgnoreCase) &&
            ((context.Request.HttpContext.Connection.RemoteIpAddress is null) ||
             !IsAddressAllowed(context.Request.HttpContext.Connection.RemoteIpAddress)))
        {
            context.Response.StatusCode = 403;
            return;
        }

        await next(context).ConfigureAwait(false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool IsAddressAllowed(IPAddress address)
    {
        for (var i = 0; i < networks.Length; i++)
        {
            if (networks[i].Contains(address))
            {
                return true;
            }
        }

        return false;
    }
}
