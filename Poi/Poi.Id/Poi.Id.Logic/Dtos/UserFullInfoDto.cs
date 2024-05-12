using Poi.Id.InfraModel.DataAccess;

namespace Poi.Id.Logic.Dtos
{
    public class UserFullInfoDto
    {
        public Guid Id { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        public ICollection<App> Apps { get; set; }
    }
}
