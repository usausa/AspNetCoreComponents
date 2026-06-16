namespace Example.Web.Infrastructure;

public static class ViewExtensions
{

    public static string Active(this bool value) => value ? "active" : string.Empty;
}
