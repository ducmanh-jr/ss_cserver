namespace NguyenDucManh0210668.Utils;

public class PagedResult0210668De1<TData>
{
    public IReadOnlyList<TData> Items { get; set; } = Array.Empty<TData>();
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
}
