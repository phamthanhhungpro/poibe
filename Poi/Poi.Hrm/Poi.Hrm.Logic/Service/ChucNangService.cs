using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Service
{
    public class ChucNangService : IChucNangService
    {
        private readonly HrmDbContext _context;
        public ChucNangService(HrmDbContext context)
        {

            _context = context;

        }

        public async Task<CudResponseDto> CreateChucNang(TenantInfo tenantInfo, ChucNangRequest ChucNang)
        {
            var data = new HrmChucNang
            {
                TenChucNang = ChucNang.TenChucNang,
                Description = ChucNang.Description,
                Method = ChucNang.Method,
                Path = ChucNang.Path,
                IsPublic = ChucNang.IsPublic
            };

            _context.HrmChucNang.Add(data);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = data.Id,
                Message = "Success"
            };
        }

        public async Task<CudResponseDto> DeleteChucNang(TenantInfo tenantInfo, Guid id)
        {
            var data = await _context.HrmChucNang.FindAsync(id);
            if (data == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not Found"
                };
            }

            data.IsDeleted = true;
            data.DeletedAt = DateTime.UtcNow;

            _context.HrmChucNang.Update(data);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success"
            };
        }

        public async Task<List<HrmChucNang>> GetChucNang(TenantInfo tenantInfo)
        {
            return await _context.HrmChucNang.OrderBy(o => o.TenChucNang).ToListAsync();
        }

        public async Task<HrmChucNang> GetChucNangById(TenantInfo tenantInfo, Guid id)
        {
            return await _context.HrmChucNang.FindAsync(id);
        }

        public async Task<PagingResponse<HrmChucNang>> GetPagingChucNang(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _context.HrmChucNang.OrderByDescending(o => o.CreatedAt).AsNoTracking();


            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<HrmChucNang>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = data,
                Count = count
            };
        }

        public async Task<CudResponseDto> UpdateChucNang(Guid id, TenantInfo tenantInfo, ChucNangRequest ChucNang)
        {
            var data = await _context.HrmChucNang.FindAsync(id);
            if (data == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not Found"
                };
            }

            data.TenChucNang = ChucNang.TenChucNang;
            data.Description = ChucNang.Description;
            data.Method = ChucNang.Method;
            data.Path = ChucNang.Path;
            data.IsPublic = ChucNang.IsPublic;

            _context.HrmChucNang.Update(data);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success"
            };
        }
    }
}
