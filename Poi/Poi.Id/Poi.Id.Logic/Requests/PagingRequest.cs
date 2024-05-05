namespace Poi.Id.Logic.Requests
{
    public class PagingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? TenantId { get; set; }

        public string Role { get; set; }
        public string Search { get; set; }
    }
}
