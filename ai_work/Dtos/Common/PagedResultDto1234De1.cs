namespace DucManhJr1234.Dtos.Common;

public class PagedResultDto1234De1<T>
{
    public int TotalItems { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }

    public List<T> Items { get; set; } = new();
}
