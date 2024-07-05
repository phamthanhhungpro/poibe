using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class TokenExpired : BaseEntity
    {
        public string Token { get; set; }
    }
}
