using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjCongViec : BaseEntity
    {
        public string TenCongViec { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int TrangThai { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }

        public PrjNhomCongViec NhomCongViec { get; set; }
        public Guid? NhomCongViecId { get; set; }

        public PrjLoaiCongViec LoaiCongViec { get; set; }
        public Guid? LoaiCongViecId { get; set; }
        public PrjCongViec CongViecCha { get; set; }
        public Guid? CongViecChaId { get; set; }
    }
}
