namespace AspNetCoreComponents.IpFilter;

using System.Net;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
internal sealed class PathRestrictConfig
{
    public string Path { get; set; } = default!;

    public IPNetwork[] Networks { get; set; } = default!;
}
