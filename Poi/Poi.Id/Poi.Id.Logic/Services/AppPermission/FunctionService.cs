using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Services.AppPermission
{
    public class FunctionService : IFunctionService
    {
        private readonly IdDbContext _context;

        public FunctionService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AssignApiToFunctionAsync(AssignEndpointToFunctionRequest request, TenantInfo info)
        {
            var function = await _context.PerFunction
                .Include(x => x.Endpoints)
                .FirstOrDefaultAsync(x => x.Id == request.FunctionId && x.AppCode == info.AppCode);

            if (function == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Function not found"
                };
            }

            var endpoints = await _context.PerEndpoint
                .Where(x => request.EndPointIds.Contains(x.Id) && x.AppCode == info.AppCode)
                .ToListAsync();

            function.Endpoints = endpoints;
            function.MainEndPointId = request.MainEndPointId;

            _context.PerFunction.Update(function);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Assign api to function successfully"
            };
        }

        public async Task<CudResponseDto> AssignScopeToFunctionAsync(AssignScopeToFunctionRequest request, TenantInfo info)
        {
            var function = await _context.PerFunction
                .Include(x => x.Scopes)
                .FirstOrDefaultAsync(x => x.Id == request.FunctionId && x.AppCode == info.AppCode);

            if (function == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Function not found"
                };
            }

            var scopes = await _context.PerScope
                .Where(x => request.ScopeIds.Contains(x.Id) && x.AppCode == info.AppCode)
                .ToListAsync();

            function.Scopes = scopes;

            _context.PerFunction.Update(function);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Assign scope to function successfully"
            };
        }

        public async Task<CudResponseDto> CreateAsync(FunctionRequest request, TenantInfo info)
        {
            if (info == null) {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Tenant info not found"
                };
            }

            var model = new PerFunction
            {
                Name = request.Name,
                Description = request.Description,
                IsPublic = request.IsPublic,
                GroupFunctionId = request.GroupFunctionId,
                AppCode = info.AppCode
            };

            _context.PerFunction.Add(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Function created successfully"
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var model = _context.PerFunction.Find(id);
            if (model == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Function not found"
                };
            }

            model.IsDeleted = true;
            model.DeletedAt = DateTime.UtcNow;

            _context.PerFunction.Update(model);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Function deleted successfully"
            };
        }

        public async Task<PagingResponse<PerFunction>> GetAllAsync(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.PerFunction.Where(x => x.AppCode == info.AppCode)
                .OrderBy(o => o.GroupFunctionId)
                .ThenBy(o => o.Name)
                .AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<PerFunction>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<PerFunction> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PerFunction
                .Include(x => x.Endpoints)
                .Include(x => x.Scopes)
                .FirstOrDefaultAsync(x => x.Id == id && x.AppCode == info.AppCode);
        }

        public async Task<List<FunctionWithGroupDto>> GetFunctionWithGroup(TenantInfo info)
        {
            var allFunctions = await _context.PerFunction
                                            .Include(x => x.GroupFunction)
                                            .Include(x => x.Scopes)
                                            //.Include(x => x.PerRoleFunctionScope).ThenInclude(p => p.Role)
                                            .Where(x => x.AppCode == info.AppCode)
                                            .ToListAsync();

            var groupByGroupFunction = allFunctions.GroupBy(x => x.GroupFunctionId);
                
            var data = groupByGroupFunction.Select(x => new FunctionWithGroupDto
            {
                GroupId = x.Key,
                GroupName = x.First().GroupFunction.Name,
                ListFunction = x.ToList()
            }).ToList();

            return data;
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, FunctionRequest request, TenantInfo info)
        {
            var model = _context.PerFunction.Find(id);
            if (model == null)
            {
                return new CudResponseDto
                {
                    IsSucceeded = false,
                    Message = "Function not found"
                };
            }

            model.Name = request.Name;
            model.Description = request.Description;
            model.IsPublic = request.IsPublic;
            model.GroupFunctionId = request.GroupFunctionId;

            _context.PerFunction.Update(model);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Function updated successfully"
            };
        }
    }
}
