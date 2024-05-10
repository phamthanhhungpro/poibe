namespace Poi.Id.Logic.Requests
{
    public class CreateUserRequest
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid? RoleId { get; set; }
        public List<Guid> AppIds { get; set; }
        public Guid? TenantId { get; set; }
    }
}
