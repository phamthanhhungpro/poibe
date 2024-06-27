namespace Poi.Prj.Logic.Dtos
{
    public class CongViecGroupByNhomCongViecDto
    {
        public string TenNhomCongViec { get; set; }
        public Guid NhomCongViecId { get; set; }
        public List<CongViecGridDto> ListCongViec { get; set; }
    }
}
