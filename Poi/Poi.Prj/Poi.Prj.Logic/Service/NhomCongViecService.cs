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
    public class NhomCongViecService : INhomCongViecService
    {
        private readonly PrjDbContext _context;
        public NhomCongViecService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(NhomCongViecRequest request, TenantInfo info)
        {
            var isExist = await _context.PrjNhomCongViec.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                              && x.MaNhomCongViec == request.MaNhomCongViec);
            if (isExist)
            {
                return new CudResponseDto
                {
                    Message = "Mã nhóm công việc đã tồn tại",
                    IsSucceeded = false
                };
            }
            var entity = new PrjNhomCongViec
            {
                TenNhomCongViec = request.TenNhomCongViec,
                MaNhomCongViec = request.MaNhomCongViec,
                MoTa = request.MoTa,
                TenantId = info.TenantId,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId
            };

            entity.CreatedBy = info.UserId;

            _context.PrjNhomCongViec.Add(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjNhomCongViec.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.PrjNhomCongViec.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<PrjNhomCongViec> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjNhomCongViec.FindAsync(id);
        }

        public async Task<IEnumerable<PrjNhomCongViec>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjNhomCongViec
                                .Where(x => x.DuAnNvChuyenMonId == DuanId && x.TenantId == info.TenantId)
                                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, NhomCongViecRequest request, TenantInfo info)
        {
            var entity = await _context.PrjNhomCongViec.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            // Check if MaNhomCongViec is already exist
            var isExist = await _context.PrjNhomCongViec.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                                 && x.MaNhomCongViec == request.MaNhomCongViec
                                                                                 && x.Id != id);

            if (isExist)
            {
                return new CudResponseDto
                {
                    Message = "Mã nhóm công việc đã tồn tại",
                    IsSucceeded = false
                };
            }

            entity.TenNhomCongViec = request.TenNhomCongViec;
            entity.MaNhomCongViec = request.MaNhomCongViec;
            entity.MoTa = request.MoTa;
            entity.UpdatedAt = DateTime.UtcNow;

            _context.PrjNhomCongViec.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Message = "Success",
                IsSucceeded = true
            };
        }
    }
}
