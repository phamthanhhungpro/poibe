namespace Poi.Id.Logic.Requests
{
    public class UpdateUserAppRequest
    {
        public List<Guid> UserIds { get; set; }
        public string UserType { get; set; }
    }
}
