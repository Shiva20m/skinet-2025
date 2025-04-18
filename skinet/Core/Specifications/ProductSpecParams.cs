namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int maxPageSize= 50;
        public int  pageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => (_pageSize <= 0) ? 6 : _pageSize;
            set
            {
                Console.WriteLine($"PageSize set to: {value}");
                _pageSize = (value <= 0) ? 6 : Math.Min(value, maxPageSize);
            }
        }
        private List<string> _brands=[];
        public List<string> Brands
        {
            // type=board,splint
            get=> _brands; 
            set
            {
                _brands = value.SelectMany(x=>
                  x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        private List<string> _types=[];
        public List<string> Types
        {
            // type=board,splint
            get=> _types; 
            set
            {
                _types = value.SelectMany(x=>
                  x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        public string? sort { get; set; }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set 
            { 
                _search = value.ToLower();
                
            }
        }
        
        
    }
    
}