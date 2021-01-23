namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize =50;
        public int PageIndex {get; set;}=1 ;// by default we display the first page
        private int _pageSize=6;
        public int PageSize
        {
            get =>_pageSize;
            set =>_pageSize=(value > MaxPageSize)? MaxPageSize:value; //protect not to send more than 50 product
        }
            public int? BrandId { get; set; }
            public int? TypeId { get; set; }
            public string sort {get;set;}
            private string _search;
            public string Search
            {
                get =>_search;      //the get will return the search result
                set => _search=value.ToLower();  //we always converting values to lower case                
            }
    }
}