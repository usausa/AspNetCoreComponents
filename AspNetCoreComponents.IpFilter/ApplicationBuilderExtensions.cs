namespace AspNetCoreComponents.IpFilter;

using System.Net;

using Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePathRestrict(this IApplicationBuilder builder, string path, IPNetwork[]? networks)
    {
        if (networks?.Length > 0)
        {
            return builder.UseMiddleware<PathRestrictMiddleware>(new PathRestrictConfig
            {
                Path = path,
                Networks = networks
            });
        }

        return builder;
    }
}
