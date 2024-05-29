using Poi.Id.InfraModel.DataAccess;
namespace Poi.Hrm.Logic.Requests
{
    public class CreateHoSoNhanSuRequest
    {
        public string MaHoSo { get; set; }
        public string NgaySinh { get; set; }
        public string TenKhac { get; set; }
        public string NoiSinh { get; set; }
        public string QueQuan { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public string ThuongTru { get; set; }
        public string NoiOHienNay { get; set; }
        public Guid UserId { get; set; }
        public ThongTinNhanSu ThongTinThem { get; set; }
    }
}
