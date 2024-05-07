using Microsoft.AspNetCore.Identity;

namespace Poi.Id.InfraModel.DataAccess
{
    public class User : IdentityUser<Guid>
    {
        public string SurName { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public Tenant Tenant { get; set; }
        public Group Group { get; set; }
        public Role Role { get; set; }
        public ICollection<App> Apps { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        // Collection of managers (users)
        public virtual ICollection<User> Managers { get; set; }

        // Collection of direct reports (users)
        public virtual ICollection<User> DirectReports { get; set; }
    }
}
