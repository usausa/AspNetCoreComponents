namespace Example.Models.Paging
{
    public interface IPageOver
    {
        int TotalPage { get; }

        bool IsOver { get; }
    }
}
