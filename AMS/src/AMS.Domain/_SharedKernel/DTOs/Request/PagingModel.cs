namespace AMS.Domain._SharedKernel.DTOs.Request
{
    public class PagingModel
    {
        private int _Page;
        
        public int Page
        {
            get
            {
                return _Page;
            }
            set
            {
                if (value > 0)
                    value -= 1;

                _Page = value;
            }
        }
        public int PageSize { get; set; }
    }
}
