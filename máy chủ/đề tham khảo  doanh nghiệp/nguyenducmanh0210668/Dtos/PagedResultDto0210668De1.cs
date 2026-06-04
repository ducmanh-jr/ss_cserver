using System.Collections.Generic;

namespace nguyenducmanh0210668.Dtos
{
    public class PagedResultDto0210668De1<T>
    {
        public int TotalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; } = new List<T>();
    }
}
