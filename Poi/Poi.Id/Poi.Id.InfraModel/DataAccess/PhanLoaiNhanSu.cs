using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class PhanLoaiNhanSu : BaseEntity
    {
        public string Ten { get; set; }
        public string MaPhanLoai { get; set; }
        public bool TrangThai { get; set; }

    }
}
