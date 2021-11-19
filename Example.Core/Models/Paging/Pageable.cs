namespace Example.Models.Paging;

public class Pageable
{
    public int Page { get; set; }

    public int Size { get; set; }

    public int Offset => (Page - 1) * Size;
}

public static class PageableExtensions
{
    public static T SetSize<T>(this T pageable, int size)
        where T : Pageable
    {
        pageable.Size = size;
        return pageable;
    }
}
