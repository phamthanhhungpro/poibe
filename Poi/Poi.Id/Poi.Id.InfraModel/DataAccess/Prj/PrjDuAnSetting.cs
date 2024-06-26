using Poi.Shared.Model.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjDuAnSetting : BaseEntity
    {
        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }

        [Column(TypeName = "jsonb")]
        public List<Dictionary<string, object>> Setting { get; set; }
    }
}
