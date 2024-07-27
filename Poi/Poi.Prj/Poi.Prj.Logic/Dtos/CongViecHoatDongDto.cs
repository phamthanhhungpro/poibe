using Poi.Id.InfraModel.DataAccess.Prj;

namespace Poi.Prj.Logic.Dtos
{
    public class CongViecHoatDongDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string ThoiGian { get; set; }
        public string NoiDung { get; set; }
        public string MoreInfo { get; set; }
    }
}
