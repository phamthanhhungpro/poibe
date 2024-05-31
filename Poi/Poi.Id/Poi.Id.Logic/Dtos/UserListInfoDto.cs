using Poi.Id.InfraModel.DataAccess;

namespace Poi.Id.Logic.Dtos
{
    public class UserListInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleCode { get; set; }
        public string GroupName { get; set; }
        public string TenantName { get; set; }
        public bool IsActive { get; set; }

        public IList<App> Apps { get; set; }

        public string FullName
        {
            get { return SurName + " " + Name; }
            set { }
        }
    }
}
