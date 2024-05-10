﻿using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces
{
    public interface IUserService
    {
        Task<PagingResponse<UserListInfoDto>> GetUsers(PagingRequest request, TenantInfo info);

        Task<CudResponseDto> UpdateUser(Guid id, UpdateUserRequest request);

        Task<CudResponseDto> DeleteUser(Guid id);

        Task<User> GetUserById(Guid id);
    }
}
