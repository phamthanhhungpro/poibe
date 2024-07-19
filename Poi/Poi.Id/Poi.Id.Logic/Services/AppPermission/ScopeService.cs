using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Interfaces.AppPermission;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services.AppPermission
{
    public class ScopeService : IScopeService
    {
        private readonly IdDbContext _context;

        public ScopeService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> CreateScopeAsync(ScopeRequest request)
        {
            var scope = new PerScope
            {
                Name = request.Name,
                Description = request.Description,
                Code = request.Code,
                AppCode = request.AppCode
            };

            _context.PerScope.Add(scope);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = scope.Id,
                Message = "Create scope successfully"
            };
        }

        public async Task<CudResponseDto> DeleteScopeAsync(Guid id)
        {
            var scope = await _context.PerScope.FindAsync(id);
            if (scope == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Scope not found"
                };
            }

            _context.PerScope.Remove(scope);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Delete scope successfully"
            };
        }

        public async Task<PerScope> GetScopeAsync(Guid id)
        {
            return await _context.PerScope.FindAsync(id);
        }

        public async Task<List<PerScope>> GetScopesAsync(TenantInfo info)
        {
            return await _context.PerScope.Where(x => x.AppCode == info.AppCode).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateScopeAsync(Guid id, ScopeRequest request)
        {
            var scope = await _context.PerScope.FindAsync(id);
            if (scope == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Scope not found"
                };
            }

            scope.Name = request.Name;
            scope.Description = request.Description;
            scope.Code = request.Code;
            scope.AppCode = request.AppCode;

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Update scope successfully"
            };
        }
    }
}
