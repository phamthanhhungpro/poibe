namespace Poi.Id.Logic.Requests
{
    public class PhongBanRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Guid> ManagerIds { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChiNhanhVanPhongId { get; set; }
    }
}
