using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class AFeedback : BaseEntity
    {
        public Guid? UserId { get; set; }
        public string AppName { get; set; }
        public string Tittle { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public string Attachments { get; set; }
    }
}
