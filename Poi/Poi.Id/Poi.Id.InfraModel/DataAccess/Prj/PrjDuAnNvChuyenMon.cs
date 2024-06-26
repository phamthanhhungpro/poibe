using Poi.Shared.Model.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjDuAnNvChuyenMon : BaseEntity
    {
        public string TenDuAn { get; set; }
        public string MoTaDuAn { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public PrjToNhom ToNhom { get; set; }
        public Guid? ToNhomId { get; set; }

        public PhongBanBoPhan PhongBanBoPhan { get; set; }
        public Guid? PhongBanBoPhanId { get; set; }

        public User QuanLyDuAn { get; set; }
        public Guid? QuanLyDuAnId { get; set; }

        public ICollection<User> ThanhVienDuAn { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }

        public PrjLinhVuc LinhVuc { get; set; }
        public Guid? LinhVucId { get; set; }
        public bool IsNhiemVuChuyenMon { get; set; }
        public ICollection<PrjNhomCongViec> NhomCongViec { get; set; }
        public bool IsCaNhan { get; set; }
        public ICollection<PrjDuAnSetting> DuAnSetting { get; set; }

    }
}
