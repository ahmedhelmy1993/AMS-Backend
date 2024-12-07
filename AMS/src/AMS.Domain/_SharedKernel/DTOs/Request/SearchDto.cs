
namespace AMS.Domain._SharedKernel.DTOs.Request
{
    public class SearchDto<T>  where T : class
    {
        public T Filter { get; set; }
        public PagingModel Paginator { get; set; }
        public SortingModel Sorting { get; set; }

        public SearchDto()
        {
            Sorting = new();
            Paginator = new();
        }

    }
}
