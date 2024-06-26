using Poi.Shared.Model.BaseModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Poi.Id.InfraModel.DataAccess.Prj
{
    public class PrjDuAnSetting : BaseEntity
    {
        public PrjDuAnNvChuyenMon DuAnNvChuyenMon { get; set; }
        public Guid DuAnNvChuyenMonId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string MoTa { get; set; }

    }
}
