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
    public class GroupFunctionService : IGroupFunctionService
    {
        private readonly IdDbContext _context;

        public GroupFunctionService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> CreateGroupFunctionAsync(FunctionGroupRequest request, TenantInfo info)
        {
            var groupFunction = new PerGroupFunction
            {
                Name = request.Name,
                Description = request.Description,
                AppCode = info.AppCode
            };

            _context.PerGroupFunction.Add(groupFunction);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = groupFunction.Id,
                Message = "Create group function successfully"
            };
        }

        public async Task<CudResponseDto> DeleteGroupFunctionAsync(Guid id)
        {
            var model = _context.PerGroupFunction.Find(id);

            if (model == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy data",
                    IsSucceeded = false
                };
            }

            model.IsDeleted = true;
            model.UpdatedAt = DateTime.UtcNow;

            _context.Update(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }

        public async Task<PerGroupFunction> GetGroupFunctionAsync(Guid id)
        {
            return await _context.PerGroupFunction.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<PerGroupFunction>> GetGroupFunctionsAsync(TenantInfo info)
        {
            return await _context.PerGroupFunction.Where(a => a.AppCode == info.AppCode).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateGroupFunctionAsync(Guid id, FunctionGroupRequest request)
        {
            var model = _context.PerGroupFunction.Find(id);

            if (model == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy data",
                    IsSucceeded = false
                };
            }

            model.Name = request.Name;
            model.Description = request.Description;
            model.UpdatedAt = DateTime.UtcNow;

            _context.Update(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                IsSucceeded = true
            };
        }
    }
}
