namespace Poi.Prj.Logic.Dtos
{
    public class DuanHoatDongDto
    {
        public string ThoiGian { get; set; }
        public List<HoatDongGroupByCongViecDto> ListCongViec { get; set; }
    }

    public class HoatDongGroupByCongViecDto
    {
        public string TenViec { get; set; }
        public Guid IdCongViec { get; set; }
        public List<CongViecHoatDongDto> ListHoatDong { get; set; }
    }
}
