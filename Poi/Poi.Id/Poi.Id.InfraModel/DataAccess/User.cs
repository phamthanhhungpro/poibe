﻿using Microsoft.AspNetCore.Identity;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Id.InfraModel.DataAccess.Prj;

namespace Poi.Id.InfraModel.DataAccess
{
    public class User : IdentityUser<Guid>
    {
        public string SurName { get; set; }
        public string Name { get; set; }
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

        public virtual HrmHoSoNhanSu HrmHoSoNhanSu { get; set; }

        public virtual ICollection<PhongBanBoPhan> LanhDaoPhongBan { get; set; }
        public virtual ICollection<PhongBanBoPhan> ThanhVienPhongBan { get; set; }

        public virtual ICollection<PrjToNhom> LanhDaoToNhom { get; set; }
        public virtual ICollection<PrjToNhom> ThanhVienToNhom { get; set; }
        public virtual ICollection<PrjDuAnNvChuyenMon> PrjDuAnNvChuyenMon { get; set; }
        public virtual ICollection<HrmChamCongDiemDanh> HrmChamCongDiemDanh { get; set; } // Tạo chấm công điểm danh
        public virtual ICollection<HrmChamCongDiemDanh> XacNhanChamCongDiemDanh { get; set; } // Xác nhận chấm công điểm danh

        public virtual ICollection<PerRole> PerRoles { get; set; }

        public string FullName
        {
            get { return SurName + " " + Name; }
            set { }
        }
    }
}
