namespace AspNetCoreComponents.IpFilter;

using System.Diagnostics.CodeAnalysis;
using System.Net;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
internal class PathRestrictConfig
{
    [AllowNull]
    public string Path { get; set; }

    [AllowNull]
    public IPNetwork[] Networks { get; set; }
}
