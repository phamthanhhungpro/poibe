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
    public class LoaiCongViecService : ILoaiCongViecService
    {
        private readonly PrjDbContext _context;

        public LoaiCongViecService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(LoaiCongViecRequest request, TenantInfo info)
        {
            var loaiCongViec = new PrjLoaiCongViec
            {
                TenantId = info.TenantId,
                TenLoaiCongViec = request.TenLoaiCongViec,
                MaLoaiCongViec = request.MaLoaiCongViec,
                MoTa = request.MoTa,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId
            };

            _context.PrjLoaiCongViec.Add(loaiCongViec);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = loaiCongViec.Id,
                Message = "Thêm mới thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var loaiCongViec = await _context.PrjLoaiCongViec.FindAsync(id);
            if (loaiCongViec == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            loaiCongViec.IsDeleted = true;
            loaiCongViec.DeletedAt = DateTime.UtcNow;

            _context.PrjLoaiCongViec.Update(loaiCongViec);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjLoaiCongViec> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjLoaiCongViec.FindAsync(id);
        }

        public async Task<IEnumerable<PrjLoaiCongViec>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjLoaiCongViec
                .Where(x => x.TenantId == info.TenantId && x.DuAnNvChuyenMonId == DuanId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, LoaiCongViecRequest request, TenantInfo info)
        {
            var loaiCongViec = await _context.PrjLoaiCongViec
                .Include(x => x.DuAnNvChuyenMon)
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);

            if (loaiCongViec == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            loaiCongViec.TenLoaiCongViec = request.TenLoaiCongViec;
            loaiCongViec.MaLoaiCongViec = request.MaLoaiCongViec;
            loaiCongViec.MoTa = request.MoTa;
            loaiCongViec.UpdatedAt = DateTime.UtcNow;

            _context.PrjLoaiCongViec.Update(loaiCongViec);
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
