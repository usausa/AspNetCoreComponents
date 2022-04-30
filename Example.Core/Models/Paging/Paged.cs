namespace Example.Models.Paging;

using System.Collections;

public class Paged<T> : IPaged, IPageOver, IEnumerable<T>
{
    private readonly IList<T> items;

    public int Page { get; }

    public int Size { get; }

    public int Count { get; }

    public T this[int index] => items[index];

    public Paged(Pageable pageable, IList<T> items, int count)
    {
        Page = pageable.Page;
        Size = pageable.Size;
        this.items = items;
        Count = count;
    }

    public bool HasPrev => Page > 1;

    public bool HasNext => Page * Size < Count;

    public int TotalPage => Count == 0 ? 1 : (int)Math.Ceiling((decimal)Count / Size);

    public bool IsOver => items.Count == 0 && Page > 1;

    public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
