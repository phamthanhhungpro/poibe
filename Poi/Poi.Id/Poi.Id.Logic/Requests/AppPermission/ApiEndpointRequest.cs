namespace Poi.Id.Logic.Requests.AppPermission
{
    public class ApiEndpointRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
    }
}
