using Poi.Id.InfraModel.DataAccess;

namespace Poi.Hrm.Logic.Dtos
{
    public class DiemDanhHistoryGroupDto
    {
        public User User { get; set; }

        public IEnumerable<DateTime> Times { get; set; }
    }
}
