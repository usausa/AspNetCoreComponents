namespace Example.Web.Infrastructure
{
    public static class ViewExtensions
    {
        // TODO format, enum to text, enum to css

        public static string Active(this bool value) => value ? "active" : string.Empty;
    }
}
