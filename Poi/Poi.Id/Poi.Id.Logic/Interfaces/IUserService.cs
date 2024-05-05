using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;

namespace Poi.Id.Logic.Interfaces
{
    public interface IUserService
    {
        Task<PagingResponse<UserListInfoDto>> GetUsers(PagingRequest request);
    }
}
