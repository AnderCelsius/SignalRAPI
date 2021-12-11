namespace SignalRAPI.Utilities.Pagination
{
    public class PaginatorHelper<T>
    {
        public T PageItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfPages { get; set; }
        public int PreviousPage { get; set; }
    }
}
