namespace Poi.Id.Logic.Requests
{
    public class UpdatePermissionRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
    }
}
