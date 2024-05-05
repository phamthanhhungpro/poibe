using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;

namespace Poi.Id.Logic.Interfaces
{
    public interface IRoleService
    {
        Task<PagingResponse<Role>> GetRole(PagingRequest request);
        Task<List<Role>> GetNoPaging();

        Task<Role> GetRoleById(Guid id);

        Task<CudResponseDto> CreateRole(RoleRequest role);
        Task<CudResponseDto> UpdateRole(Guid id, RoleRequest role);
        Task<CudResponseDto> DeleteRole(Guid id);
    }
}
