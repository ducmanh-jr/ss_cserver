namespace nguyenducmanh0210668.Dtos
{
    public class FilterDto0210668De1
    {
        private string _keyword;
        public string Keyword 
        { 
            get => _keyword; 
            set => _keyword = value?.Trim(); 
        }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
