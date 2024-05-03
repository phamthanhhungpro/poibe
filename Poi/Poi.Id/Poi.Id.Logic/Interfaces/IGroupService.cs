using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface IGroupService
    {
        Task<PagingResponse<Group>> GetGroup(PagingRequest request);

        Task<Group> GetGroupById(Guid id);

        Task<CudResponseDto> CreateGroup(GroupRequest group);
        Task<CudResponseDto> UpdateGroup(Guid id, GroupRequest group);
        Task<CudResponseDto> DeleteGroup(Guid id);
    }
}
