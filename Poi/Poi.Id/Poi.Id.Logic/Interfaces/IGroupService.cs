using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface IGroupService
    {
        Task<PagingResponse<Group>> GetGroup(PagingRequest request, TenantInfo info);

        Task<Group> GetGroupById(Guid id);

        Task<CudResponseDto> CreateGroup(GroupRequest group, TenantInfo info);
        Task<CudResponseDto> UpdateGroup(Guid id, GroupRequest group, TenantInfo info);
        Task<CudResponseDto> DeleteGroup(Guid id);
    }
}
