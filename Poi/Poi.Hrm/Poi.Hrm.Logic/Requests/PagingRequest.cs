namespace Poi.Hrm.Logic.Requests
{
    public class PagingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; }
    }
}
