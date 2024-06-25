using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Service
{
    public class TagCongViecService : ITagCongViecService
    {
        private readonly PrjDbContext _context;

        public TagCongViecService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(TagCongViecRequest request, TenantInfo info)
        {
            var TagCongViec = new PrjTagCongViec
            {
                TenantId = info.TenantId,
                TenTag = request.TenTag,
                MaTag = request.MaTag,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId
            };

            _context.PrjTagCongViec.Add(TagCongViec);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = TagCongViec.Id,
                Message = "Thêm mới thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var TagCongViec = await _context.PrjTagCongViec.FindAsync(id);
            if (TagCongViec == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            TagCongViec.IsDeleted = true;
            TagCongViec.DeletedAt = DateTime.UtcNow;

            _context.PrjTagCongViec.Update(TagCongViec);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjTagCongViec> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjTagCongViec.FindAsync(id);
        }

        public async Task<IEnumerable<PrjTagCongViec>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjTagCongViec
                .Where(x => x.TenantId == info.TenantId && x.DuAnNvChuyenMonId == DuanId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, TagCongViecRequest request, TenantInfo info)
        {
            var TagCongViec = await _context.PrjTagCongViec
                .Include(x => x.DuAnNvChuyenMon)
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);

            if (TagCongViec == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            TagCongViec.TenTag = request.TenTag;
            TagCongViec.MaTag = request.MaTag;
            TagCongViec.UpdatedAt = DateTime.UtcNow;

            _context.PrjTagCongViec.Update(TagCongViec);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }
    }
}
